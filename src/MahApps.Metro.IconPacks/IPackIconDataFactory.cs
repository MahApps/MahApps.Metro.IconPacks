using System;

namespace MahApps.Metro.IconPacks
{
    public interface IPackIconDataFactory
    {
        bool GetData(Type enumType, string name, out string pathData);
    }
}
