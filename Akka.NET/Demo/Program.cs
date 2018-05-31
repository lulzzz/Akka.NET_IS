using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Akka;
using Akka.Actor;
/*
    Все типы акторов имеют наследуют класс UntypedActor
     
*/

namespace Demo
{
    // тип сообщения для актора
   

    public class GreetingActor : ReceiveActor
    {
        public GreetingActor()
        {
            Receive<Greet>(greet => GreetHandler(greet));
        }

        #region Messages
        public class Greet
        {
            public string Who { get; private set; }
            public Greet(string who)
            {
                Who = who;
            }
        }
        #endregion

        #region Handlers
        private void GreetHandler(Greet g)
        {
            Console.WriteLine("Hello {0}", g.Who);
        }
        #endregion
    }




    class Program
    {
        static void Main(string[] args)
        {
            /*
             * Здесь мы создаем систему акторов. Класс ActorSystem
             * должен создаваться один на сборку, т.к. это "тяжелый" класс 
             */
            ActorSystem actorSystem = ActorSystem.Create("MyActorSystem");

            /*
             * Получаем ссылку на систему акторов, которые работают с 
             * GreetingActor сообщениями
             */
            var greeter = actorSystem.ActorOf<GreetingActor>("greeter");
            
            // посылаем сообщение актору
            greeter.Tell(new Greet("One"));
            greeter.Tell(new Greet("Two"));
            greeter.Tell(new Greet("Three"));
            Console.ReadLine();
        }
    }
}
