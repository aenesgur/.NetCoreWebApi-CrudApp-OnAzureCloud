using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp.BLL.Abstract
{
    public interface IManager<T> where T:class
    {
        Task<List<T>> Search(int page);
        Task<T> GetById(int id);
        Task<T> GetById_NonCached(int id);
        Task<bool> Add(T model);
        Task<bool> AddList(List<T> modelList);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, T model);
    }
}
