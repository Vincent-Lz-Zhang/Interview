using CanWeFixIt.Core.Models.Entities;
using CanWeFixIt.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Service.Data
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly CanWeFixItContext _canWeFixItContext;

        public InstrumentRepository(CanWeFixItContext canWeFixItContext)
        {
            _canWeFixItContext = canWeFixItContext;
        }

        public async Task<List<Instrument>> ListInstrumentsAsync(bool isActiveOnly, CancellationToken cancellationToken = default)
        {
            var query = _canWeFixItContext.Instruments.AsNoTracking().AsQueryable();
            if (isActiveOnly)	// TODO: consider query filter of EF Core
            {
                query = query.Where(x => x.IsActive);
            }
            return await query.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
