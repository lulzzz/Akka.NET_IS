using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChildHierarchiesSupervision
{
    /*
     * Дочерние акторы, иерархия акторов, супернадзор
     * Supervision - концепция, в которой над каждым актором есть актор выше,
     * который "наблюдает" за ним
     * Актор "/" - главный "надзорщик" Следит за актором "/system" и "/user"
     * "/system" - следит за системой
     * "/user" - создаваемые пользователем
     * 
     * Создание top-level акторов:
     * IActorRef a1 = MyActorSystem.ActorOf(Props.Create<BasicActor>(), "a1");
     * IActorRef a2 = MyActorSystem.ActorOf(Props.Create<BasicActor>(), "a2");
     * 
     * Создание дочерних акторов:
     * IActorRef b1 = Context.ActorOf(Props.Create<BasicActor>(), "b1");
     * IActorRef b2 = Context.ActorOf(Props.Create<BasicActor>(), "b2");
     * 
     * Адрес актора = позиция актора в иерархии
     * akka.tcp://MySystem@localhost:9001/user/actorName1
     * akka.tcp:// - протокол
     * MySystem - система акторов
     * localhost:9001 - адрес
     * user/actorName1 - путь
     * 
     * Как родитель отвечает на ошибки возникающие в дочерних акторах
     * 1. Дочерний актор упал, выбросил Exception в Failure сообщении
     * 2. Родитель выполняет одну из директив, которая определяется SupervisionStrategy
     * 
     * Supervision директивы:
     * Restart - перезапуск дочернего актора (по умолчанию)
     * Stop - навсегда останавливает дочерний актор
     * Escalate - родитель не знает что делать и спрашивает у актора выше по иерархии
     * Resume - игнор проблемы
     */
    class Program
    {
        public static ActorSystem MyActorSystem;
        static void Main(string[] args)
        {
            // make actor system 
            MyActorSystem = ActorSystem.Create("MyActorSystem");

            // create top-level actors within the actor system
            Props consoleWriterProps = Props.Create<ConsoleWriterActor>();
            IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, "consoleWriterActor");

            Props tailCoordinatorProps = Props.Create(() => new TailCoordinatorActor());
            IActorRef tailCoordinatorActor = MyActorSystem.ActorOf(tailCoordinatorProps, "tailCoordinatorActor");

            Props fileValidatorActorProps = Props.Create(() => new FileValidatorActor(consoleWriterActor, tailCoordinatorActor));
            IActorRef fileValidatorActor = MyActorSystem.ActorOf(fileValidatorActorProps, "validationActor");

            Props consoleReaderProps = Props.Create<ConsoleReaderActor>(fileValidatorActor);
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            // begin processing
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }
    }
}
