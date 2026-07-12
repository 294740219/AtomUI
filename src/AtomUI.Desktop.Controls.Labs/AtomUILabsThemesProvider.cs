using AtomUI.Theme;
using Avalonia.Markup.Xaml;

namespace AtomUI.Desktop.Controls.Labs;

internal class AtomUILabsThemesProvider : ControlThemesProvider
{
    public AtomUILabsThemesProvider()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
