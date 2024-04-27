using System.Security.Cryptography;
using System.Text;

namespace Task3_Paper_Rock_Scissor;

public sealed class HmacGenerator: IDisposable
{
    private const int MinimumKeyLength = 4;
    private byte[] Key { get; init; }
    private HMACSHA256 HmacSha256 { get; init; }

    private HmacGenerator(byte[] key)
    {
        Key = key;
        HmacSha256 = new HMACSHA256(Key);
    }

    public static HmacGenerator HmacGeneratorFactory()
    {
        var rnd = new Random();
        var length = rnd.Next(MinimumKeyLength, MinimumKeyLength + 50);
        var key = new byte[length];
        rnd.NextBytes(key);

        return new(key);
    }

    public string GetHmac(string text)
    {
        var hash = HmacSha256.ComputeHash(Encoding.UTF8.GetBytes(text));
        
        return Convert.ToHexString(hash);
    }

    public string GetKey() => Convert.ToHexString(Key);
    
    public void Dispose()
    {
        HmacSha256.Dispose();
    }
}