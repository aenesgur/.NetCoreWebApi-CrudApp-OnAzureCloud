using CrudApp.Cache.Abstract;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrudApp.Cache
{
    public class RedisCacheService : ICacheService
    {
        private IConfiguration _configuration;
        private readonly RedisEndpoint _redisConfiguration; 
        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            _redisConfiguration = new RedisEndpoint() { Host = _configuration["RedisConfig:Host"], Password = _configuration["RedisConfig:Password"], Port = _configuration["RedisConfig:Port"].ToInt() };
        }

        public void Set<T>(string key, T value, TimeSpan time) where T : class
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                client.Set(key, value, time);
            }
        }

        public T Get<T>(string key) where T : class
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                return client.Get<T>(key);
            }
        }

        public bool IsSet(string key) 
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                return client.ContainsKey(key);
            }
        }

        public void Clear(string key)
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                client.Remove(key);
            }
        }

        public void ClearKeysByPattern(string pattern)
        {
            var keys = GetKeysByPattern(pattern);
            if (keys != null || keys.Count >= 0)
            {
                foreach (var key in keys)
                    Clear(key);
            }
            
        }

        private List<string> GetKeysByPattern (string pattern)
        {
            using (IRedisClient client = new RedisClient(_redisConfiguration))
            {
                return client.GetKeysByPattern(pattern).ToList();
            }
        }
    }
}