using Avalonia;

namespace AtomUI.Desktop.Controls.Labs.GlowPrototype.Desktop;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<GlowPrototypeApplication>()
                         .UsePlatformDetect()
                         .LogToTrace();
    }
}
