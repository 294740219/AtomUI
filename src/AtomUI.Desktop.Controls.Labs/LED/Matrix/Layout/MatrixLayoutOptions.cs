using Avalonia;

namespace AtomUI.Desktop.Controls.Labs.LED.Matrix.Layout;

internal readonly record struct MatrixLayoutOptions(
    double DotSize,
    double DotSpacing,
    double CharacterSpacing,
    Thickness Padding);
