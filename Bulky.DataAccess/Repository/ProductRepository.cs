using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository: Repository<Product>,IProductRepository
    {
        private readonly AppDBContext _appDBContext;
        public ProductRepository(AppDBContext appDBContext): base(appDBContext)
        {
            this._appDBContext = appDBContext;
        }
    }
}
