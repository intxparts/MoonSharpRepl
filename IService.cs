using System;
using System.Collections.Generic;
using System.Text;

namespace MoonSharpRepl
{
    public interface IService
    {
        void Initialize(IAppContainer container);
    }
}
