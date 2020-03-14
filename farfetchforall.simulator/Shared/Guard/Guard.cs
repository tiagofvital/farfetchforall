using System;

namespace FarfetchForAll.Simulator.Shared
{
    public static class Guard
    {
        public static void AgainstNull<T>(T tEntity, string message)
        {
            if(tEntity == null)
            {
                throw new ArgumentNullException(typeof(T).Name, message);
            }
        }

        public static void Against(bool condition, string message)
        {
            if (condition)
            {
                throw new Exception(message);
            }
        }

        public static void AgainstNot(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception(message);
            }
        }
    }
}
