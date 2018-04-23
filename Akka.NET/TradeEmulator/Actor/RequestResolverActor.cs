﻿using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEmulator.Actor
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestResolverActor : ReceiveActor
    {
        #region Fields

        // актор хранения аккаунтов 
        private IActorRef accountDeskActor;
        
        
        #endregion
        public RequestResolverActor()
        {
            accountDeskActor = Context.ActorOf(Props.Create(() => new AccountDeskActor()));
            // сообщения для генераций аккаунтов
            accountDeskActor.Tell(new AccountDeskActor.GenerateAccountMessage(50));
            // аккаунты открывают позиции
            accountDeskActor.Tell(new AccountDeskActor.OpenPositionMessage());
        }
        /*
         * Сообщения
         * Например, открыть позицию - посылает запрос  актору OpenPosition (передаем ему Id)
         * OpenPosition обращается к AccountDesk, чтобы тог вернул ему аккаунт с указанным id
         * на возвращенный id открывается позиция
         * 
         */
        #region Messages

        #endregion

        #region Handlers

        #endregion
    }
}
