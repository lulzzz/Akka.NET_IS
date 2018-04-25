using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeEmulator
{
    /// <summary>
    /// Инструмент (валюта, золото, серебро, нефть)
    /// </summary>
    public enum Instrument
    {
        Currency, Gold, Silver, Oil
    }

    /// <summary>
    /// тип позиции продажа / покупка
    /// </summary>
    public enum PositionType
    {
        Sell, Buy
    }

    /// <summary>
    /// закрываем или открываем позицию
    /// </summary>
    public enum PositionState
    {
        Idle, Open, Close
    }

    public enum ActorOperationType
    {
        Open, Close
    }
}
