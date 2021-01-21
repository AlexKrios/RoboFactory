using System.Collections;
using UnityEngine;

namespace Modules.General.Coroutines
{
    public interface ICoroutinesController
    {
        Coroutine StartNewCoroutine(IEnumerator coroutine);
        void StopSelectCoroutine(Coroutine coroutine);
    }
}
