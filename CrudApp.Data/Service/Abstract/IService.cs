using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp.Data.Service.Abstract
{
    public interface IService<T> where T:class
    {
        Task<List<T>> Search(int page, int pageSize);
        Task<T> GetById(int id);
        Task<bool> Add(T model);
        Task<bool> AddList(List<T> modelList);
        Task<bool> Delete(int id);
        Task<bool> Update(int id, T model);

    }
}
