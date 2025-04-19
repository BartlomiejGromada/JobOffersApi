using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.AddJobOfferCommand;

internal class AddJobOfferCommand : ICommand
{
    public AddJobOfferDto Dto { get; init; }
}
