namespace JobOffersApi.Modules.Companies.Core.DTO.Employers;

internal class AddEmployerToCompanyDto
{
    public Guid UserId { get; init; }
    public string Position { get; init; }
}
