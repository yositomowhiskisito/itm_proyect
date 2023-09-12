using System;
using System.Collections.Generic;
using System.Linq;

namespace LIBUtilities.Core
{
    public class CacheHelper
    {
        private static CacheHelper Instance;
        private static Dictionary<string, object> CacheMemory;

        private CacheHelper()
        {
            CacheMemory = new Dictionary<string, object>();
        }

        public static void Add(string key, object value)
        {
            CreateInstance();
            if (!CacheMemory.ContainsKey(key))
                CacheMemory.Add(key, value);
            else
                CacheMemory[key] = value;
        }

        public static object GetValue(string key)
        {
            CreateInstance();
            if (CacheMemory.ContainsKey(key))
                return CacheMemory[key];
            return null;
        }

        private static void CreateInstance()
        {
            if (Instance == null)
                Instance = new CacheHelper();
        }

        public static void Remove(string key)
        {
            CreateInstance();
            if (CacheMemory.ContainsKey(key))
                CacheMemory.Remove(key);
        }

        public static bool ContainsKey(string key)
        {
            CreateInstance();
            return CacheMemory.ContainsKey(key);
        }

        public static bool RemoveKeys(List<string> keys)
        {
            CreateInstance();
            var list = CacheMemory.Keys.Where(x => keys.Any(y => x.Contains(y))).ToList();
            foreach (var item in list)
                Remove(item);
            return true;
        }

        public static int CountKeys(List<string> keys)
        {
            CreateInstance();
            return CacheMemory.Keys.Count(x => keys.Any(y => x.Contains(y)));
        }

        public static int CountValues(string value)
        {
            CreateInstance();
            return CacheMemory.Values.Count(x => x.ToString() == value);
        }
    }
}