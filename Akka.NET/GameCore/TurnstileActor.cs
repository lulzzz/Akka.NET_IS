using System;
using Akka.Actor;

namespace GameCore
{
    #region States
    public enum TurnstileStatus
    {
        Locked,
        Unlocked
    }
    #endregion

    #region Events

    /// <summary>
    /// событие - пытаемся пройти
    /// </summary>
    public class PushBar { }

    /// <summary>
    /// событие - вставляем монету
    /// </summary>
    public class InsertCoin { }

    #endregion

    // монеты
    public class Coins
    {
        public int Total { get; }

        public Coins(int total)
        {
            Total = total;
        }

        public Coins AddOne()
        {
            return new Coins(Total + 1);
        }

        public static Coins NoCoins()
        {
            return new Coins(0);
        }
    }

    class TurnstileActor : FSM<TurnstileStatus, Coins>
    {
        public TurnstileActor()
        {
            InitializeFsm();
        }

        /// <summary>
        /// инициализация
        /// </summary>
        private void InitializeFsm()
        {
            StartWith(TurnstileStatus.Locked, Coins.NoCoins());
            When(TurnstileStatus.Locked, LockedLogic);
            When(TurnstileStatus.Unlocked, UnlockedLogic);
            Initialize();
        }

        /// <summary>
        /// логика при разблокированном состоянии
        /// </summary>
        /// <param name="fsmevent"></param>
        /// <returns></returns>
        private State<TurnstileStatus, Coins> UnlockedLogic(Event<Coins> fsmevent)
        {
            // слишком много монет, получаем сдачу, состояние не меняется
            if (fsmevent.FsmEvent is InsertCoin)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Coin already inserted. Take back and go.");
                Console.ResetColor();
                return GoTo(TurnstileStatus.Unlocked).Using(StateData.AddOne());
            }
            // проходим
            if (fsmevent.FsmEvent is PushBar)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"You can go. State is: {StateName}.");
                Console.ResetColor();
                return GoTo(TurnstileStatus.Locked);
            }
            return null;
        }

        /// <summary>
        /// логика при блокированном состоянии
        /// </summary>
        /// <param name="fsmevent"></param>
        /// <returns></returns>
        private State<TurnstileStatus, Coins> LockedLogic(Event<Coins> fsmevent)
        {
            // вставляем монету
            if (fsmevent.FsmEvent is InsertCoin)
            {
                return GoTo(TurnstileStatus.Unlocked).Using(StateData.AddOne());
            }
            // пытаемся пройти без монеты
            if (fsmevent.FsmEvent is PushBar)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"You can't go, insert coin first please. State is {StateName}.");
                Console.ResetColor();
                return Stay();
            }
            return null;
        }
    }
}

