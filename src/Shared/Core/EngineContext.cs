#nullable disable
namespace Shared.Core
{
    public class EngineContext
    {
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }

        public static IEngine Create()
        {
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new Engine());
        }
    }
}
