using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeSystem
{
    // представляет аккаунт некоторого пользователя
    public class AccountDesk : ReceiveActor
    {
        #region Fields

        /// <summary>
        /// ссылка на датагрид в котором будет отображение сделок
        /// </summary>
        private readonly DataGridView _dataGridView;
        
        /// <summary>
        /// ссылка на кнопку паузы для приостановки эмуляции
        /// </summary>
        private readonly Button _pauseButton;

        #endregion
        
        #region Props

      
        #endregion


        #region Constructors
        public AccountDesk(DataGridView dataGridView, Button pauseButton)
        {
            // защита от дурака
            //Value = Math.Abs(money);
            //Instrument = inst;
            //Lot = lot;
            //LotNumber = lotNumber;
            //CurrentCote = currentCote;
            AccuontCounter++;
            currentId = AccuontCounter;
            _dataGridView = dataGridView;
            _pauseButton = pauseButton;
        }
        #endregion
    }
}
