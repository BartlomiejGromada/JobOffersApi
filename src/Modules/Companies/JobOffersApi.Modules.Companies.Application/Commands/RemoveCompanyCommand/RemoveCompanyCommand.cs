using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Application.Commands.RemoveCompanyCommand;

internal class RemoveCompanyCommand : ICommand
{
    public Guid CompanyId { get; init; }
}
