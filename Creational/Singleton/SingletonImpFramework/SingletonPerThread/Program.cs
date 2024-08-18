namespace SingletonThreadDesignPattern
{
    public sealed class PerTreadSingleton
    {
        private static ThreadLocal<PerTreadSingleton> threadInstance 
            = new ThreadLocal<PerTreadSingleton>(
                () => new PerTreadSingleton());

        public int Id;
        private PerTreadSingleton()
        {
            Id = Thread.CurrentThread.ManagedThreadId;
        }

        public static PerTreadSingleton Instance => threadInstance.Value;
    }
    class Program
    {
        public static void Main(string[] args)
        {
            var t1 = Task.Factory.StartNew(() => { 
                Console.WriteLine("t1: " + PerTreadSingleton.Instance.Id);
            });

            var t2 = Task.Factory.StartNew(() => {
                Console.WriteLine("t2: " + PerTreadSingleton.Instance.Id);
                Console.WriteLine("t2: " + PerTreadSingleton.Instance.Id);
            });

            Task.WaitAll(t1, t2);

            Console.ReadKey();
        }
    }
}