using Akka.Actor;
using System;
using System.Windows.Forms;

namespace TradeSystem
{
    static class Program
    {
        /// <summary>
        /// Система акторов
        /// </summary>
        public static ActorSystem TradeSystemActors;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            TradeSystemActors = ActorSystem.Create("TradeActors");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
