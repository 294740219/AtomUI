using Avalonia;
using AtomUI.Desktop.Controls.Labs.LED.Segment.Character;

namespace AtomUI.Desktop.Controls.Labs.LED.Segment.Layout;

internal readonly record struct SegmentCharacterSlot(
    SegmentCharacterPattern Pattern,
    Rect Bounds);
