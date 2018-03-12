﻿using Akka.Actor;

namespace Lifecycle
{
    /*
     * Жизненный цикл акторов
     * Есть следующие состояния:
     * Starting
     * Receiving
     * Stopping
     * Terminated
     * Restarting
     * 
     * Методы
     * PreStart - между Starting и Receiving
     * PreRestart - если актор упал
     * PostStop - если актор остановился и больше не получает сообщения
     * PostRestart - вызывается во время рестарта после PreRestart но до PreStart
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

            Props fileValidatorActorProps = Props.Create(() => new FileValidatorActor(consoleWriterActor));
            IActorRef fileValidatorActor = MyActorSystem.ActorOf(fileValidatorActorProps, "validationActor");
            
            Props consoleReaderProps = Props.Create<ConsoleReaderActor>();
            IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, "consoleReaderActor");

            // begin processing
            consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

            // blocks the main thread from exiting until the actor system is shut down
            MyActorSystem.WhenTerminated.Wait();
        }

    }
}
