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
            Money = ValidMoney(money);
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Аккаунт {0} нельзя открыть позицию. Цена {1} > средств на счете {2}", Id, Position.PositionPrice, Money);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// положить деньги на счет
        /// </summary>
        /// <param name="money"></param>
        public void PutMoney(decimal money)
        {
            Money += ValidMoney(money);
        }

        /// <summary>
        /// снять деньги со счета
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public decimal GetMoney(decimal value)
        {
            if (ValidMoney(value) > Money)
            {
                Console.WriteLine("Ошибка. Аккаунт {0}: Введенная сумма для снятия({1}) больше суммы на счету ({2})", Id, value, Money);
                return -1;
            }
            else return Money -= value;
        }

        /// <summary>
        /// защита от дурака (отрицательные деньги)
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        private decimal ValidMoney(decimal money)
        {
            return money > 0 ? money : Math.Abs(money);
        }

        #endregion
    }
}
