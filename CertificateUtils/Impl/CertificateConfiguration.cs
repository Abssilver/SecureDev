using System.Security.Cryptography.X509Certificates;

namespace CertificateUtils.Impl;

public class CertificateConfiguration : ICertificateConfiguration
{
    public int DurationInDays { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string OutPathFolder { get; set; }
}

public class ChildCertificateConfiguration : IChildCertificateConfiguration
{
    public int DurationInDays { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public string OutPathFolder { get; set; }
    public X509Certificate2 Root { get; set; }
}