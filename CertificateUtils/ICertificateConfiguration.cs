using System.Security.Cryptography.X509Certificates;

namespace CertificateUtils;

public interface ICertificateConfiguration
{
    int DurationInDays { get; set; }
    string Name { get; set; }
    string Password { get; set; }
    string OutPathFolder { get; set; }
}

public interface IChildCertificateConfiguration : ICertificateConfiguration
{
    X509Certificate2 Root { get; set; }
}