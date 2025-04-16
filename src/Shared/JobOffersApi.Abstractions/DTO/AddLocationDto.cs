namespace JobOffersApi.Abstractions.DTO;

public class AddLocationDto
{
    public string Country { get; init; }
    public string City { get; init; }
    public string? Street { get; init; }
    public string HouseNumber { get; init; }
    public string? ApartmentNumber { get; init; }
    public string? PostalCode { get; init; }
}
