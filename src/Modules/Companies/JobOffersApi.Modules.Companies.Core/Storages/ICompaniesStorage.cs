namespace JobOffersApi.Modules.Companies.Core.Storages;

internal interface ICompaniesStorage
{
    Task<bool> IsWorkingAsync(Guid companyId, Guid employerId, CancellationToken cancellationToken = default);
}
