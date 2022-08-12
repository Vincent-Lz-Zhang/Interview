using CanWeFixIt.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Core.Interfaces
{
    public interface IMarketDataRepository
    {
        Task<long> GetTotalDataValueAsync(CancellationToken cancellationToken = default);
        Task<List<MarketDataWithInstrument>> ListMarketDataWithInstrumentAsync(bool isActiveOnly, CancellationToken cancellationToken = default);
    }
}