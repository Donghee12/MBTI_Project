using Microsoft.VisualBasic;
using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace mbti
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetupUI();
            Size = new Size(1196, 727);
            AddMbtiNameLabels();
        }


        // 전역변수
        private List<Label>? menws;
        private List<Color>? menw_colors;
        private List<Label>? mbtiMenus;
        private List<Color>? mbtiGroupColors;
        private Dictionary<Panel, int> scrollTargets = new Dictionary<Panel, int>();
        private Dictionary<Panel, System.Windows.Forms.Timer> scrollTimers = new Dictionary<Panel, System.Windows.Forms.Timer>();
        private void SetupUI()
        {
            this.Text = "MBTI 성향 테스트";
            this.StartPosition = FormStartPosition.Manual;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            // 화면 중앙 기준에서 Y축 50픽셀 위로
            this.Location = new Point(
                (screenWidth - formWidth) / 2,
                (screenHeight - formHeight) / 2 - 30
            );

            lblTitle.Text = "MBTI 성향 테스트";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblInfo.Text = "MBTI는 성격유형을 4가지 지표로 분류합니다.\n\n" +
                          "◇ 외향(E) / 내향(I)\n" +
                          "◆ 감각(S) / 직관(N)\n" +
                          "◇ 사고(T) / 감정(F)\n" +
                          "◆ 판단(J) / 인식(P)\n\n" +
                          "총 16가지 유형으로 나뉘며,\n자신의 성향을 이해하는 데 도움이 됩니다.";
           
            lblInfo.TextAlign = ContentAlignment.MiddleLeft;
            lblInfo.Size = new Size(500, 200);


            btnStart.Text = "🚀 테스트 시작!";
            btnStart.Font = new Font("Segoe UI", 15, FontStyle.Bold);

            btnStart.FlatStyle = FlatStyle.Flat;
            btnStart.BackColor = Color.White;
            btnStart.ForeColor = Color.Black;

            // 테두리 및 효과 컬러
            Color accentColor = Color.FromArgb(52, 152, 219); // 스카이블루 느낌

            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.FlatAppearance.BorderColor = accentColor;
            btnStart.FlatAppearance.MouseOverBackColor = ControlPaint.Light(accentColor, 0.7f);
            btnStart.FlatAppearance.MouseDownBackColor = ControlPaint.Light(accentColor, 0.5f);
            btnStart.Paint += (s, e) =>
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int radius = 50; // 둥근 정도 조절

                Rectangle rect = new Rectangle(0, 0, btnStart.Width, btnStart.Height);
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
                path.CloseAllFigures();

                btnStart.Region = new Region(path);
            };

            picImage.Image = Image.FromFile("1.png");
            pictureBox1.Image = Image.FromFile("intj.png");
            pictureBox2.Image = Image.FromFile("intp.png");
            pictureBox3.Image = Image.FromFile("entj.png");
            pictureBox4.Image = Image.FromFile("entp.png");
            pictureBox5.Image = Image.FromFile("infj.png");
            pictureBox6.Image = Image.FromFile("infp.png");
            pictureBox7.Image = Image.FromFile("enfj.png");
            pictureBox8.Image = Image.FromFile("enfp.png");
            pictureBox9.Image = Image.FromFile("istj.png");
            pictureBox10.Image = Image.FromFile("isfj.png");
            pictureBox11.Image = Image.FromFile("estj.png");
            pictureBox12.Image = Image.FromFile("esfj.png");
            pictureBox13.Image = Image.FromFile("istp.png");
            pictureBox14.Image = Image.FromFile("isfp.png");
            pictureBox15.Image = Image.FromFile("estp.png");
            pictureBox16.Image = Image.FromFile("esfp.png");
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // 입력 애니메이션 전용 투명 Form 사용 (비동기 입력 시 효과 부여 어려움 대체)
            Label tempLabel = new Label();
            tempLabel.Text = "이름을 입력받는 중...";
            tempLabel.Font = new Font("서울남산 장체", 30, FontStyle.Bold);
            tempLabel.Size = new Size(400, 40);
            tempLabel.BackColor = Color.FromArgb(137, 186, 199);
            tempLabel.TextAlign = ContentAlignment.MiddleCenter;
            tempLabel.Location = new Point((this.ClientSize.Width - tempLabel.Width) / 2, 100);
            this.Controls.Add(tempLabel);
            tempLabel.BringToFront();

            Application.DoEvents();
            System.Threading.Thread.Sleep(400); // 짧은 대기

            Form customInput = new Form();
            customInput.FormBorderStyle = FormBorderStyle.FixedDialog;
            customInput.StartPosition = FormStartPosition.CenterParent;
            customInput.BackColor = Color.FromArgb(197, 225, 236);
            customInput.Size = new Size(350, 180);
            customInput.Text = "📌 MBTI 테스트 시작";

            Label lbl = new Label() { Left = 20, Top = 20, Text = "💬 이름을 입력해주세요", Font = new Font("Segoe UI", 11, FontStyle.Regular), AutoSize = true };
            TextBox input = new TextBox() { Left = 20, Top = 60, Width = 290, Font = new Font("Segoe UI", 11), BorderStyle = BorderStyle.None, BackColor = Color.FromArgb(197, 225, 236) };
            Button confirmation = new Button() { Text = "확인", Left = 230, Width = 80, Top = 100, DialogResult = DialogResult.OK, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            customInput.Controls.Add(lbl);
            customInput.Controls.Add(input);
            customInput.Controls.Add(confirmation);
            customInput.AcceptButton = confirmation;

            customInput.TopMost = true;
            DialogResult result = customInput.ShowDialog();

            string name = result == DialogResult.OK ? input.Text : string.Empty;

            this.Controls.Remove(tempLabel);

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("⚠ 이름을 입력하지 않으면 테스트를 시작할 수 없습니다.","입력 필요",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            this.Opacity = 1.0;

            System.Windows.Forms.Timer fadeOutTimer = new System.Windows.Forms.Timer();
            fadeOutTimer.Interval = 10;
            fadeOutTimer.Tick += (s, ev) =>
            {
                this.Opacity -= 0.05;
                if (this.Opacity <= 0)
                {
                    fadeOutTimer.Stop();
                    Form2 form2 = new Form2(name, this); // ← Form1에서 이렇게 넘겨줬어야 함
                    form2.Show();
                    this.Visible = false;
                    
                }
            };
            fadeOutTimer.Start();
            AddMbtiNameLabels();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitMbtiMenus(); // ✅ 반드시 제일 먼저 호출!!

            menws = new List<Label>();
            menws.Add(btn_Menu1);
            menws.Add(btn_Menu2);

            menw_colors = new List<Color>();
            menw_colors.Add(Color.FromArgb(255, 128, 114));
            menw_colors.Add(Color.FromArgb(254, 221, 0));

            //시작 TabPage 설정
            Tab_Main.SelectedIndex = 0;


            for (int i = 2; i <= 17; i++)
            {
                Control ctrl = this.Controls.Find("panel" + i, true).FirstOrDefault();
                if (ctrl is Panel panel)
                {
                    panel.AutoScroll = true;
                    panel.MouseWheel += Panel_MouseWheel;
                }
            }
            AddMbtiNameLabels();
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            Panel panel = sender as Panel;
            if (panel == null) return;

            int delta = e.Delta > 0 ? -60 : 60;
            int target = panel.VerticalScroll.Value + delta;
            target = Math.Max(0, Math.Min(panel.VerticalScroll.Maximum, target));
            scrollTargets[panel] = target;

            if (!scrollTimers.ContainsKey(panel))
            {
                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                timer.Interval = 15;
                timer.Tick += (s, ev) => SmoothScroll(panel);
                scrollTimers[panel] = timer;
            }

            scrollTimers[panel].Start();
        }

        private void SmoothScroll(Panel panel)
        {
            int current = panel.VerticalScroll.Value;
            int target = scrollTargets[panel];
            int step = (target - current) / 8;

            if (Math.Abs(step) < 1)
            {
                panel.AutoScrollPosition = new Point(0, target);
                scrollTimers[panel].Stop();
            }
            else
            {
                panel.AutoScrollPosition = new Point(0, current + step);
            }
        }

        public void setMenuChgane(int index)
        {
            if (Tab_Main.SelectedIndex != index)
            {
                menws[Tab_Main.SelectedIndex].ForeColor = Color.FromArgb(111, 111, 111);
                menws[index].ForeColor = menw_colors[index];
                Tab_Menu_Select_Bar.BackColor = menw_colors[index];
                Tab_Menu_Select_Bar.Location = new Point(menws[index].Location.X, 0);
                Tab_Main.SelectedIndex = index;
            }
        }

        public void setMbtiMenuChange(int index)
        {
            int groupIndex = index / 4;

            // 전부 회색으로 초기화
            for (int i = 0; i < mbtiMenus.Count; i++)
                mbtiMenus[i].ForeColor = Color.FromArgb(111, 111, 111);

            // 선택된 메뉴 강조
            mbtiMenus[index].ForeColor = mbtiGroupColors[groupIndex];
            MBTI_Menu_Select_Bar.BackColor = mbtiGroupColors[groupIndex];
            MBTI_Menu_Select_Bar.Location = new Point(mbtiMenus[index].Location.X, 0);

            // 메인 탭(tabPage3)으로 전환
            Tab_Main.SelectedIndex = 2;

            // MBTI 전용 탭 안에서 해당 index로 이동
            Tab_MBTI.SelectedIndex = index;
        }

        public void InitMbtiMenus()
        {
            mbtiMenus = new List<Label>(){ btn_MBTI1, btn_MBTI2, btn_MBTI3, btn_MBTI4,btn_MBTI5, btn_MBTI6, btn_MBTI7, btn_MBTI8,btn_MBTI9, btn_MBTI10, btn_MBTI11, btn_MBTI12,btn_MBTI13, btn_MBTI14, btn_MBTI15, btn_MBTI16};
            mbtiGroupColors = new List<Color>() {Color.FromArgb(153, 124, 225),Color.FromArgb(52, 168, 83),Color.FromArgb(66, 133, 244),Color.FromArgb(244, 180, 0)};
            for (int i = 0; i < mbtiMenus.Count; i++)
            {
                int capturedIndex = i;
                mbtiMenus[i].Click += (sender, e) => setMbtiMenuChange(capturedIndex);
            }
        }
        public void AddMbtiNameLabels()
        {
            for (int i = 2; i <= 18; i++)
            {
                // TabPage 이름은 예: tabPage2, tabPage3 ...
                var tab = Tab_MBTI.Controls.Find("tabPage" + i, true).FirstOrDefault() as TabPage;
                if (tab != null)
                {
                    string labelName = $"lblMbtiName_tab{i}";
                    var existingLabel = tab.Controls.Find(labelName, true).FirstOrDefault() as Label;

                    if (existingLabel != null)
                    {
                        // ✅ 이미 있으면 텍스트 초기화만 수행
                        existingLabel.Text = "";
                        continue;
                    }

                    // 🔍 이미 존재하면 생략
                    if (tab.Controls.ContainsKey(labelName))
                        continue;

                    Label nameLabel = new Label();
                    nameLabel.Name = $"lblMbtiName_tab{i}";
                    nameLabel.Text = "";
                    nameLabel.BackColor = Color.White;
                    nameLabel.Location = new Point(70, 280);
                    nameLabel.Size = new Size(450, 40); // 적당한 너비, 필요시 조정
                    nameLabel.Font = new Font("서울남산체", 23, FontStyle.Bold);
                    nameLabel.ForeColor = Color.Black; // 또는 다른 컬러
                    nameLabel.Visible = true;
                    tab.Controls.Add(nameLabel);
                    nameLabel.BringToFront();
                }
            }
        }


        private void btn_Menu1_Click(object sender, EventArgs e) { setMenuChgane(0); Size = new Size(1196, 727); }
        private void btn_Menu2_Click(object sender, EventArgs e) { setMenuChgane(1); Size = new Size(1196, 849); }
    }
}
