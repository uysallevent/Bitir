using BaseModule.Business;
using Core.DataAccess;
using Core.DataAccess.EntityFramework.Interfaces;
using ProductModule.Entities;
using ProductModule.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductModule.Business
{
    public class ProductBusinessBase : BusinessBase<Product>, IProductBusinessBase<Product>
    {
        private IRepository<Product> _productRepository;
        private IUnitOfWork _uow;
        public ProductBusinessBase(IRepository<Product> productRepository, IUnitOfWork uow) : base(productRepository, uow)
        {
            _productRepository = productRepository;
            _uow = uow;
        }
    }
}
