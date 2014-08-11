using System;

namespace Canvas.v1.Plugin
{
    public interface IResourcePlugins
    {
        void Register<T>(Func<T> func)
            where T : class;

        T Get<T>();
    }
}