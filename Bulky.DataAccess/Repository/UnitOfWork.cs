using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _appDBContext;
        public ICategoryRepository Category { get; set; }
        public IProductRepository Product { get; set; }
        public UnitOfWork(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
            Category = new CategoryRepository(_appDBContext);
            Product = new ProductRepository(_appDBContext);
        }


        public void Save()
        {
            _appDBContext.SaveChanges();
        }
    }
}
