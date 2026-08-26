using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Core
{
    public interface IScenario { }

    public interface IScenarioContext : IScenarioCreator, IFlowCreator { }


    public interface IScenarioCreator
    {
        public T CreateScenario<T>() where T : IScenario;
    }
}
