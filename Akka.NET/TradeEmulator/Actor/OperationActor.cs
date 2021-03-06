﻿using System;
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
            Receive<ReturnActorMessage>(ropm => ReturnActorMessageHandler(ropm));
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

        public class ReturnActorMessage
        {
            public Account Account { get; private set; }
            public IActorRef Actor { get; private set; }
            public ReturnActorMessage(Account acc)
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
        private void ReturnActorMessageHandler(ReturnActorMessage ram)
        {
            Context.Parent.Tell(new AccountDeskActor.ReceiveAccountMessage(ram.Account, Self));
        }

        #endregion

    }
}
