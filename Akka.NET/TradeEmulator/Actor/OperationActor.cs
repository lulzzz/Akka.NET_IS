using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using TradeEmulator.Types;

namespace TradeEmulator.Actor
{
    /// <summary>
    /// Общий актор для обработки Dictionary<IActorRef, Account></IActorRef>
    /// Т.к. словарь один, но актора два (закрыть / открыть позицию)
    /// выбираем соответствующее действие
    /// </summary>
    public class OperationActor : ReceiveActor
    {
        #region Fields
        private IActorRef openPositionActor;
        private IActorRef closePositionActor;
        #endregion

        #region Constructors
        public OperationActor()
        {
            Receive<OperationActorMessage>(oam => OperationActorMessageHandler(oam));
            Receive<ReturnOpenPositionMessage>(ropm => ReturnOpenPositionMessageHandler(ropm));
            Receive<ReturnClosePositionMessage>(rcpm => ReturnClosePositionMessageHandler(rcpm));
        }
        #endregion

        #region Messages

        /// <summary>
        /// сообщение для выбора актора в зависимости от операции
        /// открыть - запуск актора открытия позиции
        /// закрыть - запуск актора закрытия позиции
        /// </summary>
        public class OperationActorMessage
        {
            public ActorOperationType ActorOperationType { get; private set; }
            public Account Account { get; private set; }
            public OperationActorMessage(ActorOperationType aot, Account acc)
            {
                ActorOperationType = aot;
                Account = acc;
            } 
        }

        /// <summary>
        /// возврат аккаунта с открытой позицией
        /// </summary>

        public class ReturnOpenPositionMessage
        {
            public Account Account { get; private set; }
            public IActorRef Actor { get; private set; }
            public ReturnOpenPositionMessage(Account acc)
            {
                Account = acc;
            }
        }

        public class ReturnClosePositionMessage
        {
            public Account Account { get; private set; }
            public IActorRef Actor { get; private set; }
            public ReturnClosePositionMessage(Account acc)
            {
                Account = acc;
            }
        }
        #endregion

        #region Handlers

        /// <summary>
        ///  обработчик OperationActorMessage
        /// </summary>
        /// <param name="oam"></param>
        private void OperationActorMessageHandler(OperationActorMessage oam)
        {
            switch(oam.ActorOperationType)
            {
                case ActorOperationType.Open:
                    openPositionActor = Context.ActorOf(Props.Create(() => new OpenPositionActor()));
                    openPositionActor.Tell(new OpenPositionActor.OpenPositionMessage(oam.Account));
                    break;
                case ActorOperationType.Close:
                    
                    closePositionActor = Context.ActorOf(Props.Create(() => new ClosePositionActor()));
                    closePositionActor.Tell(new ClosePositionActor.ClosePosition(oam.Account));
                   break;
            }
        }

        /// <summary>
        /// обработка сообщения ReturnAccountMessage
        /// </summary>
        /// <param name="ram"></param>
        private void ReturnOpenPositionMessageHandler(ReturnOpenPositionMessage ropm)
        {
            Context.Parent.Tell(new AccountDeskActor.ReceiveAccountOpenMessage(ropm.Account, Self));
        }

        private void ReturnClosePositionMessageHandler(ReturnClosePositionMessage rcpm)
        {
            Context.Parent.Tell(new AccountDeskActor.ReceiveAccountCloseMessage(rcpm.Account, Self));
        }
        #endregion

    }
}
