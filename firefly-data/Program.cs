using System.Security.Claims;
using firefly_iii_odata.Crypto;
using firefly_iii_odata.Data;
using firefly_iii_odata.Middleware;
using firefly_iii_odata.Models;
using firefly_iii_odata.Models.Formatted;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddConfiguration(builder.Configuration);

var connectionString = builder.Configuration.GetConnectionString("Firefly");
var serverVersion = ServerVersion.AutoDetect(connectionString);

var appKey = builder.Configuration["AppKey"] ?? throw new Exception("No AppKey found in configuration");

builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddDbContext<FireflyContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString, serverVersion);

        if (builder.Environment.IsDevelopment())
        {
            // The following three options help with debugging, but should
            // be changed or removed for production.
            dbContextOptions.LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    });

builder.Services.AddSingleton<RsaSecurityKey>(provider =>
{
    var db = provider.GetRequiredService<FireflyContext>();
    // var logger = provider.GetRequiredService<ILogger>();
    // logger.LogInformation("Using app key {AppKey}", appKey);
    var privateKeyLoader = new RsaSecurityKeyLoader(new firefly_iii_odata.Config.AppKeyHolder { AppKey = appKey }, db);
    return privateKeyLoader.LoadKey();
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});
builder.Services
    .AddAuthentication()
    .AddJwtBearer(options =>
    {
        SecurityKey rsa = builder.Services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
        // options.Audience = "1";
        options.RequireHttpsMetadata = false;
        options.IncludeErrorDetails = true;
        options.UseSecurityTokenValidators = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = rsa,
            RequireSignedTokens = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            NameClaimType = ClaimTypes.NameIdentifier
        };
    });

builder
    .Services
    .AddControllers()
    .AddOData(opt =>
    {
        opt.AddRouteComponents("odata", GetEdmModel())
            .Select()
            .Filter()
            .OrderBy()
            .Count();
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<BasicAuthTranslator>();
app.UseMiddleware<PathAliasMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseODataRouteDebug();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();
    builder.EntitySet<Budget>("Budgets")
        .Binding
        .HasManyPath(b => b.AutoBudgets);

    var transactionSet = builder.EntitySet<Transaction>("Transactions");
    transactionSet.Binding.HasSinglePath(t => t.TransactionJournal);
    transactionSet.Binding.HasSinglePath(t => t.TransactionCurrency);

    builder.EntitySet<BudgetLimit>("BudgetLimits");

    builder.EntitySet<Account>("Accounts")
        .Binding
        .HasSinglePath(a => a.AccountType);

    builder.EntitySet<TransactionType>("TransactionTypes");
    builder.EntitySet<FormattedTransaction>("FormattedTransactions");
    builder.EntitySet<FormattedAccount>("FormattedAccounts");
    builder.EntitySet<FormattedBudgetLimit>("FormattedBudgetLimits");
    return builder.GetEdmModel();
}