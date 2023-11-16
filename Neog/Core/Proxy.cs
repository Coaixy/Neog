using Microsoft.Win32;

namespace Neog;

public class Proxy
{
    private string ProxyServer => "127.0.0.1";
    private string ProxyPort => "4399";

    public bool SetProxy(string proxyServer,string proxyPort,bool enableProxy)
    {
        if (OsHelper.IsWindows())
        {
            RegistryKey registry =
                Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings",
                    true);
            if (registry != null)
            {
                registry.SetValue("ProxyServer", proxyServer + ":" + proxyPort);
                registry.SetValue("ProxyEnable", enableProxy ? 1 : 0);
                registry.Close();
            }
            else
            {
                return false;
            }
        }
        return true;
    }
    public bool Open()
    {
        return SetProxy(ProxyServer,ProxyPort,true);
    }
    public bool Close()
    {
        return SetProxy(ProxyServer, ProxyPort, false);
    }

    public override string ToString()
    {
        return ProxyServer+":"+ProxyPort;
    }
}