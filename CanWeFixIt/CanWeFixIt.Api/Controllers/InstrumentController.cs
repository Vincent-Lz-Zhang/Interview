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
    [Route("api/v{version:apiVersion}/instruments")]
    public class InstrumentController : ControllerBase
    {
        private readonly IInstrumentService _instrumentService;

        public InstrumentController(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService ?? throw new ArgumentNullException(nameof(instrumentService));
        }

        [HttpGet]
        // TODO: add error response type for documentation
        public async Task<ActionResult<IEnumerable<InstrumentDto>>> GetAllActiveInstruments(CancellationToken cancellationToken)
        {
            // Test the error handling
            //throw new ArgumentException("Error handler test");
            
            // TODO: catch exceptions specific to this endpoint's logic
            var result = await _instrumentService.ListActiveInstruments(cancellationToken);
            return Ok(result);
        }
    }
}
