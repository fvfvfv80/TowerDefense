using System;
using System.Collections.Generic;
using System.Text;

namespace Assets.Scripts.Core
{
    public interface IFlow { }


    public interface IFlowCreator
    {
        public T CreateFlow<T>() where T : IFlow;
    }
}
