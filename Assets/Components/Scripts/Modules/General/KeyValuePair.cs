using System;
using UnityEngine;

namespace RoboFactory.General
{
    [Serializable]
    public class KeyValuePair<TKey, TValue>
    {
        [SerializeField] private TKey key;
        [SerializeField] private TValue value;

        public TKey Key
        {
            get => key;
            set => key = value;
        }
        public TValue Value
        {
            get => value;
            set => this.value = value;
        }
    }
}