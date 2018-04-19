using Akka.Actor;
using System;
using System.Collections.Generic;
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
        #region Constructors

        public OpenPositionActor()
        {
            Receive<OpenPosition>(op => OpenPositionHandler(op));
        }
        
        #endregion

        #region Messages

        /// <summary>
        /// Сообщение открыть позицию
        /// </summary>
        public class OpenPosition
        {
            public Account Account { get; private set; }
            public OpenPosition(Account acc)
            {
                Account = acc;
            }
        }

        #endregion

        #region Handlers

        /// <summary>
        /// Обработка сообщения OpenPosition
        /// </summary>
        /// <param name="op"></param>
        private void OpenPositionHandler(OpenPosition op)
        {
            Position position;
            Instrument instrument = Generator.RandomEnumValue<Instrument>();
            PositionState positionState = PositionState.Open;
            float lot = Generator.GetRandomLot();
            float lotNumber = Generator.GetRandomLotNumber();
            float closeCote = Generator.RandomOpenCoteValue(instrument);
            float openCote = Generator.RandomCloseCoteValue(instrument);
            position = new Position(op.Account, instrument, positionState, lot, lotNumber, openCote, closeCote);
            if(position.PositionValid)
            {
                MSSQL mssql = new MSSQL();
                mssql.InsertQuery(position);
            }
        }

        #endregion
    }
}
