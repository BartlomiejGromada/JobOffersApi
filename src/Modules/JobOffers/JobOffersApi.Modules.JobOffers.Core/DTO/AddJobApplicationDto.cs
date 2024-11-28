using Microsoft.AspNetCore.Http;

namespace JobOffersApi.Modules.JobOffers.Core.DTO;

internal class AddJobApplicationDto
{
    public IFormFile CV { get; set; }
}
