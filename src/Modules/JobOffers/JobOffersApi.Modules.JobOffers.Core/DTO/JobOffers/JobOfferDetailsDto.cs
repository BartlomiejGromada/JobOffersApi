using JobOffersApi.Modules.JobOffers.Core.Entities.ValueObjects;
using JobOffersApi.Modules.JobOffers.Core.Entities;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

internal class JobOfferDetailsDto : JobOfferDto
{
    public string DescriptionHtml { get; set; }
}
