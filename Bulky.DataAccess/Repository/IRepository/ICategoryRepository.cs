using Bulky.Models.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    /**
    This is the Interface Repo made for "Category"(to have some extra functions, for "Category" only)
    which extends Main Repo "IRepository"
     **/
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category category);
        void Save();
    }
}
