namespace firefly_iii_odata.Crypto;
public record LaravelEncryptedData(string Iv, string Value, string Mac, string Tag);