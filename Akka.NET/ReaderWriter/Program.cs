using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace ReaderWriter
{
    /*
     * Использование Props создания акторов
     * Создаем акторы ввода и вывода на консоль
     */
    class Program
    {
        static void Main(string[] args)
        {
            // инициализация
            ActorSystem MyActorSystem = ActorSystem.Create("MyActorSystem");

            // вывод меню
            PrintInstructions();

            // создание акторов
            IActorRef consoleWriteActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleWriterActor()));
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(Props.Create(() => new ConsoleReaderActor(consoleWriteActor)));

            // отправка сообщения из актора consoleReadActor в актор consoleWriteActor 
            consoleReaderActor.Tell("start");
            MyActorSystem.WhenTerminated.Wait();
        }
        private static void PrintInstructions()
        {
            Console.WriteLine("Write whatever you want into the console!");
            Console.Write("Some lines will appear as");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write(" red ");
            Console.ResetColor();
            Console.Write(" and others will appear as");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(" green! ");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Type 'exit' to quit this application at any time.\n");
        }
    }
}
