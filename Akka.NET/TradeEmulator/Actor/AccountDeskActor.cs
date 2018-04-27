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

            Receive<OperationActorOpenMessage>(opm => OperationActorOpenMessageHandler());
            Receive<ReceiveAccountMessage>(ram => ReceiveAccountMessageHandler(ram));

            Receive<OperationActorCloseMessage>(oacm => OperationActorCloseMessageHandler(oacm));
            //Receive<ReceiveAccountCloseMessage>(ram => ReceiveAccountCloseMessageHandler(ram));
            
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
        public class OperationActorOpenMessage
        { }

        public class OperationActorCloseMessage
        {
            public KeyValuePair<IActorRef, Account> AccountItem { get; private set; }
            public OperationActorCloseMessage(KeyValuePair<IActorRef, Account> pair)
            {
                AccountItem = pair;
            }
        }

        public class ReceiveAccountMessage
        {
            public Account Account { get; private set; }
            public IActorRef Actor { get; private set; }
            public ReceiveAccountMessage(Account acc, IActorRef actor)
            {
                Account = acc;
                Actor = actor;
            }
        }

        //public class ReceiveAccountCloseMessage
        //{
        //    public Account Account { get; private set; }
        //    public IActorRef Actor { get; private set; }
        //    public ReceiveAccountCloseMessage(Account acc, IActorRef actor)
        //    {
        //        Account = acc;
        //        Actor = actor;
        //    }
        //}


        #endregion

        #region Handlers

        /// <summary>
        /// Обработка сообщения GenerateAccountMessage
        /// </summary>
        /// <param name="gam"></param>
        private void GenerateAccoutsHandler(GenerateAccountMessage gam)
        {
            IActorRef OperationActor;
            for (int i = 0; i < gam.AccountsCount; i++)
            {
                Account account = new Account(300000);
                OperationActor = Context.ActorOf(Props.Create(() => new OperationActor()));
                Accounts.Add(OperationActor, account);
            }
            Console.WriteLine("Сгенерировано {0} аккаунтов", Accounts.Count);
        }

        /// <summary>
        /// обработка сообщения ReturnAccountMessage
        /// </summary>
        /// <param name="ram"></param>
        private void ReceiveAccountMessageHandler(ReceiveAccountMessage ram)
        {
            Accounts[ram.Actor] = ram.Account;
            Self.Tell(new OperationActorCloseMessage(new KeyValuePair<IActorRef, Account>(ram.Actor, ram.Account)));
        }

        //private void ReceiveAccountCloseMessageHandler(ReceiveAccountCloseMessage ram)
        //{
        //    Accounts[ram.Actor] = ram.Account;
        //}

        /// <summary>
        /// обработка сообщения OpenPositionMessage
        /// </summary>
        /// <param name="opm"></param>
        private void OperationActorOpenMessageHandler()
        {
            foreach (KeyValuePair<IActorRef, Account> item in Accounts)
                item.Key.Tell(new OperationActor.OperationActorMessage(ActorOperationType.Open, item.Value));
        }

        public void OperationActorCloseMessageHandler(OperationActorCloseMessage oacm)
        {
            oacm.AccountItem.Key.Tell((new OperationActor.OperationActorMessage(ActorOperationType.Close, oacm.AccountItem.Value)));
        }

        #endregion
    }
}
