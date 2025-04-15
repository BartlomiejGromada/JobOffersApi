using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Application.Commands;

internal class AddCompanyCommand : ICommand
{
    
    public string Name { get; init; }
    public string Description { get; init; }
}
