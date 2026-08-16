using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class BaseSceneRoot : MonoBehaviour , IFlowCreator
    {
        protected Dictionary<Type, Func<IFlow>> _flowCreationDict = new();

        public T CreateFlow<T>() where T : IFlow
        {
            if (!_flowCreationDict.TryGetValue(typeof(T), out var creation))
            {
                throw new InvalidOperationException($"{gameObject.name}::CreateFlow: {typeof(T).Name} is not registered.");
            }

            return (T)creation();
        }

        protected void RegisterFlow<T>(Func<T> creation) where T : class, IFlow
        {
            _flowCreationDict.Add(typeof(T), creation);
        }

    }
}