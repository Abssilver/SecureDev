namespace GRPCServices;

public class HttpsConfig
{
    public ushort HttpsPort { get; set; }
    public string CertificatePath { get; set; }
    public string CertificatePassword { get; set; }
}