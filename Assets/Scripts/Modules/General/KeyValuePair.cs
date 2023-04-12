using System;
using UnityEngine;

namespace Modules.General
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey key;
        [SerializeField] private TValue value;
            
        public TKey Key => key;
        public TValue Value => value;
    }
}