namespace CertificateUtils;

public interface ICertificateGenerator
{
    void CreateRootCertificate(ICertificateConfiguration configuration);
    void GenerateChildCertificate(IChildCertificateConfiguration configuration);
}