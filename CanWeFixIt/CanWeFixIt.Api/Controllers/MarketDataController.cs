using CanWeFixIt.Core.Models.Dtos;
using CanWeFixIt.Service.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/marketdata")]
    public class MarketDataController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;

        public MarketDataController(IMarketDataService marketDataService)
        {
            _marketDataService = marketDataService ?? throw new ArgumentNullException(nameof(marketDataService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketDataDto>>> GetAllActiveMarketDataWithInstrument(CancellationToken cancellationToken)
        {
            var result = await _marketDataService.ListActiveMarketDataWithInstrumentId(cancellationToken);
            return Ok(result);
        }
    }
}
