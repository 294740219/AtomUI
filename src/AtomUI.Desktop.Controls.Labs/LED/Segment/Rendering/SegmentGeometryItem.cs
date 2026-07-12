using AtomUI.Desktop.Controls.Labs.LED.Segment.Character;
using Avalonia.Media;

namespace AtomUI.Desktop.Controls.Labs.LED.Segment.Rendering;

internal readonly record struct SegmentGeometryItem(
    SegmentParts Part,
    Geometry Geometry);
