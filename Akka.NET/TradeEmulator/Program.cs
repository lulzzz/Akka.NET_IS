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
