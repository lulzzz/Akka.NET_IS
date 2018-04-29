using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Types;

namespace TradeEmulator
{
    /// <summary>
    /// Уведомление
    /// </summary>
    public class NotifyActor : ReceiveActor
    {
        #region Fields

        private static Stopwatch stopwatch = new Stopwatch();
        public static int counter = 0;
        private int accountsCount;

        #endregion

        #region Constructors
        public NotifyActor()
        {
            Receive<AccountCounterMessage>(c => AccountCounterHandler());
            Receive<InitMessage>(im => InitMessageHandler(im));
        }
        #endregion

        #region Messages
        
        /// <summary>
        ///  сообщение подсчета аккаунтов которые закрыли позиции
        /// </summary>
        public class AccountCounterMessage { }

        /// <summary>
        /// сюда передаем количество созданных аккаунтов из AccountDeskActor
        /// одновременно включаем таймер отчета времени
        /// </summary>
        public class InitMessage
        {
            public int AccountCount { get; private set; }
            public InitMessage(int accountCount)
            {
                AccountCount = accountCount;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// обработка сообщения AccountCounterMessage
        /// </summary>
        private void AccountCounterHandler()
        {
            counter++;
            if (counter == accountsCount)
            {
                stopwatch.Stop();
                Console.WriteLine("Обработано {0} позиций, время {1} мс.", counter, stopwatch.ElapsedMilliseconds);
            }
        }

        private void InitMessageHandler(InitMessage im)
        {
            accountsCount = im.AccountCount;
            stopwatch.Start();
        }
        #endregion
    }
}
