using System.Collections;
using UnityEngine;

namespace Modules.General.Coroutines
{
    [AddComponentMenu("Scripts/General/Coroutines", 0)]
    public class CoroutinesController : MonoBehaviour, ICoroutinesController
    {
        //public Dictionary<Coroutine, IEnumerator> CoroutineDictionary { get; } 
        
        public Coroutine StartNewCoroutine(IEnumerator func)
        {
            return StartCoroutine(func);
            //CoroutineDictionary.Add(coroutine, func);
        }
        public void StopSelectCoroutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
            //CoroutineDictionary.Remove(coroutine);
        }
    }
}