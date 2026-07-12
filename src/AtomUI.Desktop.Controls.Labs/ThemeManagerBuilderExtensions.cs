using AtomUI.Theme;

namespace AtomUI.Desktop.Controls.Labs;

public static class LabsThemeManagerBuilderExtensions
{
    public static IThemeManagerBuilder UseDesktopLabs(this IThemeManagerBuilder themeManagerBuilder)
    {
        themeManagerBuilder.AddControlThemesProvider(new AtomUILabsThemesProvider());
        return themeManagerBuilder;
    }
}
