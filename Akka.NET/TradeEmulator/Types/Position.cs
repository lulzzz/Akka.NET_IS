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
            PositionPrice = ComputePrice(Instrument, PositionState.Open);
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
        public float ComputePrice(Instrument inst, PositionState ps)
        {
            float local_price_var = 0.00f;
            if (ps == PositionState.Open)
            {
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
            }
            if (ps == PositionState.Close)
            {
                switch (inst)
                {
                    case Instrument.Currency:
                        local_price_var = LotNumber * 10000 * QuoteOnClosePosition;
                        break;
                    case Instrument.Silver:
                        local_price_var = LotNumber * 1000 * QuoteOnClosePosition;
                        break;
                    case Instrument.Gold:
                    case Instrument.Oil:
                        local_price_var = LotNumber * 100 * QuoteOnClosePosition;
                        break;
                }
            }
            return local_price_var;            
        }



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
