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
}
