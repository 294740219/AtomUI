using AtomUI.Desktop.Controls.Labs.LED.Marquee;
using Shouldly;
using Xunit;

namespace AtomUI.Desktop.Controls.Labs.Tests.LED.Matrix;

public class MatrixMarqueeValueSanitizerTests
{
    [Theory]
    [InlineData(double.NaN, 0)]
    [InlineData(double.NegativeInfinity, 0)]
    [InlineData(-1, 0)]
    [InlineData(0, 0)]
    [InlineData(48, 48)]
    [InlineData(10_001, 10_000)]
    [InlineData(double.PositiveInfinity, 10_000)]
    public void Speed_ShouldUseSafeEffectiveRange(double value, double expected)
    {
        LEDMarqueeValueSanitizer.CoerceSpeed(value).ShouldBe(expected);
    }

    [Fact]
    public void RepeatDelay_ShouldUseSafeEffectiveRange()
    {
        LEDMarqueeValueSanitizer.CoerceRepeatDelay(TimeSpan.FromSeconds(-1)).ShouldBe(TimeSpan.Zero);
        LEDMarqueeValueSanitizer.CoerceRepeatDelay(TimeSpan.FromSeconds(5)).ShouldBe(TimeSpan.FromSeconds(5));
        LEDMarqueeValueSanitizer.CoerceRepeatDelay(TimeSpan.FromHours(1)).ShouldBe(TimeSpan.FromMinutes(1));
    }
}
