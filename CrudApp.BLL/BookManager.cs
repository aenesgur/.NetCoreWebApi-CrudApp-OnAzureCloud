using CrudApp.BLL.Abstract;
using CrudApp.Cache.Abstract;
using CrudApp.Data.Service.Abstract;
using CrudApp.Entities;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrudApp.BLL
{
    public class BookManager : IBookManager
    {
        private const string REDIS_CACHE_KEY = "Book.Key.{0}";
        private const string BOOKS_FILTER_KEY = "Books.Filter.{0}.{1}";
        private const string BOOKS_CLEAR_KEY = "*Book*";
        private const int PAGE_SIZE = 4;
        private IBookService _bookService;
        private ICacheService _cacheService;
        private readonly IConfiguration _configuration;
        private readonly TimeSpan expirationTime;

        public BookManager(IBookService bookService, ICacheService cacheService, IConfiguration configuration)
        {
            _bookService = bookService;
            _cacheService = cacheService;
            _configuration = configuration;
            expirationTime = TimeSpan.FromSeconds(_configuration["RedisConfig:ExpirationTime"].ToInt());
        }
        public async Task<bool> Add(Book model)
        {
            return await _bookService.Add(model);
        }
        public async Task<bool> AddList(List<Book> modelList)
        {
            return await _bookService.AddList(modelList);
        }

        public async Task<List<Book>> Search(int page)
        {
            string cacheKey = string.Format(BOOKS_FILTER_KEY, page, PAGE_SIZE);
            var data =  _cacheService.Get<List<Book>>(cacheKey);
            if (data != null)
                return data;

            data = await _bookService.Search(page, PAGE_SIZE);
            if (data != null)
            {
                _cacheService.Set<List<Book>>(cacheKey, data, expirationTime);
                return data;
            }
            return null;
        }

        public async Task<Book> GetById(int id)
        {
            string cacheKey = string.Format(REDIS_CACHE_KEY,id);
            var data =  _cacheService.Get<Book>(cacheKey);
            if (data != null)
                return data;

            data = await _bookService.GetById(id);
            if (data != null)
            {
                _cacheService.Set<Book>(cacheKey, data, expirationTime);
                return data;
            }
            return null;
        }

        public async Task<Book> GetById_NonCached(int id)
        {
            return await _bookService.GetById(id);
        }

        public async Task<bool> Delete(int id)
        {
            var data = await GetById_NonCached(id);
            if (data != null)
            {
                var result = await _bookService.Delete(id);
                _cacheService.ClearKeysByPattern(BOOKS_CLEAR_KEY);
                return result;
            }
            return false;
        }

        public async Task<bool> Update(int id, Book model)
        {
            var data = await GetById_NonCached(id);
            if (data != null)
            {
                var result = await _bookService.Update(id, model);
                _cacheService.ClearKeysByPattern(BOOKS_CLEAR_KEY);
                return result;
            }
            return false;
            
        }
    }
}
