using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropsIActorRef
{
    /*
     * Рассматриваются пути создания акторов и отправлять им сообщения
     * IActorRef - ссылка на актор
     * Мы не общаемся напрямую с акторами, отправляем сообщения через IActorRef
     * но доставляет сообщения ActorSystem
     * Какой плюс - нам безразлично на то, где находится актор, и какая
     * машина его обрабатывает (если речь об удаленных акторах)
     * Доставка сообщения - проблема ActorSystem
     * 
     * Актор топ-уровня обращается напрямую к ActorSystem:
     * IActorRef myFirstActor = MyActorSystem.ActorOf(....)
     * 
     * Создание дочерних акторов:
       class MyActorClass : UntypedActor
       {
	        protected override void PreStart()
            {
		        IActorRef myFirstChildActor = Context.ActorOf(Props.Create(() =>
                new MyChildActorClass()), "myFirstChildActor");
	        }
        }
     * 
     * НЕЛЬЗЯ вызывать new MyActorClass() вне Props и ActorSystem
     * 
     * Все акторы имеют адрес - ActorPath, который представляет их местоположение
     * в иерархии, можно управлять ими (получив IActorRef через ActorPath)
     * 
     * Задавать имя актора не обязательно, но его имя используется в логах и
     * идентифицирует актор, может помочь при отладке. Принятно давать имена акторам
     * 
     * Context - свойство которое содержит каждый актор. Хранит метаданные текущего
     * состояния актора. Parent, Children, Sender - то что мы можем использовать
     * 
     * Props - конфигурационный класс для создания акторов. Могут содержать 
     * дополнительне данные. Объект сериализуемый, может развертываться на удаленной
     * машине
     * 
     * НЕЛЬЗЯ вызывать Props через new Props(...)
     * Способы создания Props:
     * 1. Props props1 = Props.Create(typeof(MyActor));
     * 2. Props props2 = Props.Create(() => new MyActor(..), "...");
     * 3. Props props3 = Props.Create<MyActor>();
     */
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
            // make an actor system 
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");

            Props validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
            IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, "validationActor");

            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            // tell console reader to begin
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
}
