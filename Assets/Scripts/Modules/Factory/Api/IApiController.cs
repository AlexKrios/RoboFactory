using Cysharp.Threading.Tasks;

namespace Modules.Factory.Api
{
    public interface IApiController
    {
        UniTask<string> LoadData();
    }
}
