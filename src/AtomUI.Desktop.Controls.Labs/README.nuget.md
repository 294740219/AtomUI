## AtomUI Desktop Controls Labs

AtomUI Desktop Controls Labs contains experimental Avalonia desktop controls built on AtomUI infrastructure.

Labs controls:

- do not guarantee Ant Design visual compliance;
- do not depend on AtomUI formed control packages;
- may reuse infrastructure such as AtomUI.Core themes, Shared Tokens and source generators;
- can evolve before their contracts are promoted to stable packages.

Current controls:

- `Dashboard`: minimal package and theme-registration verification control;
- `SegmentDisplay`: fourteen-segment LED text display;
- `MatrixDisplay`: fixed 5x7 dot-matrix LED text display.

### Install

```bash
dotnet add package AtomUI.Desktop.Controls.Labs
```

The package depends on AtomUI.Core and Avalonia. Installing AtomUI.Desktop.Controls is not required.

### Application Setup

```csharp
this.UseAtomUI(builder => builder.UseDesktopLabs());
```

### AXAML

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:labs="https://atomui.net/labs">
    <StackPanel Spacing="12">
        <labs:SegmentDisplay Text="12:45" />
        <labs:MatrixDisplay Text="MATRIX 2026" />
    </StackPanel>
</Window>
```

### License

AtomUI Desktop Controls Labs follows the repository license.
