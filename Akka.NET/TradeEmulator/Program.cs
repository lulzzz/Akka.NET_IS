using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TradeEmulator.AccountDeskActor;

namespace TradeEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem TradeEmulatorActor = ActorSystem.Create("TradeActorSystem");
            IActorRef AccountDeskActor = TradeEmulatorActor.ActorOf(Props.Create(() => new AccountDeskActor()));
            AccountDeskActor.Tell(new GanerateAccount(10000));
            Console.ReadLine();
        }
    }
}
