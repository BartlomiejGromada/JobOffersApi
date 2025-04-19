using JobOffersApi.Abstractions.Commands;
using JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

namespace JobOffersApi.Modules.JobOffers.Application.Commands.UpdateJobOfferCommand;

internal class UpdateJobOfferCommand : ICommand
{
    public UpdateJobOfferCommand(Guid jobOfferId, UpdateJobOfferDto dto)
    {
        JobOfferId = jobOfferId;
        Dto = dto;
    }

    public Guid JobOfferId { get; init; }
    public UpdateJobOfferDto Dto { get; init; }
}
