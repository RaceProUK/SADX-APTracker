namespace RPS.SADX.PopTracker.Generator.Utilities;

internal static class Common
{
    internal static string RemoveWhitespace(string s)
        => string.Join(string.Empty, s.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
}
