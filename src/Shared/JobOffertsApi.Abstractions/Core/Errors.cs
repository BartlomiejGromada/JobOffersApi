namespace JobOffersApi.Abstractions.Core;

public static class Errors
{
    public const string Required = "field-is-required";
    public const string InvalidValue = "invalid-value";
    public const string DateCantBePast = "date-cannot-be-past";
    public static string GreaterThen(double value) 
        => $"field-must-be-greater-than-{value}";
    public static string MaxLengthExceeded(int len) 
        => $"maximum-length-exceeded-{len}";
}
