using System.Collections.Generic;
using System.Threading.Tasks;

namespace LIBPresentationCore.Core
{
    public interface IApp
    {
        Dictionary<string, object> GetPopup(Dictionary<string, object> data);
    }
}