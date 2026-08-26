using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public abstract class BaseSceneRoot : MonoBehaviour, IScenarioContext
    {
        protected Dictionary<Type, Func<IFlow>> _flowCreationDict = new();

        protected Dictionary<Type, Func<IContext>> _contextCreationDict = new();

        protected Dictionary<Type, Func<IScenario>> _scenarioCreationDict = new();

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
                throw new InvalidOperationException($"{gameObject.name}::CreateContext: {typeof(T).Name} is not registered.");
            }

            return (T)creation();
        }

        protected void RegisterContextCreation<T>(Func<T> creation) where T : class, IContext
        {
            _contextCreationDict.Add(typeof(T), creation);
        }

        public T CreateScenario<T>() where T : IScenario
        {
            if (!_scenarioCreationDict.TryGetValue(typeof(T), out var creation))
            {
                throw new InvalidOperationException($"{gameObject.name}::CreateScenario: {typeof(T).Name} is not registered.");
            }

            return (T)creation();
        }

        protected void RegisterScenarioCreation<T>(Func<T> creation) where T : class, IScenario
        {
            _scenarioCreationDict.Add(typeof(T), creation);
        }


    }
}