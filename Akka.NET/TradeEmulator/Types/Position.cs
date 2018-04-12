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
            float lot,
            float lotNumber,
            float cote)
        {
            Account = account;
            Instrument = instrument;
            Lot = lot;
            LotNumber = lotNumber;
            ComputePrice();
        }

        #endregion

        #region Props
        
        /// <summary>
        /// аккаунт для которого открыта дання позиция
        /// </summary>
        private Account Account { get; }

        public float CoteOnOpenPostion { get; private set; }
        public float CoteOnClosePosition { get; private set; }

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
        /// состояние позиции (закрыто / открыто)
        /// </summary>
        //public PositionState PositionState { get; private set; }
        
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

        /// <summary>
        /// котировка на момент открытия позиции
        /// </summary>
        public float Cote { get; private set; }

        #endregion

        #region Methods
        /// <summary>
        /// вычисляем цену
        /// </summary>
        private void ComputePrice()
        {
            Price = Lot * LotNumber;
        }
        #endregion
    }
}
