using System;
using ServiceStack.Redis;

namespace docker_redis_test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var redisClient = GetRedisSentinel())
            {
                var test = redisClient.Get<string>("test");
                Console.WriteLine(test);
            }
            Console.ReadLine();
        }

        private static IRedisClient GetRedisSentinel()
        {
            var sentinelHost = "ip:port";
            var masterName = "clusterName";
            var authKey = "authKey";
            var sentinel = new RedisSentinel(sentinelHost, masterName);

            sentinel.HostFilter = host => $"{authKey}@{host}";
            var redisClientManager = sentinel.Start();
            return redisClientManager.GetReadOnlyClient();
        }
    }
}