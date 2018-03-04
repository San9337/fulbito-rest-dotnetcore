using model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace datalayer.Contracts
{
    public interface IWithDefaultValue<T>
    {
        T GetDefaultValue();
    }
}
