using System.Security.Cryptography;

namespace firefly_iii_odata;

public static class Decryptor
{
    public static byte[] Decrypt(byte[] cipherText, byte[] appKey, byte[] iv)
    {
        // Decrypt the encrypted data using the derived encryption key and the IV.
        using var aes = Aes.Create();
        aes.Key = appKey;
        aes.IV = iv;
        // aes.BlockSize = 256;
        aes.Padding = PaddingMode.PKCS7;
        aes.Mode = CipherMode.CBC;
        using var decryptor = aes.CreateDecryptor();
        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var ms = new MemoryStream();
        csDecrypt.CopyTo(ms);
        return ms.ToArray();
    }
}