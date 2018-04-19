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
        /// случайные котировки чтобы выпадал и loss и profit
        /// </summary>
        private readonly static float[] CurrencyRates = 
            {
                26.1267f,
                26.0531f,
                25.9276f,
                26.1512f,
                26.0277f,
                25.7600f
            };
        private readonly static float[] GoldRates = 
            {
                1.35308f,
                1.35401f,
                1.35527f,
                1.30104f,
                1.35893f,
                1.35201f,
            };
        private readonly static float[] SilverRates =
            {
                1.15308f,
                1.15401f,
                1.15527f,
                1.10104f,
                1.15893f,
                1.15201f,
            };
        private readonly static float[] OilRates =
            {
                73.89f,
                73.85f,
                73.80f,
                73.95f,
                74.00f,
                74.05f,
            };
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

        ///// <summary>
        ///// Случайный decimal
        ///// </summary>
        ///// <param name="rng"></param>
        ///// <returns></returns>
        //public static decimal RandomDecimal(this Random rng)
        //{
        //    byte scale = (byte)rng.Next(29);
        //    bool sign = rng.Next(2) == 1;
        //    return new decimal(rng.NextInt32(), rng.NextInt32(), rng.NextInt32(), sign, scale);
        //}

        ///// <summary>
        ///// Вспомагательный метод для RandomDecimal
        ///// </summary>
        ///// <param name="rng"></param>
        ///// <returns></returns>
        //private static int NextInt32(this Random rng)
        //{
        //    int firstBits = rng.Next(0, 1 << 4) << 28;
        //    int lastBits = rng.Next(0, 1 << 28);
        //    return firstBits | lastBits;
        //}

        /// <summary>
        /// получаем индекс какой-то котировки
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        private static int GetRandomIndex(Instrument inst)
        {
            Random rnd = new Random();
            int index = -1;
            switch(inst)
            {
                case Instrument.Currency:
                    index = rnd.Next(0, CurrencyRates.Length);
                    break;
                case Instrument.Gold:
                    index = rnd.Next(0, GoldRates.Length);
                    break;
                case Instrument.Silver:
                    index = rnd.Next(0, SilverRates.Length);
                    break;
                case Instrument.Oil:
                    index = rnd.Next(0, OilRates.Length);
                    break;
            }
            return index;
        }

        /// <summary>
        /// получаем случайную котировку
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static float RandomCoteValue(Instrument inst)
        {
            float coteValue = 0.00f;
            switch (inst)
            {
                case Instrument.Currency:
                    coteValue = CurrencyRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Gold:
                    coteValue = GoldRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Silver:
                    coteValue = SilverRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Oil:
                    coteValue = OilRates[GetRandomIndex(inst)];
                    break;
            }
            return coteValue;
        }

        /// <summary>
        /// получаем случайную величину лота (валюта, драгмет, топливо)
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static float GetRandomLot()
        {
            Random rnd = new Random();
            return rnd.Next(1, 5000);
        }

        public static float GetRandomLotNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 11);
        }
    }
}
