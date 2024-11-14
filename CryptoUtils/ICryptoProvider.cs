namespace CryptoUtils;

public interface ICryptoProvider
{
    public void EncryptToFile<TData>(TData data, string filePath);
    public TData DecryptFromFile<TData>(string filePath);
}