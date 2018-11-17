using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagerService.Database
{
    /// <summary>
    /// base interface for dao in order to have low coupling for validation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    interface IDao<T> where T: new()
    {
        List<T> GetList(int userId);
        int Create(T item, int userId);
        void Update(T item, int userId);
        void Delete(T item, int userId);
    }
}
