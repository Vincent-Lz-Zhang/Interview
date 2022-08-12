using CanWeFixIt.Core.Models.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Service.Data
{
    public interface IMarketDataService
    {
        Task<List<MarketDataDto>> ListActiveMarketDataWithInstrumentId(CancellationToken cancellationToken = default);
        Task<List<MarketValuationDto>> GetMarketValuationAsync(CancellationToken cancellationToken = default);
    }
}