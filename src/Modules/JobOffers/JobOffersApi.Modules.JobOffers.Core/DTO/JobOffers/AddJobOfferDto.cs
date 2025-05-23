﻿using JobOffersApi.Abstractions.DTO;
using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.JobOffers;

internal class AddJobOfferDto
{
    public string Title { get; init; }
    public string DescriptionHtml { get; init; }
    public int Vacancies { get; init; }
    public AddLocationDto Location { get; init; }
    public Guid CompanyId { get; init; }
    public string CompanyName { get; init; }
    public List<AddJobAttribute> Attributes { get; init; }
    public List<AddFinancialExpectationsDto>? FinancialConditions { get; init; }
    public int? ValidityInDays { get; init; }
}


internal class AddJobAttribute
{
    public JobAttributeType Type { get; init; }
    public string Name { get; init; }
}

