using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBDomainContext
{
    public interface IHelper
    {
        Task<Dictionary<string, object>> GetList(Dictionary<string, object> data);
        Task<Dictionary<string, object>> SaveEntity(Dictionary<string, object> data);
        Task<Dictionary<string, object>> UpdateEntity(Dictionary<string, object> data);
        Task<Dictionary<string, object>> DeleteEntity(Dictionary<string, object> data);
    }
}