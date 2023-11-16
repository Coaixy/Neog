using Microsoft.Win32;

namespace Neog;

public class Proxy
{
    private string ProxyServer { get; set; } = "127.0.0.1";
    private string ProxyPort { get; set; } = "4399";

    public bool SetProxy(string proxyServer,string proxyPort,bool enableProxy)
    {
        if (Helper.IsWindows())
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
        return SetProxy(this.ProxyServer,this.ProxyPort,true);
    }
    public bool Close()
    {
        return SetProxy(this.ProxyServer, this.ProxyPort, false);
    }
}