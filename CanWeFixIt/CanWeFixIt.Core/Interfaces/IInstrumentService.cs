using CanWeFixIt.Core.Models.Dtos;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Service.Data
{
    public interface IInstrumentService
    {
        Task<List<InstrumentDto>> ListActiveInstruments(CancellationToken cancellationToken = default);
    }
}