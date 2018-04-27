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
    /// Уведомления для RequestResolverActor
    /// TODO
    /// Значит уведомления об закрытии позиции будут здесь а не в ClosePositionActor
    /// </summary>
    public class NotifyActor : ReceiveActor
    {
        #region Messages
        
        /// <summary>
        ///  уведомляем что конкретный аккаунт открыл позицию
        /// </summary>
        public class PositionIsOpen
        {
            public Account Account { get; private set; }
            public PositionIsOpen(Account account)
            {

            }
        }

        /// <summary>
        /// конкретный аккаунт закрыл позицию
        /// </summary>
        public class PositionIsClosed
        {
            public Account Account { get; private set; }
            public PositionIsClosed(Account account)
            {

            }
        }
        #endregion

        #region Handlers

        public void PositionIsOpenHandler(PositionIsOpen pio)
        {

        }

        public void PositionIsClosedHandler(PositionIsClosed pic)
        {

        }
        #endregion
    }
}
