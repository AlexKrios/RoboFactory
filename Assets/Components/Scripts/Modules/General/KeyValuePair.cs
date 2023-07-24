using System;
using UnityEngine;

namespace RoboFactory.General
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey _key;
        [SerializeField] private TValue _value;

        public TKey Key
        {
            get => _key;
            set => _key = value;
        }
        public TValue Value
        {
            get => _value;
            set => _value = value;
        }
    }
}