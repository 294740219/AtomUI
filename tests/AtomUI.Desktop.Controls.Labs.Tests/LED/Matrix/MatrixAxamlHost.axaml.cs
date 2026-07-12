using AtomUI.Desktop.Controls.Labs.LED.Matrix;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AtomUI.Desktop.Controls.Labs.Tests.LED.Matrix;

internal partial class MatrixAxamlHost : UserControl
{
    public MatrixAxamlHost()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public MatrixDisplay Display => this.FindControl<MatrixDisplay>("PART_Matrix")!;
}
