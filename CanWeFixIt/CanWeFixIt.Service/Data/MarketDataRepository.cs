using CanWeFixIt.Core.Interfaces;
using CanWeFixIt.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Service.Data
{
    public class MarketDataRepository : IMarketDataRepository
    {
        private readonly CanWeFixItContext _canWeFixItContext;

        public MarketDataRepository(CanWeFixItContext canWeFixItContext)
        {
            _canWeFixItContext = canWeFixItContext;
        }

        public async Task<List<MarketDataWithInstrument>> ListMarketDataWithInstrumentAsync(bool isActiveOnly, CancellationToken cancellationToken = default)
        {
            var marketDataQuery = _canWeFixItContext.MarketData.AsNoTracking().AsQueryable();

            if (isActiveOnly)
            {
                marketDataQuery = marketDataQuery.Where(x => x.IsActive);
            }

            var marketDataDtoQuery = marketDataQuery.Join(_canWeFixItContext.Instruments.AsNoTracking(),
                    market => market.Sedol,
                    instrument => instrument.Sedol,
                    (market, instrument) => new MarketDataWithInstrument
                    {
                        MarketDataId = market.Id,
                        DataValue = market.DataValue,
                        Sedol = market.Sedol,
                        InstrumentId = instrument.Id,
                        IsMarketDataActive = market.IsActive,
                        IsInstrumentActive = instrument.IsActive
                    }
                );

            return await marketDataDtoQuery.ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<long> GetTotalDataValueAsync(CancellationToken cancellationToken = default)
        {
            return await _canWeFixItContext.MarketData
                .AsNoTracking()
                .Where(m => m.IsActive)
                .SumAsync(m => m.DataValue, cancellationToken: cancellationToken);
        }


    }
}
