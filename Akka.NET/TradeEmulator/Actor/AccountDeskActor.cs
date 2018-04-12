using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Types;

namespace TradeEmulator
{
    public class AccountDeskActor : ReceiveActor
    {
        #region Fields

        private IActorRef OpenPosition;
        private IActorRef ClosePosition;
        public List<Account> Accounts { get; private set; }

        #endregion
        public AccountDeskActor()
        {
            OpenPosition = Context.ActorOf(Props.Create<AccountDeskActor>(), "OpenPositionActor");
            ClosePosition = Context.ActorOf(Props.Create<AccountDeskActor>(), "ClosePositionActor");
            Accounts = new List<Account>();
            Receive<GanerateAccount>(sm => GenerateAccoutsHandler(sm));
            
        }

        #region Messages
        /// <summary>
        /// Сообщение для генерации аккаунтов
        /// </summary>
        public class GanerateAccount
        {
            public List<Account> LocalAccounts { get; private set; }
            public GanerateAccount(int accountNumber)
            {
                LocalAccounts = new List<Account>();
                for (int i = 0; i < accountNumber; i++)
                {
                    Account account = new Account(50000);
                    LocalAccounts.Add(account);
                }
            }
            
        }
        #endregion

        #region Handlers
        /// <summary>
        /// Обработка сообщения генерации аккаунтов
        /// </summary>
        /// <param name="ga"></param>
        private void GenerateAccoutsHandler(GanerateAccount ga)
        {
            Accounts = ga.LocalAccounts;
            Console.WriteLine("Generated {0} accounts", ga.LocalAccounts.Count);
        }
        #endregion
    }
}
