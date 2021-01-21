using System;
using System.Collections.Generic;
using System.Diagnostics;
using Zenject.Internal;

namespace Zenject
{
    public enum ScopeTypes
    {
        Unset,
        Transient,
        Singleton
    }

    public enum ToChoices
    {
        Self,
        Concrete
    }

    public enum InvalidBindResponses
    {
        Assert,
        Skip
    }

    public enum BindingInheritanceMethods
    {
        None,
        CopyIntoAll,
        CopyDirectOnly,
        MoveIntoAll,
        MoveDirectOnly
    }

    [NoReflectionBaking]
    public class BindInfo : IDisposable
    {
        public readonly List<TypeValuePair> Arguments;
        public readonly List<Type> ContractTypes;
        public readonly List<Type> ToTypes; // Only relevant with ToChoices.Concrete
        public BindingInheritanceMethods BindingInheritanceMethod;
        public object ConcreteIdentifier;
        public BindingCondition Condition;
        public string ContextInfo;
        public object Identifier;
        public Action<InjectContext, object> InstantiatedCallback;
        public InvalidBindResponses InvalidBindResponse;
        public bool MarkAsCreationBinding;
        public bool MarkAsUniqueSingleton;
        public bool NonLazy;
        public bool OnlyBindIfNotBound;
        public bool RequireExplicitScope;
        public bool SaveProvider;
        public ScopeTypes Scope;
        public ToChoices ToChoice;

        public BindInfo()
        {
            ContractTypes = new List<Type>();
            ToTypes = new List<Type>();
            Arguments = new List<TypeValuePair>();

            Reset();
        }

        public void Dispose()
        {
            ZenPools.DespawnBindInfo(this);
        }

        [Conditional("UNITY_EDITOR")]
        public void SetContextInfo(string contextInfo)
        {
            ContextInfo = contextInfo;
        }

        public void Reset()
        {
            MarkAsCreationBinding = true;
            MarkAsUniqueSingleton = false;
            ConcreteIdentifier = null;
            SaveProvider = false;
            OnlyBindIfNotBound = false;
            RequireExplicitScope = false;
            Identifier = null;
            ContractTypes.Clear();
            BindingInheritanceMethod = BindingInheritanceMethods.None;
            InvalidBindResponse = InvalidBindResponses.Assert;
            NonLazy = false;
            Condition = null;
            ToChoice = ToChoices.Self;
            ContextInfo = null;
            ToTypes.Clear();
            Scope = ScopeTypes.Unset;
            Arguments.Clear();
            InstantiatedCallback = null;
        }
    }
}