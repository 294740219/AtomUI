using Avalonia.Media;

namespace AtomUI.Desktop.Controls.Labs.LED.Segment.Rendering;

internal sealed record SegmentVisibleGeometry(
    Geometry? ActiveGeometry,
    Geometry? InactiveGeometry);
