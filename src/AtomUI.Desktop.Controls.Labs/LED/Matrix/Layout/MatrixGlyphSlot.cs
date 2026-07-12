using AtomUI.Desktop.Controls.Labs.LED.Matrix.Character;
using Avalonia;

namespace AtomUI.Desktop.Controls.Labs.LED.Matrix.Layout;

internal readonly record struct MatrixGlyphSlot(
    MatrixCharacterPattern Pattern,
    Point Origin);
