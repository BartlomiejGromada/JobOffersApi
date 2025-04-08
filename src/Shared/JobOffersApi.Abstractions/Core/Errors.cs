namespace JobOffersApi.Abstractions.Core;

public static class Errors
{
    public const string Required = "field-is-required";
    public const string InvalidValue = "invalid-value";
    public const string DateCantBePast = "date-cannot-be-past";
    public const string DateCantBeFuture = "date-cannot-be-future";
    public static string GreaterThen(double value) 
        => $"field-must-be-greater-than-{value}";
    public static string MaxLengthExceeded(int len) 
        => $"maximum-length-exceeded-{len}";
    public static string MustBeEqual(string field)
    => $"field-must-be-equals-to-field-{field}";

    public static string MinLength(int len)
        => $"minimum-length-is-{len}";

    public const string ContainsUpperCase = "must-contain-at-least-one-uppercase-letter";
    public const string ContainsLowerCase = "must-contain-at-least-one-lowercase-letter";
    public const string ContainsOneDigit = "must-contain-at-least-one-digit";
    public const string ContainsOneSpecialCharacter = "must-contain-at-least-one-special-character";

}
