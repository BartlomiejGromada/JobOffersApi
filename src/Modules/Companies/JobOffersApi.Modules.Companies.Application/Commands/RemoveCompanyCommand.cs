using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Application.Commands;

internal class RemoveCompanyCommand : ICommand
{
    public Guid Id { get; init; }
}
