using Akka.Actor;
using System.ComponentModel;
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
        #region Constructors
        public AccountDesk(DataGridView dataGridView, Button pauseButton)
        {
            _dataGridView = dataGridView;
            _pauseButton = pauseButton;
            Positions = new BindingList<Position>();
        }
        #endregion
        #region Props
        public BindingList<Position> Positions { get; set; }
        #endregion

    }
}
