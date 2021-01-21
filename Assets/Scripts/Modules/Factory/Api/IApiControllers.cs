using System;
using System.Collections;

namespace Modules.Factory.Api
{
    public interface IApiControllers
    {
        IEnumerator LoadData(Action<string> finishDelegate);
    }
}
