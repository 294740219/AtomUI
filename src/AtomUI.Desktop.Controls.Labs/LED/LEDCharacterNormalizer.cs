namespace AtomUI.Desktop.Controls.Labs.LED;

internal static class LEDCharacterNormalizer
{
    public static char NormalizeAscii(char character)
    {
        return character is >= 'a' and <= 'z'
            ? (char)(character - ('a' - 'A'))
            : character;
    }
}
