using AutoMapper;
using CanWeFixIt.Core.Interfaces;
using CanWeFixIt.Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Service.Data
{
    public class MarketDataService : IMarketDataService
    {
        private readonly IMarketDataRepository _marketDataRepository;
        private readonly IMapper _mapper;

        public MarketDataService(IMapper mapper, IMarketDataRepository marketDataRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _marketDataRepository = marketDataRepository ?? throw new ArgumentNullException(nameof(marketDataRepository));
        }

        public async Task<List<MarketDataDto>> ListActiveMarketDataWithInstrumentId(CancellationToken cancellationToken = default)
        {
            var marketDataWithInstrument = await _marketDataRepository.ListMarketDataWithInstrumentAsync(isActiveOnly: true, cancellationToken);

            return marketDataWithInstrument.Select(
                    md => _mapper.Map<MarketDataDto>(md)
                ).ToList();
        }

        public async Task<List<MarketValuationDto>> GetMarketValuationAsync(CancellationToken cancellationToken = default)
        {
            var totalValue = await _marketDataRepository.GetTotalDataValueAsync(cancellationToken);

            return new List<MarketValuationDto>() { _mapper.Map<MarketValuationDto>(totalValue) };
        }
    }
}
