using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Types;

namespace TradeEmulator.Actor
{
    /// <summary>
    /// Актор закрытия позиций
    /// </summary>
    public class ClosePositionActor : ReceiveActor
    {
        #region Fields
        
        /// <summary>
        ///  акторя снятия комиссий
        /// </summary>
        private IActorRef TransactionFee;
        
        #endregion
        #region Constructors

        public ClosePositionActor()
        {
            Receive<ClosePosition>(cp => ClosePositionHandler(cp));
            Receive<AccountFromTransactionFee>(tf => AccountFromTransactionFeeHandler(tf));
            TransactionFee = Context.ActorOf(Props.Create(() => new TransactionFeeActor()));
        }

        #endregion

        #region Messages
        
        /// <summary>
        /// сообщение закрыть позицию
        /// </summary>
        public class ClosePosition
        {
            public Account Account { get; private set; }
            public ClosePosition(Account acc)
            {
                Account = acc;
            }
        }

        /// <summary>
        /// сообщение получение аккаунта после вычета комиссии
        /// </summary>
        public class AccountFromTransactionFee
        {
            public Account Account { get; private set; }
            public AccountFromTransactionFee(Account account)
            {
                Account = account;
            }
        }

        #endregion

        #region Handlers
        /// <summary>
        /// Обработка сообщения закрыть позицию
        /// </summary>
        /// <param name="cp"></param>
        private void ClosePositionHandler(ClosePosition cp)
        {
            Position position = cp.Account.Position;
            
            // получаем котировку закрытия
            float quote = Generator.RandomQuoteValue(position.Instrument, PositionState.Close);

            // закрываем позицию
            cp.Account.TryClosePosition(quote);
            
            // отправляем аккаунт для снятия комиссии
            TransactionFee.Tell(new TransactionFeeActor.TransactionFee(cp.Account));            
        }

        private void AccountFromTransactionFeeHandler(AccountFromTransactionFee tf)
        {
            // возвращаем аккаунт в OperationActor
            Sender.Tell(new OperationActor.ReturnActorMessage(tf.Account));
        }

        #endregion
    }
}
