using JobOffersApi.Abstractions.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JobOffersApi.Infrastructure.Helpers;

public sealed class FileHelper : IFileHelper
{
    public async Task<byte[]> ConvertToByteArrayAsync(IFormFile file,
        CancellationToken cancellationToken = default)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("Invalid file");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, cancellationToken);
        return memoryStream.ToArray();
    }
}
