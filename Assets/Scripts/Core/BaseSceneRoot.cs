using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class BaseSceneRoot : MonoBehaviour, IFlowCreator, IContextCreator
    {
        protected Dictionary<Type, Func<IFlow>> _flowCreationDict = new();

        protected Dictionary<Type, Func<IContext>> _contextCreationDict = new();

        public T CreateFlow<T>() where T : IFlow
        {
            if (!_flowCreationDict.TryGetValue(typeof(T), out var creation))
            {
                throw new InvalidOperationException($"{gameObject.name}::CreateFlow: {typeof(T).Name} is not registered.");
            }

            return (T)creation();
        }

        protected void RegisterFlowCreation<T>(Func<T> creation) where T : class, IFlow
        {
            _flowCreationDict.Add(typeof(T), creation);
        }

        public T CreateContext<T>() where T : IContext
        {
            if (!_contextCreationDict.TryGetValue(typeof(T), out var creation))
            {
                throw new InvalidOperationException($"{gameObject.name}::CreateRole: {typeof(T).Name} is not registered.");
            }

            return (T)creation();
        }

        protected void RegisterContextCreation<T>(Func<T> creation) where T : class, IContext
        {
            _contextCreationDict.Add(typeof(T), creation);
        }

    }
}