using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Application.Commands;

internal class AddEmployerToCompanyCommand : ICommand
{
    public AddEmployerToCompanyCommand(
        Guid companyId,
        Guid userId, 
        string position)
    {
        CompanyId = companyId;
        UserId = userId;
        Position = position;
    }

    public Guid CompanyId { get; init; }
    public Guid UserId { get; init; }
    public string Position { get; init; }
}
