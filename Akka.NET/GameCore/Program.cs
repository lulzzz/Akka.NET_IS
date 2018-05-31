using System;
using Akka.Actor;

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
