using AtomUI.Desktop.Controls.Labs;
using AtomUI.Desktop.Controls.Labs.LED.Matrix;
using AtomUI.Desktop.Controls.Labs.LED.Segment;
using AtomUI.Theme;
using AtomUI.Theme.Styling;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Threading;

namespace AtomUI.Desktop.Controls.Labs.Sample;

public class MainWindow : Window
{
    private readonly SegmentDisplay _clockDisplay;
    private readonly SegmentDisplay _counterDisplay;
    private readonly MatrixDisplay _matrixCounterDisplay;
    private readonly CheckBox _darkThemeCheckBox;
    private readonly CheckBox _compactThemeCheckBox;
    private readonly DispatcherTimer _dynamicDisplayTimer;
    private readonly GlowWorkbench _glowWorkbench;
    private int _counterValue;

    public MainWindow()
    {
        _clockDisplay = new SegmentDisplay
        {
            CharacterHeight  = 64,
            SegmentThickness = 7,
            SegmentGap       = 2,
            CharacterSpacing = 8,
            Padding          = new Thickness(16),
            Background       = new SolidColorBrush(Color.FromRgb(8, 12, 16)),
            ActiveBrush      = new SolidColorBrush(Color.FromRgb(83, 237, 255)),
            InactiveBrush    = new SolidColorBrush(Color.FromArgb(34, 83, 237, 255)),
            CornerRadius     = new CornerRadius(10)
        };
        _counterDisplay = new SegmentDisplay
        {
            CharacterHeight      = 54,
            SegmentThickness     = 6,
            SegmentGap           = 2,
            CharacterSpacing     = 7,
            Padding              = new Thickness(14),
            Background           = new SolidColorBrush(Color.FromRgb(16, 12, 8)),
            ActiveBrush          = new SolidColorBrush(Color.FromRgb(255, 168, 64)),
            InactiveBrush        = new SolidColorBrush(Color.FromArgb(34, 255, 168, 64)),
            CornerRadius         = new CornerRadius(10),
            ShowInactiveSegments = true
        };
        _matrixCounterDisplay = new MatrixDisplay
        {
            Text             = "000000",
            DotSize          = 7,
            DotSpacing       = 2,
            CharacterSpacing = 9,
            Padding          = new Thickness(14),
            Background       = new SolidColorBrush(Color.FromRgb(9, 18, 14)),
            ActiveBrush      = new SolidColorBrush(Color.FromRgb(97, 247, 157)),
            InactiveBrush    = new SolidColorBrush(Color.FromArgb(34, 97, 247, 157)),
            CornerRadius     = new CornerRadius(8)
        };
        _darkThemeCheckBox = CreateThemeCheckBox("Dark");
        _compactThemeCheckBox = CreateThemeCheckBox("Compact");
        _darkThemeCheckBox.IsCheckedChanged += HandleThemeModeChanged;
        _compactThemeCheckBox.IsCheckedChanged += HandleThemeModeChanged;
        _dynamicDisplayTimer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };
        _dynamicDisplayTimer.Tick += HandleDynamicDisplayTimerTick;
        _glowWorkbench = new GlowWorkbench();
        Closed += HandleClosed;
        UpdateDynamicDisplays();
        _dynamicDisplayTimer.Start();

        Title   = "AtomUI Labs Dashboard Sample";
        Width   = 960;
        Height  = 640;
        AddThemeAwareStyles();
        Content = new ScrollViewer
        {
            Classes = { "sample-surface" },
            ClipToBounds                  = true,
            VerticalScrollBarVisibility   = ScrollBarVisibility.Visible,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
            Content = new StackPanel
            {
                Margin      = new Thickness(24),
                Spacing     = 18,
                Orientation = Orientation.Vertical,
                Children =
                {
                    new Dashboard(),
                    CreateGroupLabel("Theme"),
                    new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Spacing = 16,
                        Children =
                        {
                            _darkThemeCheckBox,
                            _compactThemeCheckBox
                        }
                    },
                    CreateGroupLabel("Matrix MVP"),
                    CreateSectionLabel("Matrix - default themed values"),
                    new MatrixDisplay
                    {
                        Text = "MATRIX 5X7"
                    },
                    CreateSectionLabel("Matrix - lowercase input and numbers"),
                    new MatrixDisplay
                    {
                        Text             = "labs 2026",
                        DotSize          = 7,
                        DotSpacing       = 2,
                        CharacterSpacing = 9
                    },
                    CreateSectionLabel("Matrix - supported symbols"),
                    new MatrixDisplay
                    {
                        Text             = "?-.:_+=/",
                        DotSize          = 7,
                        DotSpacing       = 2,
                        CharacterSpacing = 8,
                        Padding          = new Thickness(14),
                        Background       = new SolidColorBrush(Color.FromRgb(10, 15, 20)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(95, 236, 255)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(38, 95, 236, 255)),
                        CornerRadius     = new CornerRadius(8)
                    },
                    CreateGroupLabel("Matrix Dot Shape"),
                    CreateSectionLabel("Matrix - circle dots"),
                    CreateMatrixShapeSample(MatrixDotShape.Circle),
                    CreateSectionLabel("Matrix - square dots"),
                    CreateMatrixShapeSample(MatrixDotShape.Square),
                    CreateSectionLabel("Matrix - rounded square dots, subtle corners"),
                    CreateMatrixShapeSample(MatrixDotShape.RoundedSquare, 0.1),
                    CreateSectionLabel("Matrix - rounded square dots, soft corners"),
                    CreateMatrixShapeSample(MatrixDotShape.RoundedSquare, 0.35),
                    CreateGroupLabel("Matrix Panel Border"),
                    CreateSectionLabel("Matrix - no border (default)"),
                    CreateMatrixBorderSample(null, default),
                    CreateSectionLabel("Matrix - uniform rounded border"),
                    CreateMatrixBorderSample(
                        new SolidColorBrush(Color.FromRgb(74, 157, 181)),
                        new Thickness(2)),
                    CreateSectionLabel("Matrix - asymmetric rounded border"),
                    CreateMatrixBorderSample(
                        new SolidColorBrush(Color.FromRgb(255, 190, 92)),
                        new Thickness(2, 4, 6, 8)),
                    CreateSectionLabel("Matrix - translucent border over background"),
                    CreateMatrixBorderSample(
                        new SolidColorBrush(Color.FromArgb(120, 133, 232, 255)),
                        new Thickness(5)),
                    CreateGroupLabel("Matrix Glow"),
                    _glowWorkbench,
                    CreateSectionLabel("Matrix - glow disabled (default)"),
                    CreateMatrixGlowSample(null, 6, 0.35),
                    CreateSectionLabel("Matrix - subtle cyan glow"),
                    CreateMatrixGlowSample(new SolidColorBrush(Color.FromRgb(74, 222, 255)), 6, 0.35),
                    CreateSectionLabel("Matrix - strong magenta glow"),
                    CreateMatrixGlowSample(new SolidColorBrush(Color.FromRgb(255, 76, 210)), 12, 0.55),
                    CreateSectionLabel("Matrix - maximum supported glow radius"),
                    CreateMatrixGlowSample(new SolidColorBrush(Color.FromRgb(105, 255, 168)), 24, 0.4),
                    CreateSectionLabel("Matrix - unknown character fallback"),
                    new MatrixDisplay
                    {
                        Text             = "A中Z",
                        DotSize          = 8,
                        DotSpacing       = 2,
                        CharacterSpacing = 10,
                        Padding          = new Thickness(14)
                    },
                    CreateSectionLabel("Matrix - inactive dots disabled"),
                    new MatrixDisplay
                    {
                        Text             = "CLEAN",
                        DotSize          = 7,
                        DotSpacing       = 2,
                        CharacterSpacing = 9,
                        ShowInactiveDots = false,
                        Padding          = new Thickness(12),
                        Background       = new SolidColorBrush(Color.FromRgb(18, 18, 22)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(255, 203, 82)),
                        CornerRadius     = new CornerRadius(8)
                    },
                    CreateSectionLabel("Matrix - centered in a larger panel"),
                    new MatrixDisplay
                    {
                        Text                       = "CENTER",
                        Width                      = 640,
                        Height                     = 130,
                        DotSize                    = 7,
                        DotSpacing                 = 2,
                        CharacterSpacing           = 10,
                        Padding                    = new Thickness(14),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment   = VerticalAlignment.Center,
                        Background                 = new SolidColorBrush(Color.FromRgb(12, 22, 18)),
                        ActiveBrush                = new SolidColorBrush(Color.FromRgb(103, 242, 163)),
                        InactiveBrush              = new SolidColorBrush(Color.FromArgb(34, 103, 242, 163)),
                        CornerRadius               = new CornerRadius(8)
                    },
                    CreateSectionLabel("Matrix - scale down when space is constrained"),
                    new MatrixDisplay
                    {
                        Text                       = "1234567890",
                        Width                      = 280,
                        Height                     = 72,
                        DotSize                    = 8,
                        DotSpacing                 = 2,
                        CharacterSpacing           = 10,
                        Padding                    = new Thickness(12),
                        OverflowMode               = MatrixOverflowMode.ScaleDown,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment   = VerticalAlignment.Center,
                        Background                 = new SolidColorBrush(Color.FromRgb(24, 15, 12)),
                        ActiveBrush                = new SolidColorBrush(Color.FromRgb(255, 142, 91)),
                        InactiveBrush              = new SolidColorBrush(Color.FromArgb(34, 255, 142, 91)),
                        CornerRadius               = new CornerRadius(8)
                    },
                    CreateSectionLabel("Matrix - long text clipped to viewport"),
                    new MatrixDisplay
                    {
                        Text             = "CLIPPED MATRIX CONTENT 0123456789",
                        Width            = 280,
                        Height           = 72,
                        DotSize          = 7,
                        DotSpacing       = 2,
                        CharacterSpacing = 9,
                        Padding          = new Thickness(12),
                        OverflowMode     = MatrixOverflowMode.Clip,
                        Background       = new SolidColorBrush(Color.FromRgb(17, 17, 23)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(180, 165, 255)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(32, 180, 165, 255)),
                        CornerRadius     = new CornerRadius(8)
                    },
                    CreateGroupLabel("Matrix Marquee"),
                    CreateSectionLabel("Matrix - short text through-screen marquee"),
                    CreateMatrixMarqueeSample("HELLO", 48, TimeSpan.FromMilliseconds(500)),
                    CreateSectionLabel("Matrix - long announcement marquee"),
                    CreateMatrixMarqueeSample(
                        "ATOMUI LABS MATRIX MARQUEE 2026",
                        72,
                        TimeSpan.FromMilliseconds(750)),
                    CreateSectionLabel("Matrix - marquee with glow"),
                    CreateMatrixMarqueeSample(
                        "GLOW MOVES WITH MARQUEE",
                        56,
                        TimeSpan.FromMilliseconds(500),
                        new SolidColorBrush(Color.FromRgb(255, 48, 48))),
                    CreateSectionLabel("Matrix - scale down in an extremely small viewport"),
                    new MatrixDisplay
                    {
                        Text                       = "TINY SPACE",
                        Width                      = 120,
                        Height                     = 32,
                        DotSize                    = 8,
                        DotSpacing                 = 2,
                        CharacterSpacing           = 9,
                        Padding                    = new Thickness(8),
                        OverflowMode               = MatrixOverflowMode.ScaleDown,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment   = VerticalAlignment.Center,
                        Background                 = new SolidColorBrush(Color.FromRgb(22, 16, 9)),
                        ActiveBrush                = new SolidColorBrush(Color.FromRgb(255, 210, 96)),
                        InactiveBrush              = null,
                        ShowInactiveDots           = false,
                        CornerRadius               = new CornerRadius(6)
                    },
                    CreateGroupLabel("Dynamic"),
                    CreateSectionLabel("Matrix - dynamic counter"),
                    _matrixCounterDisplay,
                    CreateSectionLabel("Segment - dynamic clock"),
                    _clockDisplay,
                    CreateSectionLabel("Segment - dynamic counter"),
                    _counterDisplay,
                    CreateGroupLabel("Basic"),
                    CreateSectionLabel("Segment - default themed values"),
                    new SegmentDisplay
                    {
                        Text             = "12:45",
                        CharacterHeight  = 70,
                        SegmentThickness = 7,
                        SegmentGap       = 3,
                        CharacterSpacing = 9
                    },
                    CreateSectionLabel("Segment - custom panel and inactive segments"),
                    new SegmentDisplay
                    {
                        Text             = "HELLO",
                        CharacterHeight  = 86,
                        SegmentThickness = 11,
                        SegmentGap       = 3,
                        CharacterSpacing = 12,
                        Background       = new SolidColorBrush(Color.FromRgb(10, 16, 18)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(91, 255, 126)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(42, 91, 255, 126)),
                        CornerRadius     = new CornerRadius(12),
                        Padding          = new Thickness(20)
                    },
                    CreateSectionLabel("Segment - symbols and letters"),
                    new SegmentDisplay
                    {
                        Text             = "A-01",
                        CharacterHeight  = 72,
                        SegmentThickness = 8,
                        SegmentGap       = 2,
                        Background       = new SolidColorBrush(Color.FromRgb(18, 18, 24)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(255, 78, 78)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(38, 255, 78, 78)),
                        CornerRadius     = new CornerRadius(10),
                        Padding          = new Thickness(18)
                    },
                    CreateGroupLabel("Shape"),
                    CreateSectionLabel("Segment - shape tuning"),
                    new SegmentDisplay
                    {
                        Text              = "3.14",
                        CharacterHeight   = 72,
                        SegmentThickness  = 10,
                        SegmentGap        = 2,
                        SegmentBevelRatio = 0.15,
                        DotScale          = 0.48,
                        Background        = new SolidColorBrush(Color.FromRgb(18, 20, 22)),
                        ActiveBrush       = new SolidColorBrush(Color.FromRgb(183, 255, 103)),
                        InactiveBrush     = new SolidColorBrush(Color.FromArgb(36, 183, 255, 103)),
                        CornerRadius      = new CornerRadius(10),
                        Padding           = new Thickness(18)
                    },
                    CreateSectionLabel("Segment - minimal glow layer"),
                    new SegmentDisplay
                    {
                        Text             = "GLOW",
                        CharacterHeight  = 78,
                        SegmentThickness = 9,
                        SegmentGap       = 2,
                        CharacterSpacing = 10,
                        Background       = new SolidColorBrush(Color.FromRgb(6, 8, 13)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(104, 221, 255)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(28, 104, 221, 255)),
                        GlowBrush        = new SolidColorBrush(Color.FromRgb(104, 221, 255)),
                        GlowOpacity      = 0.28,
                        GlowRadius       = 6,
                        CornerRadius     = new CornerRadius(10),
                        Padding          = new Thickness(18)
                    },
                    CreateSectionLabel("Segment - pronounced magenta glow"),
                    CreateSegmentGlowSample(new SolidColorBrush(Color.FromRgb(255, 74, 206)), 12, 0.52),
                    CreateSectionLabel("Segment - maximum supported glow radius"),
                    CreateSegmentGlowSample(new SolidColorBrush(Color.FromRgb(255, 190, 82)), 24, 0.4),
                    CreateGroupLabel("Layout"),
                    CreateSectionLabel("Segment - centered in a larger panel"),
                    new SegmentDisplay
                    {
                        Text                       = "CENTER",
                        Width                      = 560,
                        Height                     = 150,
                        CharacterHeight            = 74,
                        SegmentThickness           = 8,
                        SegmentGap                 = 2,
                        CharacterSpacing           = 8,
                        Background                 = new SolidColorBrush(Color.FromRgb(12, 18, 23)),
                        ActiveBrush                = new SolidColorBrush(Color.FromRgb(106, 236, 177)),
                        InactiveBrush              = new SolidColorBrush(Color.FromArgb(28, 106, 236, 177)),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment   = VerticalAlignment.Center,
                        CornerRadius               = new CornerRadius(10),
                        Padding                    = new Thickness(18)
                    },
                    CreateSectionLabel("Segment - scale down when space is constrained"),
                    new SegmentDisplay
                    {
                        Text                       = "1234567890",
                        Width                      = 280,
                        Height                     = 74,
                        CharacterHeight            = 72,
                        SegmentThickness           = 7,
                        SegmentGap                 = 2,
                        CharacterSpacing           = 8,
                        OverflowMode               = SegmentOverflowMode.ScaleDown,
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment   = VerticalAlignment.Center,
                        Background                 = new SolidColorBrush(Color.FromRgb(24, 18, 12)),
                        ActiveBrush                = new SolidColorBrush(Color.FromRgb(255, 185, 92)),
                        InactiveBrush              = new SolidColorBrush(Color.FromArgb(32, 255, 185, 92)),
                        CornerRadius               = new CornerRadius(10),
                        Padding                    = new Thickness(12)
                    },
                    CreateGroupLabel("Size"),
                    CreateSectionLabel("Segment - inactive segments disabled"),
                    new SegmentDisplay
                    {
                        Text                 = "88.8",
                        CharacterHeight      = 64,
                        SegmentThickness     = 6,
                        SegmentGap           = 2,
                        CharacterSpacing     = 8,
                        ShowInactiveSegments = false
                    },
                    CreateSectionLabel("Segment - small size"),
                    new SegmentDisplay
                    {
                        Text             = "LAB",
                        CharacterHeight  = 34,
                        SegmentThickness = 4,
                        SegmentGap       = 1,
                        CharacterSpacing = 5,
                        Padding          = new Thickness(8)
                    },
                    CreateSectionLabel("Segment - thick segments and large gap"),
                    new SegmentDisplay
                    {
                        Text             = "2026",
                        CharacterHeight  = 78,
                        SegmentThickness = 18,
                        SegmentGap       = 8,
                        CharacterSpacing = 10,
                        Background       = new SolidColorBrush(Color.FromRgb(12, 13, 17)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(92, 182, 255)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(34, 92, 182, 255)),
                        CornerRadius     = new CornerRadius(10),
                        Padding          = new Thickness(18)
                    },
                    CreateSectionLabel("Segment - compact size"),
                    new SegmentDisplay
                    {
                        Text             = "MINI",
                        CharacterHeight  = 28,
                        CharacterSpacing = 4,
                        SegmentThickness = 3,
                        SegmentGap       = 1,
                        Padding          = new Thickness(6),
                        Background       = new SolidColorBrush(Color.FromRgb(245, 247, 250)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(40, 40, 46)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(32, 40, 40, 46)),
                        CornerRadius     = new CornerRadius(6)
                    },
                    CreateSectionLabel("Segment - large size"),
                    new SegmentDisplay
                    {
                        Text             = "BIG",
                        CharacterHeight  = 116,
                        CharacterSpacing = 18,
                        SegmentThickness = 15,
                        SegmentGap       = 4,
                        Padding          = new Thickness(24),
                        Background       = new SolidColorBrush(Color.FromRgb(8, 10, 14)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(255, 222, 89)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(36, 255, 222, 89)),
                        CornerRadius     = new CornerRadius(14)
                    },
                    CreateGroupLabel("Color"),
                    CreateSectionLabel("Segment - warm foreground and dark background"),
                    new SegmentDisplay
                    {
                        Text             = "SAFE",
                        CharacterHeight  = 72,
                        CharacterSpacing = 10,
                        SegmentThickness = 9,
                        SegmentGap       = 2,
                        Padding          = new Thickness(18),
                        Background       = new SolidColorBrush(Color.FromRgb(24, 14, 10)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(255, 126, 71)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(42, 255, 126, 71)),
                        CornerRadius     = new CornerRadius(10)
                    },
                    CreateSectionLabel("Segment - cool foreground and light background"),
                    new SegmentDisplay
                    {
                        Text             = "COOL",
                        CharacterHeight  = 72,
                        CharacterSpacing = 10,
                        SegmentThickness = 9,
                        SegmentGap       = 2,
                        Padding          = new Thickness(18),
                        Background       = new SolidColorBrush(Color.FromRgb(236, 246, 248)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(0, 128, 168)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(42, 0, 128, 168)),
                        CornerRadius     = new CornerRadius(10)
                    },
                    CreateSectionLabel("Segment - transparent background"),
                    new SegmentDisplay
                    {
                        Text             = "OPEN",
                        CharacterHeight  = 68,
                        CharacterSpacing = 9,
                        SegmentThickness = 8,
                        SegmentGap       = 2,
                        Background       = Brushes.Transparent,
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(128, 70, 255)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(34, 128, 70, 255)),
                        Padding          = new Thickness(4)
                    },
                    CreateGroupLabel("Symbols"),
                    CreateSectionLabel("Segment - symbols"),
                    new SegmentDisplay
                    {
                        Text             = "-_:.",
                        CharacterHeight  = 70,
                        CharacterSpacing = 10,
                        SegmentThickness = 8,
                        SegmentGap       = 2,
                        Padding          = new Thickness(14),
                        Background       = new SolidColorBrush(Color.FromRgb(15, 18, 24)),
                        ActiveBrush      = new SolidColorBrush(Color.FromRgb(117, 255, 209)),
                        InactiveBrush    = new SolidColorBrush(Color.FromArgb(32, 117, 255, 209)),
                        CornerRadius     = new CornerRadius(8)
                    }
                }
            }
        };
    }

    private static TextBlock CreateGroupLabel(string text)
    {
        var label = new TextBlock
        {
            Text       = text,
            FontSize   = 18,
            FontWeight = FontWeight.Bold,
            Margin     = new Thickness(0, 12, 0, 0)
        };
        label.Classes.Add("sample-group-label");
        return label;
    }

    private static MatrixDisplay CreateMatrixShapeSample(
        MatrixDotShape dotShape,
        double dotCornerRadiusRatio = 0.25)
    {
        return new MatrixDisplay
        {
            Text                 = "SHAPE 2026",
            DotSize              = 8,
            DotSpacing           = 3,
            DotShape             = dotShape,
            DotCornerRadiusRatio = dotCornerRadiusRatio,
            CharacterSpacing     = 10,
            Padding              = new Thickness(14),
            Background           = new SolidColorBrush(Color.FromRgb(9, 16, 22)),
            ActiveBrush          = new SolidColorBrush(Color.FromRgb(103, 220, 255)),
            InactiveBrush        = new SolidColorBrush(Color.FromArgb(36, 103, 220, 255)),
            CornerRadius         = new CornerRadius(8)
        };
    }

    private static MatrixDisplay CreateMatrixGlowSample(IBrush? glowBrush, double radius, double opacity)
    {
        return new MatrixDisplay
        {
            Text             = "GLOW 2026",
            DotSize          = 8,
            DotSpacing       = 3,
            CharacterSpacing = 10,
            Padding          = new Thickness(24),
            Background       = new SolidColorBrush(Color.FromRgb(6, 10, 14)),
            ActiveBrush      = new SolidColorBrush(Color.FromRgb(238, 250, 255)),
            InactiveBrush    = new SolidColorBrush(Color.FromArgb(28, 112, 196, 218)),
            GlowBrush        = glowBrush,
            GlowRadius       = radius,
            GlowOpacity      = opacity,
            CornerRadius     = new CornerRadius(8)
        };
    }

    private static MatrixDisplay CreateMatrixMarqueeSample(
        string text,
        double speed,
        TimeSpan repeatDelay,
        IBrush? glowBrush = null)
    {
        return new MatrixDisplay
        {
            Text                     = text,
            Width                    = 640,
            Height                   = 112,
            DotSize                  = 7,
            DotSpacing               = 2,
            CharacterSpacing         = 9,
            Padding                  = new Thickness(14),
            IsMarqueeEnabled         = true,
            MarqueeSpeed             = speed,
            MarqueeRepeatDelay       = repeatDelay,
            Background               = new SolidColorBrush(Color.FromRgb(6, 10, 14)),
            ActiveBrush              = glowBrush ?? new SolidColorBrush(Color.FromRgb(95, 236, 255)),
            InactiveBrush            = new SolidColorBrush(Color.FromArgb(28, 110, 138, 148)),
            GlowBrush                = glowBrush,
            GlowOpacity              = 0.55,
            GlowRadius               = 8,
            CornerRadius             = new CornerRadius(8),
            VerticalContentAlignment = VerticalAlignment.Center
        };
    }

    private static SegmentDisplay CreateSegmentGlowSample(IBrush glowBrush, double radius, double opacity)
    {
        return new SegmentDisplay
        {
            Text                 = "88:88",
            CharacterHeight      = 78,
            SegmentThickness     = 9,
            SegmentGap           = 2,
            CharacterSpacing     = 10,
            Padding              = new Thickness(24),
            Background           = new SolidColorBrush(Color.FromRgb(6, 8, 13)),
            ActiveBrush          = new SolidColorBrush(Color.FromRgb(248, 250, 255)),
            InactiveBrush        = new SolidColorBrush(Color.FromArgb(28, 126, 144, 156)),
            ShowInactiveSegments = true,
            GlowBrush            = glowBrush,
            GlowRadius           = radius,
            GlowOpacity          = opacity,
            CornerRadius         = new CornerRadius(10)
        };
    }

    private static MatrixDisplay CreateMatrixBorderSample(
        IBrush? borderBrush,
        Thickness borderThickness)
    {
        return new MatrixDisplay
        {
            Text                       = "BORDER 2026",
            Width                      = 560,
            Height                     = 112,
            DotSize                    = 7,
            DotSpacing                 = 2,
            CharacterSpacing           = 9,
            Padding                    = new Thickness(14),
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment   = VerticalAlignment.Center,
            Background                 = new SolidColorBrush(Color.FromRgb(13, 20, 25)),
            BorderBrush                = borderBrush,
            BorderThickness            = borderThickness,
            ActiveBrush                = new SolidColorBrush(Color.FromRgb(112, 229, 255)),
            InactiveBrush              = new SolidColorBrush(Color.FromArgb(34, 112, 229, 255)),
            CornerRadius               = new CornerRadius(12)
        };
    }

    private static CheckBox CreateThemeCheckBox(string content)
    {
        var checkBox = new CheckBox
        {
            Content = content,
            VerticalAlignment = VerticalAlignment.Center
        };
        checkBox.Classes.Add("sample-theme-toggle");
        return checkBox;
    }

    private void AddThemeAwareStyles()
    {
        var surfaceStyle = new Style(selector =>
            selector.OfType<ScrollViewer>().Class("sample-surface"));
        surfaceStyle.Add(ScrollViewer.BackgroundProperty, SharedTokenKind.ColorBgLayout);
        Styles.Add(surfaceStyle);

        var groupLabelStyle = new Style(selector =>
            selector.OfType<TextBlock>().Class("sample-group-label"));
        groupLabelStyle.Add(TextBlock.ForegroundProperty, SharedTokenKind.ColorText);
        Styles.Add(groupLabelStyle);

        var sectionLabelStyle = new Style(selector =>
            selector.OfType<TextBlock>().Class("sample-section-label"));
        sectionLabelStyle.Add(TextBlock.ForegroundProperty, SharedTokenKind.ColorTextSecondary);
        Styles.Add(sectionLabelStyle);

        var themeToggleStyle = new Style(selector =>
            selector.OfType<CheckBox>().Class("sample-theme-toggle"));
        themeToggleStyle.Add(CheckBox.ForegroundProperty, SharedTokenKind.ColorText);
        Styles.Add(themeToggleStyle);
    }

    private void HandleThemeModeChanged(object? sender, RoutedEventArgs e)
    {
        var variantName = IThemeManager.DEFAULT_THEME_ID;
        if (_darkThemeCheckBox.IsChecked == true)
        {
            variantName += "-Dark";
        }
        if (_compactThemeCheckBox.IsChecked == true)
        {
            variantName += "-Compact";
        }

        if (Application.Current is { } application)
        {
            application.RequestedThemeVariant = new ThemeVariant(variantName, null);
        }
    }

    private void HandleDynamicDisplayTimerTick(object? sender, EventArgs e)
    {
        UpdateDynamicDisplays();
    }

    private void HandleClosed(object? sender, EventArgs e)
    {
        _dynamicDisplayTimer.Stop();
        _dynamicDisplayTimer.Tick -= HandleDynamicDisplayTimerTick;
        _glowWorkbench.Dispose();
        _darkThemeCheckBox.IsCheckedChanged -= HandleThemeModeChanged;
        _compactThemeCheckBox.IsCheckedChanged -= HandleThemeModeChanged;
        Closed -= HandleClosed;
    }

    private void UpdateDynamicDisplays()
    {
        var now = DateTime.Now;
        _clockDisplay.Text = $"{now:HH}:{now:mm}:{now:ss}";
        _counterDisplay.Text = _counterValue.ToString("D4");
        _matrixCounterDisplay.Text = _counterValue.ToString("D6");

        _counterValue = (_counterValue + 1) % 10000;
    }

    private static TextBlock CreateSectionLabel(string text)
    {
        var label = new TextBlock
        {
            Text       = text,
            FontSize   = 13,
            FontWeight = FontWeight.SemiBold
        };
        label.Classes.Add("sample-section-label");
        return label;
    }
}
