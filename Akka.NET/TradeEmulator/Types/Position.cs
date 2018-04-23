using System;
using TradeEmulator.Types;

namespace TradeEmulator
{
    /// <summary>
    /// позиция в трейд-системе
    /// </summary>
    public class Position
    {
        #region Constructors

        public Position(
            Instrument instrument,
            float lotNumber,
            float quote)
        {
            Instrument = instrument;
            LotNumber = lotNumber;
            QuoteOnOpenPosition = quote;
            PositionPrice = ComputePrice(Instrument);
            PositionState = PositionState.Idle;
            GetLotFromLotSize();
        }

        #endregion

        #region Props

        /// <summary>
        /// котировка по время открытия позиции
        /// </summary>
        public float QuoteOnOpenPosition { get; private set; }

        /// <summary>
        /// котировка во время закрытия позиции
        /// </summary>
        public float QuoteOnClosePosition { get; set; }

        /// <summary>
        /// состояние позиции
        /// </summary>
        public PositionState PositionState { get; set; }

        /// <summary>
        /// инструмент
        /// </summary>
        public Instrument Instrument { get; private set; }
        
        /// <summary>
        /// лот
        /// </summary>
        public float Lot { get; private set; }

        /// <summary>
        /// количество лотов
        /// </summary>
        public float LotNumber { get; private set; }

        /// <summary>
        /// Цена позиции
        /// </summary>
        public float PositionPrice { get; private set; }

        #endregion

        #region Methods
        
        /// <summary>
        /// вычисляем цену в зависимости от инструмента
        /// </summary>
        public float ComputePrice(Instrument inst)
        {
            float local_price_var = 0.00f;
            switch (inst)
            {
                case Instrument.Currency:
                    local_price_var = LotNumber * 10000 * QuoteOnOpenPosition;
                    break;
                case Instrument.Silver:
                    local_price_var = LotNumber * 1000 * QuoteOnOpenPosition;
                    break;
                case Instrument.Gold:
                case Instrument.Oil:
                    local_price_var = LotNumber * 100 * QuoteOnOpenPosition;
                    break;   
            }
            return local_price_var;            
        }


        /// <summary>
        /// пытаемся закрыть позицию, возвращаем профит / лосс аккаунту и возвращаем его
        /// </summary>
        /// <param name="closingQuote"></param>
        /// <returns></returns>
        //public void ClosePosition(float closingQuote)
        //{
        //    // котировка при закрытии
        //    QuoteOnClosePosition = closingQuote;
        //    // получаем цену по новой котировке
        //    float ClosingPrice = ComputePrice(Instrument);
        //    // разница
        //    float Delta = ClosingPrice - PositionPrice;
        //    // возвращаем деньги на счет аккаунта
        //    PutMoney((decimal)ClosingPrice);
        //    // если профит
        //    if (Delta > 0)
        //    {
        //        Console.BackgroundColor = ConsoleColor.Green;
        //        Console.WriteLine("Аккаунт: {0} позиция закрыта, прибыль: +{1}", AccountId, Delta);
        //        Console.ResetColor();
        //    }
        //    // если loss
        //    if (Delta < 0)
        //    {
        //        Console.BackgroundColor = ConsoleColor.DarkYellow;
        //        Console.WriteLine("Аккаунт: {0} позиция закрыта, убыток: -{1}", AccountId, Delta);
        //        Console.ResetColor();
        //    }
        //    // фиксируем закрытие позиции
        //    PositionState = PositionState.Close;
        //}

        private void GetLotFromLotSize()
        {
            switch (Instrument)
            {
                case Instrument.Currency:
                    Lot = LotNumber * 10000;
                    break;
                case Instrument.Silver:
                    Lot = LotNumber * 1000;
                    break;
                case Instrument.Gold:
                case Instrument.Oil:
                    Lot = LotNumber * 100;
                    break;
            }
        }

        #endregion
    }
}
