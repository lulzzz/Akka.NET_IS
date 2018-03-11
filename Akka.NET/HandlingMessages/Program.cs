using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace HandlingMessages
{
    /*
     * Определение собственных типов сообщений и их обработка
     * Если актор получает сообщение которое не знает как обработать, он его игнорирует
     */
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация
            ActorSystem MyActorSystem = ActorSystem.Create("MyActorSystem");

            // создание акторов
            IActorRef consoleWriteActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(consoleWriteActor)));

            // отправка сообщения из актора consoleReadActor в актор consoleWriteActor 
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);
            MyActorSystem.WhenTerminated.Wait();
        }

    }
}

