using Akka.Actor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeSystem
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Главный трейдинг-актор (супервизор)
        /// </summary>
        private IActorRef tradeActor;

        public Form1()
        {
            InitializeComponent();          
        }

       



        private async Task LoadData()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                    Accounts.Add(new AccountDesk(50.000m, Instrument.Currency, 0.0f, 0.0f, currentCote));
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // закрытие супервизора
            tradeActor.Tell(PoisonPill.Instance);
            // закрытие системы акторов
            Program.TradeSystemActors.Terminate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tradeActor = Program.TradeSystemActors.ActorOf(Props.Create()=> new AccountDesk()
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }
    }
}
