using MySpector.Repo;

namespace MySpector.Core
{
    public class ServiceLocator
    {
        public Repository Repo { get; }

        private static ServiceLocator _instance;

        public static ServiceLocator Instance
        {
            get
            {
                _instance = _instance ?? new ServiceLocator();
                return _instance;
            }
        }

        private ServiceLocator()
        {
            Repo = new Repository();
        }
    }
}