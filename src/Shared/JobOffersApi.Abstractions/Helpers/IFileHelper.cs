using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersApi.Abstractions.Helpers;

public interface IFileHelper
{
    Task<byte[]> ConvertToByteArrayAsync(IFormFile file, CancellationToken cancellationToken = default);
}
