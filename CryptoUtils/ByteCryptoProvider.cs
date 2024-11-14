using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CryptoUtils;

public class ByteCryptoProvider: ICryptoProvider
{
    private readonly byte[] _entropyBytes;
    private readonly string _baseDirectoryPath;
    
    public ByteCryptoProvider(string entropySalt, string saveDirectoryPath = "")
    {
        _entropyBytes = Encoding.UTF8.GetBytes(entropySalt);
        _baseDirectoryPath = string.IsNullOrEmpty(saveDirectoryPath)
            ? AppDomain.CurrentDomain.BaseDirectory
            : saveDirectoryPath;
    }

    public void EncryptToFile<TData>(TData data, string fileName)
    {
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(TData));
            using var memoryStream = new MemoryStream();
            using var xmlWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            xmlSerializer.Serialize(xmlWriter, data);
            var protectedData = ProtectedData.Protect(memoryStream.ToArray(), _entropyBytes, DataProtectionScope.LocalMachine);
            File.WriteAllBytes($"{Path.Combine(_baseDirectoryPath, fileName)}.protected", protectedData);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }
    }

    public TData DecryptFromFile<TData>(string fileName)
    {
        try
        {
            var xmlSerializer = new XmlSerializer(typeof(TData));
            var protectedData = File.ReadAllBytes($"{Path.Combine(_baseDirectoryPath, fileName)}.protected");
            var data = ProtectedData.Unprotect(protectedData, _entropyBytes, DataProtectionScope.LocalMachine);
            using var memoryStream = new MemoryStream(data);
            return (TData)xmlSerializer.Deserialize(memoryStream);
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return default(TData);
        }
    }
}