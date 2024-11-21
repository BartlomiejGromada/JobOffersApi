using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Exceptions;
using System.Collections.Generic;

namespace JobOffersApi.Modules.JobOffers.Core.Entities;

public class Location : ValueObject
{
    public string Country { get; private set; }
    public string City { get; private set; }
    public string? Street { get; private set; }
    public string HouseNumber { get; private set; }
    public string? ApartmentNumber { get; private set; }
    public string? PostalCode { get; private set; }

    private Location() { }

    public Location(
        string country,
        string city, 
        string houseNumber,
        string? street = null,
        string? apartmentNumber = null,
        string? postalCode = null)
    {
        if (string.IsNullOrWhiteSpace(country))
            throw new InvalidLocationException("Country cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(city))
            throw new InvalidLocationException("City cannot be null or empty.");
        if (string.IsNullOrWhiteSpace(houseNumber))
            throw new InvalidLocationException("House number cannot be null or empty.");

        Country = country;
        City = city;
        Street = street;
        HouseNumber = houseNumber;
        ApartmentNumber = apartmentNumber;
        PostalCode = postalCode;
    }

    protected override IEnumerable<object?> GetAtomicValues()
    {
        yield return Country;
        yield return City;
        yield return Street;
        yield return HouseNumber;
        yield return ApartmentNumber;
        yield return PostalCode;
    }
}
