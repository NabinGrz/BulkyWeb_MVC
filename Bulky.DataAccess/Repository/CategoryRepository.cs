using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    /**
     * We do not need the implementation for base function, because we already have done that in Base Repo
     * So we are extending 
     * **/
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly AppDBContext _appDBContext;
       
        public CategoryRepository(AppDBContext appDBContext): base(appDBContext) /*Here we are, passing the "DBConext" to all the Base Class */
        {
            this._appDBContext = appDBContext;
        }
        public void Save()
        {
            _appDBContext.SaveChanges();
        }

        public void Update(Category category)
        {
            _appDBContext.Update(category);
        }
    }
}
