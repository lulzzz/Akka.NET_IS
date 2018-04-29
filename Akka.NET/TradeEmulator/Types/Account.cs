using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEmulator.Types
{
    /// <summary>
    /// Класс аккаунт
    /// </summary>
    public class Account
    {
        #region Fields

        /// <summary>
        /// счетчик объектов класса
        /// </summary>
        private static int accontCounter = 0;

        /// <summary>
        /// Id конкретного экземпляра класса
        /// </summary>
        private int currentId = 0;

        #endregion

        #region Constructors

        public Account(decimal money)
        {
            Money = ValidateMoney(money);
            currentId = ++accontCounter;
        }

        #endregion

        #region Props

        /// <summary>
        /// возврат Id аккаунта
        /// </summary>
        public int Id
        {
            get { return currentId; }
        }

        public Position Position { get; set; }

        /// <summary>
        /// Денежный счет
        /// </summary>
        public decimal Money { get; private set; }

        #endregion

        #region Methods

        // можем ли открыть позицию?
        public void CanOpenPosition()
        {
            // проверяем счет
            if ((decimal)Position.PositionPrice <= Money)
            {
                // фиксируем что у аккаунта хватило средств для открытия позиции
                Position.PositionState = PositionState.Open;
                GetMoney((decimal)Position.PositionPrice);
            }
            else if ((decimal)Position.PositionPrice > Money)
            {
                //Console.WriteLine("Аккаунт {0} нельзя открыть позицию. Цена {1}USD > средств на счете {2}USD", Id, Position.PositionPrice, Money);
            }
        }

        /// <summary>
        /// положить деньги на счет
        /// </summary>
        /// <param name="value"></param>
        public void PutMoney(decimal value)
        {
            Money += ValidateMoney(value);
            //Console.WriteLine("Аккаунт {0}: Аккаунт пополнен на: {1}USD, остатоr на счете {2}USD", Id, value, Money);
        }

        /// <summary>
        /// снять деньги со счета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void GetMoney(decimal value)
        {
            if (ValidateMoney(value) > Money)
            {
                //Console.WriteLine("Аккаунт {0}: Невозможно снять {1}USD, на счету {2}USD", Id, value, Money);
            }
            else
            {
                Money -= value;
                //Console.WriteLine("Аккаунт {0}: Снято: {1}USD, остаток на счете {2}USD", Id, value, Money);
            }
        }

        /// <summary>
        /// комиссия
        /// </summary>
        /// <param name="value"></param>
        public void GetFee(decimal value)
        {
            if (ValidateMoney(value) > Money)
            {
                //Console.WriteLine("Аккаунт {0}: Невозможно снять комиссию {1}USD, на счету {2}USD", Id, value, Money);
            }
            else
            {
                Money -= value;
                //Console.WriteLine("Аккаунт {0}: Комиссия: {1}USD, остаток на счете {2}USD", Id, value, Money);
            }
        }

        /// <summary>
        /// защита от дурака (отрицательные деньги)
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        private decimal ValidateMoney(decimal money)
        {
            return money > 0 ? money : Math.Abs(money);
        }

        /// <summary>
        /// пытаемся закрыть позицию, возвращаем профит / лосс аккаунту и возвращаем его
        /// </summary>
        /// <param name="closingQuote"></param>
        /// <returns></returns>
        public void TryClosePosition(float closingQuote)
        {
            if (Position.PositionState == PositionState.Open)
            {
                // котировка при закрытии
                Position.QuoteOnClosePosition = closingQuote;

                // получаем цену по новой котировке
                float ClosingPrice = Position.ComputePrice(Position.Instrument, PositionState.Close);

                // разница
                float Delta = ClosingPrice - Position.PositionPrice;

                // если профит
                if (Delta > 0)
                {
                    // возвращаем деньги на счет аккаунта
                    PutMoney((decimal)ClosingPrice);
                    //Console.WriteLine("Аккаунт: {0} позиция закрыта, прибыль: +{1}$", Id, Delta);
                }

                // если loss
                if (Delta < 0)
                {
                    //Console.WriteLine("Аккаунт: {0} позиция закрыта, убыток: {1}", Id, Delta);
                }
                // фиксируем закрытие позиции
                Position.PositionState = PositionState.Close;
            }
        }
        #endregion
    }
}
