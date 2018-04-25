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
    /// Актор закрытия позиций
    /// </summary>
    public class ClosePositionActor : ReceiveActor
    {

        #region Constructors

        public ClosePositionActor()
        {
            Receive<ClosePosition>(cp => ClosePositionHandler(cp));
        }

        #endregion

        #region Messages

        public class ClosePosition
        {
            public Account Account { get; private set; }
            public ClosePosition(Account acc)
            {
                Account = acc;
            }
        }

        #endregion

        #region Handlers

        private void ClosePositionHandler(ClosePosition cp)
        {
            
            Position position = cp.Account.Position;
            float quote = Generator.RandomQuoteValue(position.Instrument, PositionState.Close);
            cp.Account.TryClosePosition(quote);
            // возвращаем аккаунт в OperationActor
            Sender.Tell(new OperationActor.ReturnClosePositionMessage(cp.Account));
        }

        #endregion
    }
}
