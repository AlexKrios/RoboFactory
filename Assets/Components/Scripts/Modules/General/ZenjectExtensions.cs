using System.Linq;
using RoboFactory.General.Services;
using Zenject;

namespace RoboFactory.General
{
    public static class ZenjectExtensions
    {
        public static void BindService<TService>(this DiContainer container)
            where TService : Service
        {
            var current = typeof(TService);
            var types = current.GetInterfaces().ToHashSet();
            types.Add(current);
            types.Add(typeof(Service));
            
            container.Bind(types).To<TService>().AsSingle().NonLazy();
        }
    }
}