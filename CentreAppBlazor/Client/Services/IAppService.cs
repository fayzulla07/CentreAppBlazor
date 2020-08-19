using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentreAppBlazor.Client.Services
{
    public interface IAppService
    {
        Task<T> GetAsync<T>(string uri);
        Task UpdateAsync<T>(T obj, int id, string uri);
        Task<T> InsertAsync<T>(T obj, string url);
        Task DeleteAsync(int Id, string url);
        Task<T> GetByIdAsync<T>(int id, string uri);

        Task<T> PostAsync<T>(object obj, string url);

    }
}
