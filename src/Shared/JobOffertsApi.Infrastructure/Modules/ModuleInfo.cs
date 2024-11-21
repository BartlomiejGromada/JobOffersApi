using System.Collections.Generic;

namespace JobOffersApi.Infrastructure.Modules;

public record ModuleInfo(string Name, IEnumerable<string> Policies);