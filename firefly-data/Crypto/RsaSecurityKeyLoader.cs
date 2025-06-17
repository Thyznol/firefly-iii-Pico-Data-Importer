using System.Security.Cryptography;
using System.Text.Json;
using firefly_iii_odata.Config;
using firefly_iii_odata.Data;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace firefly_iii_odata.Crypto;

class RsaSecurityKeyLoader
{
    private readonly AppKeyHolder _appKey;
    private readonly FireflyContext _db;

    public RsaSecurityKeyLoader(AppKeyHolder appKey, FireflyContext db)
    {
        _appKey = appKey;
        _db = db;
    }

    public RsaSecurityKey LoadKey()
    {
        var publicKeyConfig = _db.Configurations.Single(c => c.Name == "oauth_public_key");
        var encryptedKey = publicKeyConfig.Data.Replace("\"", "");
        var encryptedValueBytes = Convert.FromBase64String(encryptedKey);
        var encryptedData = JsonSerializer.Deserialize<LaravelEncryptedData>(new MemoryStream(encryptedValueBytes),
                                new JsonSerializerOptions
                                {
                                    PropertyNameCaseInsensitive = true
                                })
                            ?? throw new Exception("Invalid encrypted value found in database");

        //TODO: Verify MAC..
        // Console.WriteLine(_appKey.AppKey);
        var key = Encoding.ASCII.GetBytes(_appKey.AppKey);
        // Console.WriteLine($"Key: {key}. Length: {key.Length}");

        var cipherText = Convert.FromBase64String(encryptedData.Value);
        var iv = Convert.FromBase64String(encryptedData.Iv);
        var plainText = Decryptor.Decrypt(cipherText, key, iv);
        // Console.WriteLine();
        var keyText = Encoding.ASCII.GetString(plainText);
        keyText = keyText[((byte)((char)((byte)keyText.IndexOf('-'))))..keyText.LastIndexOf('-')].ReplaceLineEndings();
        // Console.WriteLine(keyText);
        // Console.WriteLine("***");
        var justEncoded = string.Join("", keyText.Split('\n')[1..^1]);
        // Console.WriteLine(justEncoded);
        var source = Convert.FromBase64String(justEncoded);
        RSA rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(
            source: source,
            bytesRead: out int _
        );

        return new RsaSecurityKey(rsa);
    }
}