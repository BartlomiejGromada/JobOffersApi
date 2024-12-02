using JobOffersApi.Abstractions.Commands;

namespace JobOffersApi.Modules.Companies.Core.Commands;

internal class AddEmployerToCompanyCommand : ICommand
{
    public AddEmployerToCompanyCommand(
        Guid companyId,
        string userEmail, 
        string position, 
        Guid invokerId,
        string invokerRole)
    {
        CompanyId = companyId;
        UserEmail = userEmail;
        Position = position;
        InvokerId = invokerId;
        InvokerRole = invokerRole;
    }

    public Guid CompanyId { get; init; }
    public string UserEmail { get; init; }
    public string Position { get; init; }
    public Guid InvokerId { get; init; }
    public string InvokerRole { get; init; }
}
