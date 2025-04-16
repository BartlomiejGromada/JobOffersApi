using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.Companies.Core.DTO.Companies;

namespace JobOffersApi.Modules.Companies.Application.Commands.UpdateCompanyCommand;

internal class UpdateCompanyCommand : ICommand
{
    public Guid CompanyId { get; init; }
    public UpdateCompanyDto Dto { get; init; }
}
