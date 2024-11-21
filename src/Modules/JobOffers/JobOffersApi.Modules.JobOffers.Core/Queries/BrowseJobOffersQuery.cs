using JobOffersApi.Abstractions.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobOffersApi.Modules.JobOffers.Core.Queries;

internal class BrowseJobOffersQuery : IQuery<Paged<JobOfferDto>>
{
}
