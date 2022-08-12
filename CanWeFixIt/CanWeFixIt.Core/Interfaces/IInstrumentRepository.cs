using CanWeFixIt.Core.Models.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CanWeFixIt.Core.Interfaces
{
    public interface IInstrumentRepository
    {
        Task<List<Instrument>> ListInstrumentsAsync(bool isActiveOnly, CancellationToken cancellationToken = default);
    }
}