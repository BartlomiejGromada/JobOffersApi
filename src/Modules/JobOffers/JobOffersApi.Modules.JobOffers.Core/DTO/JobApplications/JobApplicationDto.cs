using JobOffersApi.Modules.JobOffers.Core.Entities.JobMenus;

namespace JobOffersApi.Modules.JobOffers.Core.DTO.JobApplications;

internal class JobApplicationDto
{
    public Guid CandidateId { get; init; }
    public string CandidateFirstName { get; init; }
    public string CandidateLastName { get; init; }
    public string? MessageToEmployer { get; init; }
    public DispositionDto Disposition { get; init; }
    public FinancialConditionDto? FinancialExpectations { get; init; }
    public ContractType? PreferredContract { get; init; }
    public JobApplicationStatus Status { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}

internal class DispositionDto
{
    public Availability Availability { get; init; }
    public DateOnly? Date { get; init; }
}