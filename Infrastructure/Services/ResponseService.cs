using BlinkCash.Core.Dtos;
using BlinkCash.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlinkCash.Infrastructure.Services
{
    public class ResponseService : IResponseService
    {
        public ExecutionResponse<T>
            ExecutionResponse<T>(string message, T data = null, bool status = false) where T : class
        {
            return new ExecutionResponse<T>
            {
                Status = status,
                Message = message,
                Data = data
            };
        }

        public ExecutionResponse<List<T>>
            ExecutionResponseList<T>(string message, List<T> data = null, bool status = false) where T : class
        {
            return new ExecutionResponse<List<T>>
            {
                Status = status,
                Message = message,
                Data = data
            };
        }

        public ExecutionResponse<T[]> ExecutionResponseList<T>(string message, T[] data = null, bool status = false) where T : class
        {
            return new ExecutionResponse<T[]>
            {
                Status = status,
                Message = message,
                Data = data
            };
        }
    }
}
