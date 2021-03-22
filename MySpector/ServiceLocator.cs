using MySpector.Repo;

namespace MySpector.Core
{
    public class ServiceLocator
    {
        private static ServiceLocator _instance;

        public static ServiceLocator Instance
        {
            get
            {
                _instance = _instance ?? new ServiceLocator();
                return _instance;
            }
        }

        public Repo Repo { get; internal set; }

        private ServiceLocator()
        {

        }
    }
}