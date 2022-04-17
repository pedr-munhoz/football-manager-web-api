namespace api.Infrastructure.Extensions;

public static class IdExtensions
{
    public static long? ToLongId(this string value)
    {
        bool success = Int64.TryParse(value, out long number);

        if (!success)
            return null;

        return number;
    }

    public static string ToStringId(this long value)
    {
        return value.ToString();
    }
}

