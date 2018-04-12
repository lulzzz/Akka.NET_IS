using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEmulator.Types
{
    /// <summary>
    ///  класс вспомогательных методов
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// Возврат случайного Enum'a
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomEnumValue<T>()
        {
            var v = System.Enum.GetValues(typeof(T));
            return (T)v.GetValue(new Random().Next(v.Length));
        }

        /// <summary>
        /// Случайный decimal
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        public static decimal RandomDecimal(this Random rng)
        {
            byte scale = (byte)rng.Next(29);
            bool sign = rng.Next(2) == 1;
            return new decimal(rng.NextInt32(), rng.NextInt32(), rng.NextInt32(), sign, scale);
        }

        /// <summary>
        /// Вспомагательный метод для RandomDecimal
        /// </summary>
        /// <param name="rng"></param>
        /// <returns></returns>
        private static int NextInt32(this Random rng)
        {
            int firstBits = rng.Next(0, 1 << 4) << 28;
            int lastBits = rng.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static float RandomValue(Instrument inst)
        {
            switch (inst)
            {
                case Instrument.Currency:
            }
        }

        private float 
    }
}
