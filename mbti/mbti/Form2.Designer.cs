namespace mbti
{
    partial class Form2
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
            lbl_MBTI_Q = new Label();
            btn_MBTI1 = new Button();
            btn_MBTI2 = new Button();
            btn_MBTI3 = new Button();
            btn_MBTI4 = new Button();
            btn_MBTI5 = new Button();
            btnNext = new Button();
            lblDebugScore = new Label();
            btnPrev = new Button();
            btn_MBTI6 = new Button();
            SuspendLayout();
            // 
            // lbl_MBTI_Q
            // 
            lbl_MBTI_Q.AutoSize = true;
            lbl_MBTI_Q.Font = new Font("맑은 고딕", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lbl_MBTI_Q.Location = new Point(150, 66);
            lbl_MBTI_Q.Name = "lbl_MBTI_Q";
            lbl_MBTI_Q.Size = new Size(191, 40);
            lbl_MBTI_Q.TabIndex = 0;
            lbl_MBTI_Q.Text = "ㄹㄴㅁㄻㄴㄻ";
            // 
            // btn_MBTI1
            // 
            btn_MBTI1.Location = new Point(150, 342);
            btn_MBTI1.Name = "btn_MBTI1";
            btn_MBTI1.Size = new Size(75, 23);
            btn_MBTI1.TabIndex = 1;
            btn_MBTI1.Text = "button1";
            btn_MBTI1.UseVisualStyleBackColor = true;
            // 
            // btn_MBTI2
            // 
            btn_MBTI2.Location = new Point(350, 342);
            btn_MBTI2.Name = "btn_MBTI2";
            btn_MBTI2.Size = new Size(75, 23);
            btn_MBTI2.TabIndex = 2;
            btn_MBTI2.Text = "button2";
            btn_MBTI2.UseVisualStyleBackColor = true;
            // 
            // btn_MBTI3
            // 
            btn_MBTI3.Location = new Point(550, 342);
            btn_MBTI3.Name = "btn_MBTI3";
            btn_MBTI3.Size = new Size(75, 23);
            btn_MBTI3.TabIndex = 3;
            btn_MBTI3.Text = "button3";
            btn_MBTI3.UseVisualStyleBackColor = true;
            // 
            // btn_MBTI4
            // 
            btn_MBTI4.Location = new Point(750, 342);
            btn_MBTI4.Name = "btn_MBTI4";
            btn_MBTI4.Size = new Size(75, 23);
            btn_MBTI4.TabIndex = 4;
            btn_MBTI4.Text = "button4";
            btn_MBTI4.UseVisualStyleBackColor = true;
            // 
            // btn_MBTI5
            // 
            btn_MBTI5.Location = new Point(932, 342);
            btn_MBTI5.Name = "btn_MBTI5";
            btn_MBTI5.Size = new Size(75, 23);
            btn_MBTI5.TabIndex = 5;
            btn_MBTI5.Text = "button5";
            btn_MBTI5.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            btnNext.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnNext.Location = new Point(879, 544);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(277, 119);
            btnNext.TabIndex = 6;
            btnNext.Text = "다음";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // lblDebugScore
            // 
            lblDebugScore.AutoSize = true;
            lblDebugScore.Location = new Point(750, 66);
            lblDebugScore.Name = "lblDebugScore";
            lblDebugScore.Size = new Size(0, 15);
            lblDebugScore.TabIndex = 7;
            // 
            // btnPrev
            // 
            btnPrev.Font = new Font("맑은 고딕", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrev.Location = new Point(31, 544);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(277, 119);
            btnPrev.TabIndex = 8;
            btnPrev.Text = "이전";
            btnPrev.UseVisualStyleBackColor = true;
            btnPrev.Click += btnPrev_Click;
            // 
            // btn_MBTI6
            // 
            btn_MBTI6.Location = new Point(1069, 342);
            btn_MBTI6.Name = "btn_MBTI6";
            btn_MBTI6.Size = new Size(75, 23);
            btn_MBTI6.TabIndex = 9;
            btn_MBTI6.Text = "button5";
            btn_MBTI6.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(197, 225, 236);
            ClientSize = new Size(1180, 688);
            Controls.Add(btn_MBTI6);
            Controls.Add(btnPrev);
            Controls.Add(lblDebugScore);
            Controls.Add(btnNext);
            Controls.Add(btn_MBTI5);
            Controls.Add(btn_MBTI4);
            Controls.Add(btn_MBTI3);
            Controls.Add(btn_MBTI2);
            Controls.Add(btn_MBTI1);
            Controls.Add(lbl_MBTI_Q);
            KeyPreview = true;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MBTI 질문지";
            Load += Form2_Load;
            KeyDown += Form2_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_MBTI_Q;
        private Button btn_MBTI1;
        private Button btn_MBTI2;
        private Button btn_MBTI3;
        private Button btn_MBTI4;
        private Button btn_MBTI5;
        private Button btnNext;
        private Label lblDebugScore;
        private Button btnPrev;
        private Button btn_MBTI6;
    }
}