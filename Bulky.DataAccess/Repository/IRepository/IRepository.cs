using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        /**
        This are all base functionalities** which we will acces in other Repo.
        If we need the functionalities beside this, we will make seperate Repo for that,
        which will extends this main repo and have all the functionalities
        **/

        //**T==Category**
        //Get all datas
        IEnumerable<T> GetAll();
        //Get single data
        T GetT(Expression<Func<T,bool>> filter);
        //Created data
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
