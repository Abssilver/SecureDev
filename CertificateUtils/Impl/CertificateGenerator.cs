using System.Collections;
using System.Reflection;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.BouncyCastle.X509.Extension;

namespace CertificateUtils.Impl;

public class CertificateGenerator: ICertificateGenerator
{
    public void CreateRootCertificate(ICertificateConfiguration configuration)
    {
        var random = new SecureRandom();
        var generator = new RsaKeyPairGenerator();
        var rsaParams =
            new RsaKeyGenerationParameters(new BigInteger("10001", 16), random, 1024, 4);
        generator.Init(rsaParams);
        var keyPair = generator.GenerateKeyPair();
        var issuer = "CN=" + configuration.Name;

        var p12FileName = Path.Combine(configuration.OutPathFolder, $"{configuration.Name}.p12");
        var crtFileName = Path.Combine(configuration.OutPathFolder, $"{configuration.Name}.crt");

        var serialNumber = BitConverter.GetBytes(DateTime.Now.ToBinary());
        var certGenerator = new X509V3CertificateGenerator();
        certGenerator.SetSerialNumber(new BigInteger(1, serialNumber));
        certGenerator.SetIssuerDN(new X509Name(issuer));
        certGenerator.SetNotBefore(DateTime.Now.ToUniversalTime());
        certGenerator.SetNotAfter(DateTime.Now.ToUniversalTime() + TimeSpan.FromDays(configuration.DurationInDays));
        certGenerator.SetSubjectDN(new X509Name(issuer));
        certGenerator.SetPublicKey(keyPair.Public);
        certGenerator.SetSignatureAlgorithm("MD5WithRSA");
        certGenerator.AddExtension(X509Extensions.AuthorityKeyIdentifier, false,
            new AuthorityKeyIdentifierStructure(keyPair.Public));
        certGenerator.AddExtension(X509Extensions.SubjectKeyIdentifier, false,
            new SubjectKeyIdentifierStructure(keyPair.Public));
        certGenerator.AddExtension(X509Extensions.BasicConstraints, false,
            new BasicConstraints(true));

        var rootCertificate = certGenerator.Generate(keyPair.Private);
        try
        {
            using var stream = new FileStream(p12FileName, FileMode.Create);
            var p12 = new Pkcs12Store();
            var entry = new X509CertificateEntry(rootCertificate);
            p12.SetKeyEntry(configuration.Name, new AsymmetricKeyEntry(keyPair.Private),
                new [] { entry });
            p12.Save(stream, configuration.Password.ToCharArray(), random);
            stream.Close();
        }
        catch (Exception exception)
        {
            throw new CertificateGenerationException(
                "Error while saving private part of certificate.\r\n" + exception.Message);
        }

        var rawCertificate = rootCertificate.GetEncoded();
        try
        {
            using var stream = new FileStream(crtFileName, FileMode.Create);
            stream.Write(rawCertificate, 0, rawCertificate.Length);
            stream.Close();
        }
        catch (Exception exception)
        {
            throw new CertificateGenerationException(
                "Error while saving public part of certificate.\r\n" + exception.Message);
        }
    }

    public void GenerateChildCertificate(IChildCertificateConfiguration configuration)
    {
            var rootCertificateInternal = DotNetUtilities.FromX509Certificate(configuration.Root);
            var random = new SecureRandom();
            var generator = new RsaKeyPairGenerator();
            var rsaParams = new RsaKeyGenerationParameters(new BigInteger("10001", 16), random, 1024, 4);
            generator.Init(rsaParams);
            var keyPair = generator.GenerateKeyPair();

            var subject = "CN=" + configuration.Name;
            var p12FileName = Path.Combine(configuration.OutPathFolder, $"{configuration.Name}.p12");

            var serialNumber = BitConverter.GetBytes(DateTime.Now.ToBinary());

            var certGenerator = new X509V3CertificateGenerator();
            certGenerator.SetSerialNumber(new BigInteger(1, serialNumber));
            certGenerator.SetIssuerDN(rootCertificateInternal.IssuerDN);
            certGenerator.SetNotBefore(DateTime.Now.ToUniversalTime());

            certGenerator.SetNotAfter(DateTime.Now.ToUniversalTime() + TimeSpan.FromDays(configuration.DurationInDays));
            certGenerator.SetSubjectDN(new X509Name(subject));
            certGenerator.SetPublicKey(keyPair.Public);
            certGenerator.SetSignatureAlgorithm("MD5WithRSA");
            certGenerator.AddExtension(X509Extensions.AuthorityKeyIdentifier, false,
                new AuthorityKeyIdentifierStructure(rootCertificateInternal.GetPublicKey()));
            certGenerator.AddExtension(X509Extensions.SubjectKeyIdentifier, false,
                new SubjectKeyIdentifierStructure(keyPair.Public));
            var keyUsage = new KeyUsage(configuration.Name.EndsWith("CA") ? 182 : 176);
            certGenerator.AddExtension(X509Extensions.KeyUsage, true, keyUsage);
            var keyPurposes = new ArrayList
            {
                KeyPurposeID.IdKPServerAuth,
                KeyPurposeID.IdKPCodeSigning,
                KeyPurposeID.IdKPEmailProtection,
                KeyPurposeID.IdKPClientAuth
            };
            certGenerator.AddExtension(X509Extensions.ExtendedKeyUsage, true,
                new ExtendedKeyUsage(keyPurposes));
            if (configuration.Name.EndsWith("CA"))
                certGenerator.AddExtension(X509Extensions.BasicConstraints, true, new BasicConstraints(true));

            var fieldInfo = typeof(X509V3CertificateGenerator).GetField("tbsGen", BindingFlags.NonPublic | BindingFlags.Instance);
            var v3TbsCertificateGenerator = (V3TbsCertificateGenerator)fieldInfo.GetValue(certGenerator);
            var tbsCert = v3TbsCertificateGenerator.GenerateTbsCertificate();

            var md5 = new MD5CryptoServiceProvider();
            var tbsCertHash = md5.ComputeHash(tbsCert.GetDerEncoded());
            var signer = new RSAPKCS1SignatureFormatter();
            signer.SetHashAlgorithm("MD5");
            signer.SetKey(configuration.Root.PrivateKey);

            var certSignature = signer.CreateSignature(tbsCertHash);
            var signedCertificate = new X509Certificate(
                    new X509CertificateStructure(tbsCert,
                        new AlgorithmIdentifier(PkcsObjectIdentifiers.MD5WithRsaEncryption),
                        new DerBitString(certSignature)));
            try
            {
                using var stream = new FileStream(p12FileName, FileMode.Create);
                var p12 = new Pkcs12Store();
                var certEntry = new X509CertificateEntry(signedCertificate);
                var rootCertEntry = new X509CertificateEntry(rootCertificateInternal);
                p12.SetKeyEntry(configuration.Name, new AsymmetricKeyEntry(keyPair.Private),
                    new[] { certEntry, rootCertEntry });
                p12.Save(stream, configuration.Password.ToCharArray(), random);
                stream.Close();
            }
            catch (Exception exception)
            {
                throw new CertificateGenerationException(
                    "Error while saving private part of certificate.\r\n" + exception.Message);
            }
    }
}