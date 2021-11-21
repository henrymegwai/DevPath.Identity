
using BlinkCash.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Core.Interfaces.Repositories
{
    public interface IConfirmationTokenRepository
    {
        Task<ConfirmationTokenDto> Create(ConfirmationTokenDto model);
        Task<bool> Deactivate(string tokenId);
        Task<ConfirmationTokenDto> Get(string tokenId);
    }
}
