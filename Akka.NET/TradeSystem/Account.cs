using System;
using System.Windows.Forms;

namespace TradeSystem
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
        private static int accuontCounter = 0;

        /// <summary>
        /// Id данного аккаунта
        /// </summary>
        private int currentId = 0;
        #endregion
        #region Constructors
        public Account(decimal money)
        {
            Money = ValidMoney(money);
            currentId = ++accuontCounter;
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

        /// <summary>
        /// Денежный счет
        /// </summary>
        public decimal Money { get; private set; }
        #endregion
        #region Methods
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
        /// <param name="money"></param>
        /// <returns></returns>
        public decimal GetMoney(decimal money)
        {
            if (ValidMoney(money) > Money)
                MessageBox.Show("Недостаточно средств на счете", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else return Money -= money;
            return 0;
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
