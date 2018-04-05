using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSystem
{
    /// <summary>
    /// позиция в трейд-системе
    /// </summary>
    public class Position
    {
        #region Fields
        private float _price;
        #endregion
        #region Constructors
        public Position(
            Account account, 
            Instrument instrument, 
            float lot, 
            float lotNumber, 
            float currentCote,
            float newCote)
        {
            Account = account;
            Instrument = instrument;
            Lot = lot;
            LotNumber = lotNumber;
            CurrentCote = currentCote;
            NewCote = newCote;
        }
        #endregion
        #region Props
        /// <summary>
        /// аккаунт для которого открыта дання позиция
        /// </summary>
        public Account Account { get; private set; }
       
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
        public float Price
        {
            get
            {
                return _price;
            }
            private set
            {
                _price = Lot * LotNumber;
            }
        }

        /// <summary>
        /// котировка на момент открытия позиции
        /// </summary>
        public float CurrentCote { get; private set; }

        /// <summary>
        /// котировка на момент закрытия позиции
        /// </summary>
        public float NewCote { get; private set; }
        #endregion
    }
}
