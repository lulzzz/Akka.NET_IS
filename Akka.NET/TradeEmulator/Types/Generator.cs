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
                1303.13f,
                1304.35f,
                1325.59f,
                1301.78f,
                1398.21f,
                1302.56f,
            };
        private readonly static float[] GoldCloseRates =
            {
                1353.08f,
                1354.01f,
                1355.27f,
                1301.04f,
                1358.93f,
                1352.01f,
            };
        private readonly static float[] SilverOpenRates =
            {
                17.11f,
                17.20f,
                16.95f,
                16.63f,
                16.60f,
                16.63f,
            };
        private readonly static float[] SilverCloseRates =
            {
                16.66f,
                16.51f,
                16.49f,
                16.28f,
                16.46f,
                16.53f,
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


        /*
           размеры лотов для инструментов
           
           Валютные операции:
           1.00f, //  10000  у.е.
           0.05f, //   500
           0.04f, //   400
           0.03f, //   300
           0.02f, //   200
           0.01f //    100
           Размер позиции = Котировка * (Размер_Лота * Количество_лотов)

           Золото:
           1.00f, //    100
           0.05f, //      5 
           0.04f, //      4 
           0.03f, //      3 
           0.02f, //      2
           0.01f //       1 тройская унция (ту) = 31,1034768 гр

           Серебро:
           1.00f, //    1000 
           0.05f, //      50 
           0.04f, //      40 
           0.03f, //      30 
           0.02f, //      20 
           0.01f //       10
           
           Нефть:
           1.00f, //    100 баррелей 
           0.05f, //      50 
           0.04f, //      40 
           0.03f, //      30 
           0.02f, //      20 
           0.01f //       10
         */
        private readonly static float[] LotSize =
            {
                1.00f,
                0.05f,
                0.04f,
                0.03f,
                0.02f,
                0.01f
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

        /// <summary>
        /// получаем индекс в массиве котировок в зависимости от инструмента
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
        public static float RandomQuoteValue(Instrument inst, PositionState positionState)
        {
            float quote = 0.00f;
            if (positionState == PositionState.Open)
            {
                switch (inst)
                {
                    case Instrument.Currency:
                        quote = CurrencyOpenRates[GetRandomIndex(inst)];
                        break;
                    case Instrument.Gold:
                        quote = GoldOpenRates[GetRandomIndex(inst)];
                        break;
                    case Instrument.Silver:
                        quote = SilverOpenRates[GetRandomIndex(inst)];
                        break;
                    case Instrument.Oil:
                        quote = OilOpenRates[GetRandomIndex(inst)];
                        break;
                }
            }
            if (positionState == PositionState.Close)
            {
                switch (inst)
                {
                    case Instrument.Currency:
                        quote = CurrencyCloseRates[GetRandomIndex(inst)];
                        break;
                    case Instrument.Gold:
                        quote = GoldCloseRates[GetRandomIndex(inst)];
                        break;
                    case Instrument.Silver:
                        quote = SilverCloseRates[GetRandomIndex(inst)];
                        break;
                    case Instrument.Oil:
                        quote = OilCloseRates[GetRandomIndex(inst)];
                        break;
                }
            }
            return quote;
        }

        /// <summary>
        /// получаем размер лота (валюта, драгмет, топливо)
        /// </summary>
        /// <param name="inst"></param>
        /// <returns></returns>
        public static float GetRandomLotSize(Instrument inst)
        {
            return LotSize[GetRandomIndex(inst)];
        }
    }
}
