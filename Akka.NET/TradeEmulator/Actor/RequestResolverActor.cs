using Akka.Actor;
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
            accountDeskActor.Tell(new AccountDeskActor.GenerateAccountMessage(10000));
            // аккаунты открывают позиции
            accountDeskActor.Tell(new AccountDeskActor.OperationActorOpenMessage());
        }

        #region Messages

        #endregion

        #region Handlers

        #endregion
    }
}
