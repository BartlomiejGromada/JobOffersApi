﻿using JobOffersApi.Modules.Companies.Core.DTO.Companies;
using JobOffersApi.Modules.Companies.Core.DTO.Extensions;
using JobOffersApi.Modules.Companies.Core.Entities;
using JobOffersApi.Modules.Companies.Core.Storages;
using JobOffersApi.Modules.Companies.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace JobOffersApi.Modules.Users.Infrastructure.Storages;

internal sealed class CompaniesStorage : ICompaniesStorage
{
    private readonly IQueryable<Company> _companies;

    public CompaniesStorage(CompaniesDbContext dbContext)
    {
        _companies = dbContext.Companies.AsNoTracking();
    }

    public Task<bool> IsWorkingAsync(Guid companyId, Guid employerId, CancellationToken cancellationToken = default)
        => _companies
            .Where(c => c.Id == companyId)
            .AnyAsync(c => c.CompaniesEmployers.Where(ce => ce.Employer.Id == employerId) != null, cancellationToken);

    public Task<bool> IsExistAsync(string name, CancellationToken cancellationToken = default)
        => _companies.AnyAsync(c => c.Name == name, cancellationToken);

    public async Task<CompanyDto?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var company = await _companies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        return company?.ToDto();
    }
}
