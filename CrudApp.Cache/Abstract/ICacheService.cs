using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp.Cache.Abstract
{
    public interface ICacheService
    {
        T Get<T>(string key) where T : class;
        void Set<T>(string key, T value, TimeSpan time) where T : class;
        bool IsSet(string key);
        void Clear(string key);
        void ClearKeysByPattern(string pattern);
    }
}
