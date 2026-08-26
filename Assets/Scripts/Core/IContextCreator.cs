using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Core
{
    public interface IContext { }

    public interface IContextCreator
    {
        public T CreateContext<T>() where T : IContext;
    }


    public interface IScenario { }

    public interface IScenarioContext: IScenarioCreator, IFlowCreator { }


    public interface IScenarioCreator
    {
        public T CreateScenario<T>() where T : IScenario;
    }
}
