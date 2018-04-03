using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeSystem
{
    /// <summary>
    /// Класс аккаунт
    /// </summary>
    public class Account
    {
        #region Constructors
        public Account()
        {

        }
        #endregion
        #region Fields
        /// <summary>
        /// счетчик объектов класса
        /// </summary>
        private static int AccuontCounter = 0;

        /// <summary>
        /// текущий Id
        /// </summary>
        private int currentId = 0;
        #endregion

        #region Props
        /// <summary>
        /// возврат Id аккаунта
        /// </summary>
        public int Id
        {
            get { return currentId; }
        }

        /// <summary>
        /// Денежный счет
        /// </summary>
        public decimal Value { get; private set; }

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
