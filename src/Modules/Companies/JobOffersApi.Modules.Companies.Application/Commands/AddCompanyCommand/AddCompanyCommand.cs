using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Abstractions.DTO;

namespace JobOffersApi.Modules.Companies.Application.Commands.AddCompanyCommand;

internal class AddCompanyCommand : ICommand
{

    public string Name { get; init; }
    public string Description { get; init; }
    public AddLocationDto Location { get; init; }
}
