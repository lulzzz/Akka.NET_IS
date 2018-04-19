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
            Account account,
            Instrument instrument,
            PositionState positionState,
            float lot,
            float lotNumber,
            float openCote,
            float closeCote)
        {
            PositionValid = false;
            Account = account;
            Instrument = instrument;
            PositionState = positionState;
            Lot = lot;
            LotNumber = lotNumber;
            CoteOnOpenPostion = openCote;
            CoteOnClosePosition = closeCote;
            ComputePrice();
        }

        #endregion

        #region Props
        
        /// <summary>
        /// аккаунт для которого открыта дання позиция
        /// </summary>
        private Account Account { get; }

        /// <summary>
        /// котировка по время открытия позиции
        /// </summary>
        public float CoteOnOpenPostion { get; private set; }

        /// <summary>
        /// котировка во время закрытия позиции
        /// </summary>
        public float CoteOnClosePosition { get; private set; }

        /// <summary>
        /// флаг что позиция валидная для открытия / закрытия,
        /// т.е. проверяем хватает денег на счету аккаунта
        /// для открытия позиции или закрытия при loss
        /// </summary>
        public bool PositionValid { get; private set; }

        /// <summary>
        /// состояние позиции
        /// </summary>
        public PositionState PositionState { get; private set; }
        
        /// <summary>
        /// возвращает Id аккаунта
        /// </summary>
        public int AccountId
        {
            get
            {
                return Account.Id;
            }
        }

        /// <summary>
        /// получение средств на счету
        /// </summary>
        public decimal AccountMoney
        {
            get
            {
                return Account.Money;
            }
        }

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
        /// Цена
        /// </summary>
        public float Price { get; private set; }

        #endregion

        #region Methods
        
        /// <summary>
        /// вычисляем цену и валидируем позицию
        /// </summary>
        private void ComputePrice()
        {
            Price = Lot * LotNumber;
            if ((decimal)Price <= AccountMoney)
            {
                Account.GetMoney((decimal)Price);
                PositionValid = true;
            }
            else if ((decimal)Price > AccountMoney)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Аккаунт {0} не может открыть позицию. Цена {1} > средств на счете {2}", AccountId, Price, AccountMoney);
            }
        }

        #endregion
    }
}
