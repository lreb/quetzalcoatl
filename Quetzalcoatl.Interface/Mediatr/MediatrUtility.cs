using System;
using System.Linq;
using System.Reflection;

namespace Quetzalcoatl.Infrastructure.Mediatr
{
    public static class MediatrUtility
    {
        public static Type[] GetMediatrAssembliesToScan(string projectNameSpace)
        {
            var assemblies = Assembly.Load(projectNameSpace)
                .GetTypes()
                .ToArray();

            return assemblies;
        }
    }
}
