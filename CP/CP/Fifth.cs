namespace Fifth
{
    public interface IService
    {
        string GetData();
    }

    public class RealService : IService
    {
        public string GetData()
        {
            Console.WriteLine("Запрос к реальному сервису...");
            Thread.Sleep(3000);
            return Guid.NewGuid().ToString();
        }
    }

    public class ProxyService : IService
    {
        private IService _realService;
        private string _cachedData;
        private DateTime _lastRequestTime;
        private readonly TimeSpan _cacheTimeout = TimeSpan.FromSeconds(5);

        public ProxyService(IService realService)
        {
            _realService = realService;
        }

        public string GetData()
        {
            if (_cachedData != null && DateTime.Now - _lastRequestTime < _cacheTimeout)
            {
                Console.WriteLine("Возвращаем данные из кэша...");
                return _cachedData;
            }
            else
            {
                _cachedData = _realService.GetData();
                _lastRequestTime = DateTime.Now;
                return _cachedData;
            }
        }
    }

    public class Program
    {
        public static void main(string[] args)
        {
            IService realService = new RealService();
            IService proxyService = new ProxyService(realService);

            Console.WriteLine("Первый запрос:");
            var data1 = proxyService.GetData();
            Console.WriteLine($"Полученные данные: {data1}");


            Console.WriteLine("\nВторой запрос (сразу):");
            var data2 = proxyService.GetData();
            Console.WriteLine($"Полученные данные: {data2}");

            Thread.Sleep(6000);

            Console.WriteLine("\nТретий запрос (после таймаута):");
            var data3 = proxyService.GetData();
            Console.WriteLine($"Полученные данные: {data3}");

        }
    }
}