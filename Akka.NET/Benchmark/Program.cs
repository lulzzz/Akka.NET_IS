using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public class StartMessage
    {
        public string Data { get; private set; }
        public StartMessage(string msg)
        {
            Data = msg;
        }
    }
    public class BenchmarkActor : ReceiveActor
    {
        Stopwatch sw = new Stopwatch();
        bool isStarted = false;
        
        public BenchmarkActor()
        {
            Receive<StartMessage>(x => HandleMessage(x));
        }

        void HandleMessage(StartMessage sm)
        {
            if(!isStarted)
            {
                sw.Start();
                isStarted = true;
            }
            Console.WriteLine("Received {0}", sm.Data);
            if (sm.Data.Equals("499"))
            {
                sw.Stop();
                Console.WriteLine("Time taken: {0}", sw.ElapsedMilliseconds);
            }
        }


    }
    class Program
    {
        static void Main(string[] args)
        {
            ActorSystem actorSystem = ActorSystem.Create("test");
            IActorRef benchActor = actorSystem.ActorOf(Props.Create(() => new BenchmarkActor()));
            int cnt = 0;
            for (int i = 0; i < 500; i++)
            {
                string message = cnt++.ToString();
                benchActor.Tell(new StartMessage(message));
            }
            Console.ReadLine();
        }
    }
}
