using System.Security.Cryptography.X509Certificates;
using Fiddler;

namespace Neog;

public class OsHelper
{
    public static bool IsWindows()
    {
        return (Environment.OSVersion.Platform == PlatformID.Win32NT ||
                Environment.OSVersion.Platform == PlatformID.Win32S ||
                Environment.OSVersion.Platform == PlatformID.Win32Windows ||
                Environment.OSVersion.Platform == PlatformID.WinCE);
    }

    public static void Certify()
    {
        //创建证书并信任
        CertMaker.createRootCert();
        if (!CertMaker.rootCertExists())
        {
            throw new Exception("创建失败");
        }

        X509Store x509Store = new X509Store(StoreName.Root);
        x509Store.Open(OpenFlags.ReadWrite);
        X509Certificate2 cert = CertMaker.GetRootCertificate();
        x509Store.Add(cert);
        x509Store.Close();
    }
}