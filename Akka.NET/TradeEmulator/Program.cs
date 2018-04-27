using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Actor;
using static TradeEmulator.AccountDeskActor;

namespace TradeEmulator
{
    /* TODO
     Замерять время не одной позиции а конкретного количества позиций, например 10000
     Позиции из бд не удаляются после закрытия
     */
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem TradeEmulatorActor = ActorSystem.Create("TradeActorSystem");
            IActorRef RequestResolver = TradeEmulatorActor.ActorOf(Props.Create(() => new RequestResolverActor()));
            Console.ReadLine();
        }
    }
}
