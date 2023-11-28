using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminClient.Utility.HttpHelper.Interfaces
{
    public interface IHttpConnection<T>
    {
        Task<List<T>> GetAll();
        Task<T?> GetOne(string id);
        Task<bool> Delete(string id);
    }
}
