using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Akka.Actor;
using Akka.Event;
using System.Collections.Immutable;

namespace GameCore
{
   
    class Program
    {
        static void Main(string[] args)
        {
            var system = ActorSystem.Create("TurnstileSimulator");
            var turnstileActor = system.ActorOf<TurnstileActor>("Turnstile");

            // пытаемся пройти - монет нет
            turnstileActor.Tell(new PushBar());
            // вставляем монету
            turnstileActor.Tell(new InsertCoin());
            // проходим
            turnstileActor.Tell(new PushBar());
            // пытается пройти заяц
            turnstileActor.Tell(new PushBar());

            Console.ReadLine();
        }
    }
}
