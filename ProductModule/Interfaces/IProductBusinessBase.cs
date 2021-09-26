using BaseModule.Interfaces;
using Core.Entities;
using ProductModule.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.Interfaces
{
    public interface IProductBusinessBase<T>: IBusinessBase<T> where T : class, IEntity, new()
    {
    }
}
