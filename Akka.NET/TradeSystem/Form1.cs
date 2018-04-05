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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // закрытие супервизора
            tradeActor.Tell(PoisonPill.Instance);
            // закрытие системы акторов
            Program.TradeSystemActors.Terminate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tradeActor = Program.TradeSystemActors.ActorOf(Props.Create(()=> new AccountDesk(dataGridView1, pauseButton)), "tradeActor");
            tradeActor.Tell(new AccountDesk.InitializeDGV(null));
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {

        }
    }
}
