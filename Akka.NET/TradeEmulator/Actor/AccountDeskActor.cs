using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Actor;
using TradeEmulator.Types;

namespace TradeEmulator
{
    /// <summary>
    /// Актор генерирует и хранит аккаунты
    /// </summary>
    public class AccountDeskActor : ReceiveActor
    {
        #region Fields

        /// <summary>
        /// каждому аккаунту свой актор
        /// </summary>
        public Dictionary<IActorRef, Account> Accounts { get; private set; }

        #endregion

        #region Constructors

        public AccountDeskActor()
        {
            Accounts = new Dictionary<IActorRef, Account>();
            // прием сообщений
            Receive<GenerateAccountMessage>(sm => GenerateAccoutsHandler(sm));
            Receive<OpenPositionMessage>(opm => OpenPositionHandler());
            Receive<ReturnAccountMessage>(ram => ReturnAccountMessageHandler(ram));
        }

        #endregion

        #region Messages
        
        /// <summary>
        /// Сообщение для генерации аккаунтов
        /// </summary>
        public class GenerateAccountMessage
        {
            public int AccountsCount { get; private set; }
            public GenerateAccountMessage(int accountsCount)
            {
                AccountsCount = accountsCount;
            }
        }

        /// <summary>
        /// Сообщение для открытия позиций
        /// </summary>
        public class OpenPositionMessage
        { }

        /// Сообщение для закрытия позиций
        public class ClosePositionMessage
        { }

        /// <summary>
        /// возврат аккаунта с открытой позицией
        /// </summary>
        public class ReturnAccountMessage
        {
            public Account Account { get; private set; }
            public IActorRef OpenPositionActor { get; private set; }
            public ReturnAccountMessage(Account acc, IActorRef actor)
            {
                Account = acc;
                OpenPositionActor = actor;
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
            IActorRef OpenPositionActor;
            for (int i = 0; i < gam.AccountsCount; i++)
            {
                Account account = new Account(300000);
                OpenPositionActor = Context.ActorOf(Props.Create(() => new OpenPositionActor()));
                Accounts.Add(OpenPositionActor, account);
            }
            Console.WriteLine("Сгенерировано {0} аккаунтов", Accounts.Count);
        }

        /// <summary>
        /// обработка сообщения ReturnAccountMessage
        /// </summary>
        /// <param name="ram"></param>
        private void ReturnAccountMessageHandler(ReturnAccountMessage ram)
        {
            Accounts[ram.OpenPositionActor] = ram.Account;
        }

        /// <summary>
        /// обработка сообщения OpenPositionMessage
        /// </summary>
        /// <param name="opm"></param>
        private void OpenPositionHandler()
        {
            foreach (KeyValuePair<IActorRef, Account> item in Accounts)
                item.Key.Tell(new OpenPositionActor.OpenPositionMessage(item.Value));
        }

        #endregion
    }
}
