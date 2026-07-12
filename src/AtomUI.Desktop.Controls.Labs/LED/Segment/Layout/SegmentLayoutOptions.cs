using Avalonia;

namespace AtomUI.Desktop.Controls.Labs.LED.Segment.Layout;

internal readonly record struct SegmentLayoutOptions(
    double CharacterHeight,
    double CharacterAspectRatio,
    double CharacterSpacing,
    Thickness Padding);
