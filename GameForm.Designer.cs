namespace Tetris
{
    partial class GameForm
    {
        private System.ComponentModel.IContainer components = null;

        public const int CellSizeDisplay = 30;

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
            GameGridOutPut = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)GameGridOutPut).BeginInit();
            SuspendLayout();
            // 
            // GameGridOutPut
            // 
            GameGridOutPut.Location = new Point(0, 0);
            GameGridOutPut.Name = "GameGridOutPut";
            GameGridOutPut.Size = new Size(100, 50);
            GameGridOutPut.TabIndex = 1;
            GameGridOutPut.TabStop = false;
            GameGridOutPut.Paint += GameGridPaintHandler;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(611, 496);
            Controls.Add(GameGridOutPut);
            Name = "GameForm";
            Text = "GameForm";
            KeyUp += OnKeyUp;
            ((System.ComponentModel.ISupportInitialize)GameGridOutPut).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private PictureBox GameGridOutPut;
    }
}