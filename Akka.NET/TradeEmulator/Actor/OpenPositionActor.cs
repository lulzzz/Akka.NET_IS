using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Types;

namespace TradeEmulator.Actor
{
    /// <summary>
    /// Актор открытия позиций
    /// </summary>
    public class OpenPositionActor : ReceiveActor
    {
        #region Fields
        
        #endregion
        #region Constructors

        public OpenPositionActor()
        {
            Receive<OpenPositionMessage>(op => OpenPositionHandler(op));
        }
        
        #endregion

        #region Messages

        /// <summary>
        /// Сообщение открыть позицию
        /// </summary>
        public class OpenPositionMessage
        {
            public Account Account { get; private set; }
            public OpenPositionMessage(Account acc)
            {
                Account = acc;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Обработка сообщения OpenPositionMessage
        /// </summary>
        /// <param name="opm"></param>
        private void OpenPositionHandler(OpenPositionMessage opm)
        {
            
            MSSQL mssql = new MSSQL();
            Position position;
            Instrument instrument = Generator.RandomEnumValue<Instrument>();
            float lotNumber = Generator.GetRandomLotSize(instrument);
            float quote = Generator.RandomQuoteValue(instrument, PositionState.Open);
            position = new Position(instrument, lotNumber, quote);
            opm.Account.Position = position;
            
            // проверяем можем ли открыть позицию
            opm.Account.CanOpenPosition();
            
            // если позиция открыта, добавляем в бд
            if (opm.Account.Position.PositionState == PositionState.Open)
                mssql.InsertPositionQuery(opm.Account);
            
            // возвращаем аккаунт в родительский актор OperationActor
            Sender.Tell(new OperationActor.ReturnActorMessage(opm.Account));
        }

        #endregion
    }
}
