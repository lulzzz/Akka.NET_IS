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
        private readonly static float[] CurrencyOpenRates = 
            {
                26.1267f,
                26.0531f,
                25.9276f,
                26.1512f,
                26.0277f,
                25.7600f
            };

        private readonly static float[] CurrencyCloseRates =
            {
                26.0112f,
                26.1301f,
                25.8473f,
                26.5112f,
                26.1175f,
                25.9754f
            };
        private readonly static float[] GoldOpenRates = 
            {
                1.50353f,
                1.10435f,
                1.72553f,
                1.40103f,
                1.39853f,
                1.10253f,
            };
        private readonly static float[] GoldCloseRates =
            {
                1.35308f,
                1.35401f,
                1.35527f,
                1.30104f,
                1.35893f,
                1.35201f,
            };
        private readonly static float[] SilverOpenRates =
            {
                1.15308f,
                1.15401f,
                1.15527f,
                1.10104f,
                1.15893f,
                1.15201f,
            };
        private readonly static float[] SilverCloseRates =
            {
                1.12371f,
                1.15481f,
                1.14997f,
                1.15194f,
                1.13853f,
                1.14361f,
            };
        private readonly static float[] OilOpenRates =
            {
                73.89f,
                73.85f,
                73.80f,
                73.95f,
                74.00f,
                74.05f,
            };
        private readonly static float[] OilCloseRates =
            {
                73.79f,
                73.83f,
                73.82f,
                73.71f,
                74.25f,
                74.69f,
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
                    index = rnd.Next(0, CurrencyOpenRates.Length);
                    break;
                case Instrument.Gold:
                    index = rnd.Next(0, GoldOpenRates.Length);
                    break;
                case Instrument.Silver:
                    index = rnd.Next(0, SilverOpenRates.Length);
                    break;
                case Instrument.Oil:
                    index = rnd.Next(0, OilOpenRates.Length);
                    break;
            }
            return index;
        }

        /// <summary>
        /// получаем случайную котировку
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static float RandomOpenCoteValue(Instrument inst)
        {
            float coteValue = 0.00f;
            switch (inst)
            {
                case Instrument.Currency:
                    coteValue = CurrencyOpenRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Gold:
                    coteValue = GoldOpenRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Silver:
                    coteValue = SilverOpenRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Oil:
                    coteValue = OilOpenRates[GetRandomIndex(inst)];
                    break;
            }
            return coteValue;
        }

        public static float RandomCloseCoteValue(Instrument inst)
        {
            float coteValue = 0.00f;
            switch (inst)
            {
                case Instrument.Currency:
                    coteValue = CurrencyCloseRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Gold:
                    coteValue = GoldCloseRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Silver:
                    coteValue = SilverCloseRates[GetRandomIndex(inst)];
                    break;
                case Instrument.Oil:
                    coteValue = OilCloseRates[GetRandomIndex(inst)];
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
