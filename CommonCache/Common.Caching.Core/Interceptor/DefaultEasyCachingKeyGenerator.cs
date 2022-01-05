namespace Common.Caching.Core.Interceptor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    /// <summary>
    /// Default easycaching key generator.
    /// </summary>
    public class DefaultEasyCachingKeyGenerator : IEasyCachingKeyGenerator
    {
        private const char LinkChar = ':';

        public string GetCacheKey(MethodInfo methodInfo, object[] args, string prefix, string key)
        {

            var methodArguments = args?.Any() == true
                                      ? args.Select(ParameterCacheKeys.GenerateCacheKey)
                                      : new[] { "0" };
            if (key.Trim().Length > 0)
            {
                // return  GenerateCacheKey(methodInfo, prefix, key, methodArguments,args[0]);
                return GenerateCacheKey(methodInfo, prefix, key, methodArguments, args);
            }

            return GenerateCacheKey(methodInfo, prefix, key, methodArguments);

        }

        public string GetCacheKeyPrefix(MethodInfo methodInfo, string prefix)
        {
            if (!string.IsNullOrWhiteSpace(prefix)) return $"{prefix}{LinkChar}";

            var typeName = methodInfo.DeclaringType?.Name;
            var methodName = methodInfo.Name;

            return $"{typeName}{LinkChar}{methodName}{LinkChar}";
        }


        private string GenerateCacheKey(MethodInfo methodInfo, string prefix, string key, IEnumerable<string> parameters, object[] originalObject = null)
        {

            var cacheKeyPrefix = GetCacheKeyPrefix(methodInfo, prefix);
            bool bUsingKey = false;
            var builder = new StringBuilder();
            key = key.Replace("{", "").Replace("}", "");
            string[] keys = key.Trim().Split(',');

            builder.Append(cacheKeyPrefix);
            foreach (var sKey in keys)
            {
                if (sKey != "")
                {
                    string[] skeyName = sKey.Split('.');
                    if (skeyName.Length > 1)
                    {
                        builder.Append(string.Join(LinkChar.ToString(), "{" + GenerateCacheKeywithModel(originalObject, skeyName[1], skeyName[0]) + "}"));

                    }
                    else
                    {
                        builder.Append(string.Join(LinkChar.ToString(), "{" + skeyName[0].ToLower() + "}"));

                    }
                    builder.Append(string.Join(LinkChar.ToString(), "_"));
                    bUsingKey = true;
                }
            }


            if (!bUsingKey)
            {
                builder.Append(string.Join(LinkChar.ToString(), parameters));
            }
            return builder.ToString();
        }

        private static string GenerateCacheKeywithModel(object[] parameter, string keyName, string keyObjectName)
        {
            string key = string.Empty;
            if (parameter == null) return string.Empty;

            foreach (var vObject in parameter)
            {
                System.Type t = vObject.GetType();

                if (t.Name.Equals(keyObjectName, StringComparison.OrdinalIgnoreCase))
                {

                    if (t.IsClass)
                    {
                        key += $"{ t.Name}_";
                        foreach (var p in t.GetProperties())
                        {
                            if (p.Name.Equals(keyName, StringComparison.OrdinalIgnoreCase))
                                key += $"{ p.Name}_{p.GetValue(vObject)}_";
                        }

                    }
                }
            }
            return key.ToLower();

        }
    }
}

