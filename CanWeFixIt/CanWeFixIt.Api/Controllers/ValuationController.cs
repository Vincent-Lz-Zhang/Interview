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
    [Route("/api/v{version:apiVersion}/valuations")]
    public class ValuationController : ControllerBase
    {
        private readonly IMarketDataService _marketDataService;

        public ValuationController(IMarketDataService marketDataService)
        {
            _marketDataService = marketDataService ?? throw new ArgumentNullException(nameof(marketDataService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarketValuationDto>>> GetTotalValuation(CancellationToken cancellationToken)
        {
            var result = await _marketDataService.GetMarketValuationAsync(cancellationToken);
            return Ok(result);
        }
    }
}
