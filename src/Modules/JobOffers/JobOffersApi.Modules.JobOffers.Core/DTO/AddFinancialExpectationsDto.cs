using JobOffersApi.Abstractions.Core;
using JobOffersApi.Modules.JobOffers.Core.Entities.Enums;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.DTO;

internal class AddFinancialExpectationsDto
{
    public decimal Value { get; init; }
    public CurrencyCode ConcurrencyCode { get; init; }
    public SalaryType SalaryType { get; init; }
    public SalaryPeriod SalaryPeriod { get; init; }
}
