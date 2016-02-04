using System;
using System.Collections.Generic;

namespace MirrorWebService.Managers.Interfaces
{
    public interface ICallServices
    {
        T GetData<T>();
    }
}