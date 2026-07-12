using AtomUI.Desktop.Controls.Labs.LED;
using Shouldly;
using Xunit;

namespace AtomUI.Desktop.Controls.Labs.Tests.LED;

public class LEDCharacterNormalizerTests
{
    [Theory]
    [InlineData('a', 'A')]
    [InlineData('z', 'Z')]
    [InlineData('m', 'M')]
    [InlineData('A', 'A')]
    [InlineData('0', '0')]
    [InlineData(':', ':')]
    [InlineData(' ', ' ')]
    public void NormalizeAscii_ShouldOnlyUppercaseAsciiLowercaseLetters(char input, char expected)
    {
        LEDCharacterNormalizer.NormalizeAscii(input).ShouldBe(expected);
    }
}
