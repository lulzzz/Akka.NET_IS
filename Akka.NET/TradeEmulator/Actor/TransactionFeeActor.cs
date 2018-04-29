using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Types;

namespace TradeEmulator.Actor
{
    public class TransactionFeeActor : ReceiveActor
    {
        // комиссия - 5% от позиции незаивисимо от типа
        private readonly float fee = 0.05f;
        

        #region Constructors
        public TransactionFeeActor()
        {
            Receive<TransactionFee>(tf => TransactionFeeHandler(tf));
        }
        #endregion


        #region Messages

        public class TransactionFee
        {
            public Account Account { get; private set; }
            public TransactionFee(Account account)
            {
                Account = account;
            }
        }

        #endregion

        #region Handlers
        private void TransactionFeeHandler(TransactionFee tf)
        {
            // вычисляем стоимость комиссии
            float local_fee = tf.Account.Position.PositionPrice * fee;
            // снимаем деньги
            tf.Account.GetFee((decimal)local_fee);

            // возвращаем аккаунт в ClosePosition
            Sender.Tell(new ClosePositionActor.AccountFromTransactionFee(tf.Account));

            // сообщаем NotifyActor что очередная позиция обработана

            var selectNotifyActoer = Context.ActorSelection("/user/RequestResolverActor/AccountDeskActor/NotifyActor");
            selectNotifyActoer.Tell(new NotifyActor.AccountCounterMessage());
        }
        #endregion
    }
}
