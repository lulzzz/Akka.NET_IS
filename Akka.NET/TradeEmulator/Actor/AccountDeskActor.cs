using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Actor;
using TradeEmulator.Types;
using static TradeEmulator.Actor.OpenPositionActor;

namespace TradeEmulator
{
    /// <summary>
    /// Актор генерирует и хранит аккаунты
    /// </summary>
    public class AccountDeskActor : ReceiveActor
    {
        #region Fields

        /// <summary>
        /// актор открытия позиций
        /// </summary>
        private IActorRef openPositionActor;

        /// <summary>
        /// актор закрытия позиций
        /// </summary>
        private IActorRef closePositionActor;

        /// <summary>
        /// список аккаунтов
        /// </summary>
        public List<Account> Accounts { get; private set; }

        #endregion

        #region Constructors

        public AccountDeskActor()
        {
            openPositionActor = Context.ActorOf(Props.Create(() => new OpenPositionActor()));
            //closePositionActor = Context.ActorOf(Props.Create(() => new ClosePositionActor()));
            Accounts = new List<Account>();
            // прием сообщений
            Receive<GenerateAccountMessage>(sm => GenerateAccoutsHandler(sm));
            Receive<OpenPositionMessage>(opm => OpenPositionHandler(opm));
        }

        #endregion

        #region Messages
        
        /// <summary>
        /// Сообщение для генерации аккаунтов
        /// </summary>
        public class GenerateAccountMessage
        {
            public List<Account> LocalAccounts { get; private set; }
            public GenerateAccountMessage(int accountNumber)
            {
                LocalAccounts = new List<Account>();
                for (int i = 0; i < accountNumber; i++)
                {
                    Account account = new Account(50000);
                    LocalAccounts.Add(account);
                }
            }
        }

        /// <summary>
        /// Сообщение для открытия позиций
        /// </summary>
        public class OpenPositionMessage
        {
            public List<Account> Accounts { get; private set; }
            public OpenPositionMessage(List<Account> accounts)
            {
                Accounts = accounts;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Обработка сообщения GenerateAccountMessage
        /// </summary>
        /// <param name="gam"></param>
        private void GenerateAccoutsHandler(GenerateAccountMessage gam)
        {
            Accounts = gam.LocalAccounts;
            Console.WriteLine("Сгенерировано {0} аккаунтов", gam.LocalAccounts.Count);
            Self.Tell(new OpenPositionMessage(Accounts));
        }

        /// <summary>
        /// обработка сообщения OpenPositionMessage
        /// </summary>
        /// <param name="opm"></param>
        private void OpenPositionHandler(OpenPositionMessage opm)
        {
            foreach (Account account in opm.Accounts)
                openPositionActor.Tell(new OpenPosition(account));
        }

        #endregion
    }
}
