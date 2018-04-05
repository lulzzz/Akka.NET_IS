using Akka.Actor;
using System.ComponentModel;
using System.Windows.Forms;

namespace TradeSystem
{
    // основной актор, формировка списка аккаунтов и позиций
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
        private Button _pauseButton;
        #endregion
        
        #region Constructors
        public AccountDesk(DataGridView dataGridView, Button pauseButton)
        {
            _dataGridView = dataGridView;
            _pauseButton = pauseButton;
            Positions = new BindingList<Position>();

            //FillDataGrid();

        }
        #endregion
        
        #region Props
        public BindingList<Position> Positions { get; set; }
        #endregion
        
        #region Methods
        /// <summary>
        /// тестовый метод по заполнению датагрида из актора
        /// </summary>
        private void FillDataGrid()
        {
            
            Account acc = new Account(50.000m);
            Position pos = new Position(acc, Instrument.Currency, 0.1f, 2.0f, 5.12f, 6.25f);
            for (int i = 0; i < 10; i++)
                Positions.Add(pos);
        }
        #endregion

        public class InitializeDGV
        {
            public InitializeDGV(BindingList<Position> bl)
            {
                initialPositions = bl;
            }
            public BindingList<Position> initialPositions { get; set; }
        }
    }
}
