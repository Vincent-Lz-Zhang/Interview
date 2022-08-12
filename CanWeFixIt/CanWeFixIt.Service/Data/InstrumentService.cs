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
    public class InstrumentService : IInstrumentService
    {
        private readonly IInstrumentRepository _instrumentRepository;
        private readonly IMapper _mapper;

        public InstrumentService(IMapper mapper, IInstrumentRepository instrumentRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _instrumentRepository = instrumentRepository ?? throw new ArgumentNullException(nameof(instrumentRepository));
        }

        public async Task<List<InstrumentDto>> ListActiveInstruments(CancellationToken cancellationToken = default)
        {
            var instruments = await _instrumentRepository.ListInstrumentsAsync(isActiveOnly: true, cancellationToken);

            return instruments.Select(
                    i => _mapper.Map<InstrumentDto>(i)
                ).ToList();
        }
    }
}
