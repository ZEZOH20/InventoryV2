namespace InventoryV2.Shares;

public static class StringExtension
{
    public static string? Truncate(this string value, int maxLenght)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLenght ? value : value.Substring(0, maxLenght);
    }
}