namespace JobOffersApi.Modules.Companies.Core.DTO.Employers;

internal class EmployerDto
{
    public Guid Id { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public DateOnly DateOfBirth { get; init; }
    public DateTimeOffset CreatedDate { get; init; }
}
