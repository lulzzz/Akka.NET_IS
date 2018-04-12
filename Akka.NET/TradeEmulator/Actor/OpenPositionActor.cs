using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeEmulator.Types;

namespace TradeEmulator.Actor
{
    public class OpenPositionActor : ReceiveActor
    {
        Account account;
        #region Constructors
        public OpenPositionActor(Account acc)
        {
            account = acc;
        }
        #endregion

        #region Messages
        private Account OpenPositionActor(Account acc)
        {
            Account local_account = acc;
            Instrument inst = Generator.RandomEnumValue<Instrument>();
            //Posi
        }
        #endregion

        #region Handlers
        #endregion
    }
}
