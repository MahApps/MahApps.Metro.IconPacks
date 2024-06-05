using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace MahApps.Metro.IconPacks.Utils
{
    public static class AssemblyTextFileReader
    {
        private static string GetManifestResourceName(this Assembly assembly, string fileName)
        {
            var name = assembly.GetManifestResourceNames().SingleOrDefault(n => n.EndsWith(fileName, StringComparison.InvariantCultureIgnoreCase));

            if (string.IsNullOrEmpty(name))
            {
                throw new FileNotFoundException($"Embedded file '{fileName}' could not be found in assembly '{assembly.FullName}'.", fileName);
            }

            return name;
        }

        public static async Task<string> ReadFileAsync([NotNull] this Assembly assembly, string fileName)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var resourceName = assembly.GetManifestResourceName(fileName);

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                return await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            return null;
        }

        public static string ReadFile([NotNull] this Assembly assembly, string fileName)
        {
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));

            var resourceName = assembly.GetManifestResourceName(fileName);

            using var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream != null)
            {
                using var reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }

            return null;
        }
    }
}