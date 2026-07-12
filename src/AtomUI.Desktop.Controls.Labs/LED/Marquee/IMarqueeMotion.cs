namespace AtomUI.Desktop.Controls.Labs.LED.Marquee;

internal interface IMarqueeMotion
{
    MarqueeRenderPlan Calculate(in MarqueeMotionContext context);
}
