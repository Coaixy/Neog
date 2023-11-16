using Fiddler;

namespace Neog;

public class Fd
{
    private readonly FiddlerCoreStartupSettingsBuilder _builderInstance;

    Fd(Proxy proxy)
    {
        FiddlerCoreStartupSettingsBuilder fiddlerCoreStartupSettingsBuilder = new FiddlerCoreStartupSettingsBuilder();
        fiddlerCoreStartupSettingsBuilder.DecryptSSL();
        fiddlerCoreStartupSettingsBuilder.ListenOnPort(Convert.ToUInt16(proxy.ProxyPort));
        _builderInstance = fiddlerCoreStartupSettingsBuilder;
    }

    public void HandleBefore(Action<Session> requestHandle)
    {
        FiddlerApplication.BeforeRequest += session => { requestHandle(session); };
    }

    public static Fd Create(Proxy proxy)
    {
        return new Fd(proxy);
    }

    public void Run()
    {
        FiddlerApplication.Startup(_builderInstance.Build());
    }

    public void Stop()
    {
        FiddlerApplication.Shutdown();
    }
}