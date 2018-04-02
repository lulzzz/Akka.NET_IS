namespace TradeSystem
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.AccountId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OrderType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LotSummary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PositionCote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastOperation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AccountId,
            this.AccountValue,
            this.LotAmount,
            this.LotCount,
            this.OrderType,
            this.LotSummary,
            this.PositionCote,
            this.LastOperation,
            this.OperationResult});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(826, 357);
            this.dataGridView1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Текущая котировка";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 363);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(802, 71);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Управление";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(117, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Мин. кол. лотов";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(120, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(721, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Запуск";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // AccountId
            // 
            this.AccountId.HeaderText = "Id Аккаунта";
            this.AccountId.Name = "AccountId";
            this.AccountId.ReadOnly = true;
            // 
            // AccountValue
            // 
            this.AccountValue.HeaderText = "Счет";
            this.AccountValue.Name = "AccountValue";
            this.AccountValue.ReadOnly = true;
            // 
            // LotAmount
            // 
            this.LotAmount.HeaderText = "Лот";
            this.LotAmount.Name = "LotAmount";
            this.LotAmount.ReadOnly = true;
            // 
            // LotCount
            // 
            this.LotCount.HeaderText = "Количество лотов";
            this.LotCount.Name = "LotCount";
            this.LotCount.ReadOnly = true;
            // 
            // OrderType
            // 
            this.OrderType.HeaderText = "Тип сделки";
            this.OrderType.Name = "OrderType";
            this.OrderType.ReadOnly = true;
            // 
            // LotSummary
            // 
            this.LotSummary.HeaderText = "Итого";
            this.LotSummary.Name = "LotSummary";
            this.LotSummary.ReadOnly = true;
            // 
            // PositionCote
            // 
            this.PositionCote.HeaderText = "Котировка на момент сделки";
            this.PositionCote.Name = "PositionCote";
            this.PositionCote.ReadOnly = true;
            // 
            // LastOperation
            // 
            this.LastOperation.HeaderText = "Текущая операция";
            this.LastOperation.Name = "LastOperation";
            this.LastOperation.ReadOnly = true;
            // 
            // OperationResult
            // 
            this.OperationResult.HeaderText = "Результат операции";
            this.OperationResult.Name = "OperationResult";
            this.OperationResult.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 443);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "TradeSystem";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountId;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderType;
        private System.Windows.Forms.DataGridViewTextBoxColumn LotSummary;
        private System.Windows.Forms.DataGridViewTextBoxColumn PositionCote;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastOperation;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationResult;
    }
}

