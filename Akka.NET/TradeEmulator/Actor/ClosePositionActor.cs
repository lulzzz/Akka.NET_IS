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
            public Position Account { get; private set; }
            public ClosePosition(Position acc)
            {
                Account = acc;
            }
        }

        #endregion

        #region Handlers

        private void ClosePositionHandler(ClosePosition cp)
        {
            
        }

        #endregion
    }
}
