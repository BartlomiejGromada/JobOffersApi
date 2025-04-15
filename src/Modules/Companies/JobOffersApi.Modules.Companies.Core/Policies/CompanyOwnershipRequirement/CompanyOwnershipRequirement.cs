using Microsoft.AspNetCore.Authorization;

namespace JobOffersApi.Modules.Companies.Core.Policies.CompanyOwnershipRequirement;

internal sealed class CompanyOwnershipRequirement : IAuthorizationRequirement
{
}
