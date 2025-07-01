using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace mbti
{

    public partial class Form2 : Form
    {

        private Form1? mainForm;
        private string? userName;
        private Button[]? mbtiButtons;

        private int selectedValue = 0;
        private List<int> selectedScores = new List<int>();
        int currentQuestionIndex = 0;

        int e = 0, i = 0;
        int s = 0, n = 0;
        int t = 0, f = 0;
        int j = 0, p = 0;
        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string name, Form1 form1)
        {
            this.userName = name;
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += Form2_KeyDown; 
            this.mainForm = form1; // 기존 Form1 받아오기
            InitMbtiButtons();
            LoadNextQuestion();
        }


        private void InitMbtiButtons()
        {
            mbtiButtons = new Button[] { btn_MBTI1, btn_MBTI2, btn_MBTI3, btn_MBTI4, btn_MBTI5, btn_MBTI6 };

            foreach (var btn in mbtiButtons)
                btn.TabStop = false;

            int[] sizes = { 120, 100, 80, 80, 100, 120 };
            Color[] colors = {
                Color.FromArgb(190, 220, 240),
                Color.FromArgb(160, 210, 230),
                Color.FromArgb(130, 190, 220),
                Color.FromArgb(110, 170, 210),
                Color.FromArgb(90, 150, 200),
                Color.FromArgb(70, 130, 190)
            };

            string[] labels = { "전혀 아니다", "아니다", "약간 아니다", "약간 그렇다", "그렇다", "매우 그렇다" };

            int formCenterX = this.ClientSize.Width / 2;
            int spacing = 30;
            int baseY = 280;
            int[] btnX = new int[6];

            // ✅ 버튼들을 왼쪽으로 이동시키기 위해 기준점 조정
            int totalWidth = sizes.Sum() + spacing * (sizes.Length - 1);
            int startX = formCenterX - totalWidth / 2 - 10; // ← 여기서 왼쪽으로 40픽셀 더 이동

            btnX[0] = startX;
            for (int i = 1; i < sizes.Length; i++)
                btnX[i] = btnX[i - 1] + sizes[i - 1] + spacing;

            for (int i = 0; i < mbtiButtons.Length; i++)
            {
                var btn = mbtiButtons[i];
                int size = sizes[i];

                btn.Size = new Size(size, size);
                btn.Location = new Point(btnX[i], baseY + (130 - size) / 2);

                btn.BackColor = colors[i];
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Text = "";

                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, size, size);
                btn.Region = new Region(path);

                int selectedScore = i + 1;
                btn.Click += (s, e) =>
                {
                    selectedValue = selectedScore;
                    btnNext.Enabled = true;

                    for (int j = 0; j < mbtiButtons.Length; j++)
                        mbtiButtons[j].BackColor = colors[j];

                    btn.BackColor = Color.FromArgb(10, 40, 70);
                    this.ActiveControl = null;
                };

                Label lbl = new Label();
                lbl.Text = labels[i];
                lbl.TextAlign = ContentAlignment.MiddleCenter;
                lbl.AutoSize = false;
                lbl.Size = new Size(100, 20);
                lbl.Font = new Font("서울남산 장체", 15, FontStyle.Bold);
                lbl.Location = new Point(btn.Location.X + (btn.Width - lbl.Width) / 2, btn.Location.Y + size + 5);
                this.Controls.Add(lbl);
            }
        }
        private void SaveScore(int score, int index)
        {
            if (index >= mbtiTypes.Count) return;

            string type = mbtiTypes[index];
            int leftScore = score <= 3 ? 4 - score : 0;
            int rightScore = score > 3 ? score - 3 : 0;

            switch (type)
            {
                case "EI": e += rightScore; i += leftScore; break;
                case "SN": n += rightScore; s += leftScore; break;
                case "TF": t += rightScore; f += leftScore; break;
                case "JP": j += rightScore; p += leftScore; break;
            }
            //ShowScoreStatus();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (selectedValue == 0)
            {
                MessageBox.Show("먼저 선택지를 하나 골라주세요!", "선택 필요", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (currentQuestionIndex >= mbtiQuestions.Count)
                return;

            selectedScores.Add(selectedValue);
            SaveScore(selectedValue, currentQuestionIndex);
            currentQuestionIndex++;
            selectedValue = 0;
            ResetButtons();
            LoadNextQuestion();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex == 0) return;

            currentQuestionIndex--;
            int lastScore = selectedScores[currentQuestionIndex];
            UndoLastScore(currentQuestionIndex, lastScore);
            selectedScores.RemoveAt(currentQuestionIndex);
            selectedValue = lastScore;
            ResetButtons();
            LoadNextQuestion();

            btnNext.Enabled = true;
            mbtiButtons[lastScore - 1].BackColor = Color.FromArgb(10, 40, 70);
        }

        private void UndoLastScore(int index, int score)
        {
            if (index >= mbtiTypes.Count) return;

            string type = mbtiTypes[index];
            int leftScore = score <= 3 ? 4 - score : 0;
            int rightScore = score > 3 ? score - 3 : 0;

            switch (type)
            {
                case "EI": e -= rightScore; i -= leftScore; break;
                case "SN": n -= rightScore; s -= leftScore; break;
                case "TF": t -= rightScore; f -= leftScore; break;
                case "JP": j -= rightScore; p -= leftScore; break;
            }
        }

        private void ResetButtons()
        {
            Color[] defaultColors = {
                Color.FromArgb(190, 220, 240),
                Color.FromArgb(160, 210, 230),
                Color.FromArgb(130, 190, 220),
                Color.FromArgb(110, 170, 210),
                Color.FromArgb(90, 150, 200),
                Color.FromArgb(70, 130, 190)
            };

            for (int i = 0; i < mbtiButtons.Length; i++)
                mbtiButtons[i].BackColor = defaultColors[i];
        }

        private void ShowResult()
        {
            string result = "";
            result += e >= i ? "E" : "I";
            result += s >= n ? "S" : "N";
            result += t >= f ? "T" : "F";
            result += j >= p ? "J" : "P";

            MessageBox.Show($"당신의 MBTI는 {result}입니다!", "MBTI 결과", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Dictionary<string, int> mbtiToTabIndex = new Dictionary<string, int>
            {
                {"INTJ", 0}, {"INTP", 1}, {"ENTJ", 2}, {"ENTP", 3},
                {"INFJ", 4}, {"INFP", 5}, {"ENFJ", 6}, {"ENFP", 7},
                {"ISTJ", 8}, {"ISFJ", 9}, {"ESTJ", 10}, {"ESFJ", 11},
                {"ISTP", 12}, {"ISFP", 13}, {"ESTP", 14}, {"ESFP", 15}
            };

            // ShowResult() 내부
            if (mbtiToTabIndex.TryGetValue(result, out int mbtiIndex))
            {
                mainForm.Invoke(new Action(() =>
                {
                    int tabPageNumber = mbtiIndex + 3; // tabPage2 ~ tabPage18
                    string labelName = $"lblMbtiName_tab{tabPageNumber}";

                    // 해당 tabPage 가져오기
                    var tabPage = mainForm.Tab_MBTI.Controls.Find("tabPage" + tabPageNumber, true).FirstOrDefault() as TabPage;
                    if (tabPage != null)
                    {
                        var nameLabel = tabPage.Controls.Find(labelName, true).FirstOrDefault() as Label;
                        if (nameLabel != null)
                        {
                            nameLabel.Text = $"{userName}님은 \"{result}\" 유형입니다!";
                            nameLabel.BackColor = Color.White;
                            nameLabel.BringToFront();
                            nameLabel.TabIndex = 0;
                        }

                        // 🔽 출력 버튼이 이미 없으면 추가
                        if (tabPage.Controls.Find("btnCapture", true).FirstOrDefault() == null)
                        {
                            Button printBtn = new Button();
                            printBtn.Name = "btnCapture";
                            printBtn.Text = "출력";
                            printBtn.Size = new Size(100, 40);
                            printBtn.Location = new Point(1070, 30);
                            printBtn.BackColor = Color.FromArgb(255, 220, 200);
                            printBtn.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                            printBtn.Click += (s, ev) =>
                            {

                                var tabPage = mainForm.Tab_MBTI.Controls.Find("tabPage" + tabPageNumber, true).FirstOrDefault() as TabPage;
                                tabPage.Controls.SetChildIndex(nameLabel, 0); // 가장 앞에
                                if (tabPage != null)
                                {
                                    // 🔸 저장 전 크기 백업
                                    Size originalFormSize = mainForm.Size;
                                    Size originalTabMainSize = mainForm.Tab_Main.Size;
                                    Size originalTabMbtiSize = mainForm.Tab_MBTI.Size;
                                    Size originalTabPageSize = tabPage.Size;

                                    // 🔹 연결된 Panel 이름은 panelN = tabPageN+1 - 2 (즉 panel1 ~ panel16)
                                    int panelIndex = tabPageNumber - 2;
                                    var panel = mainForm.Controls.Find("panel" + panelIndex, true).FirstOrDefault() as Panel;
                                    Size? originalPanelSize = panel?.Size;

                                    // 🔹 MBTI_BackPanel도 있으면 사이즈 조절
                                    var backPanel = tabPage.Controls.Find("MBTI_BackPanel", true).FirstOrDefault() as Panel;
                                    Size? originalBackPanelSize = backPanel?.Size;

                                    // 🔸 임시로 크게 확장
                                    mainForm.Size = new Size(1196, 1200);
                                    mainForm.Tab_Main.Size = new Size(1172, 1250);
                                    mainForm.Tab_MBTI.Size = new Size(1172, 1250);
                                    tabPage.Size = new Size(1172, 1250);
                                    if (backPanel != null) backPanel.Size = new Size(1172, 1250);
                                    if (panel != null) panel.Size = new Size(1172, 820);

                                    // 캡처 직전
                                    tabPage.PerformLayout();     // 레이아웃 강제 적용
                                    tabPage.Refresh();           // 강제 다시 그리기

                                    Application.DoEvents(); // UI 적용


                                    // 🔹 캡처 진행
                                    string filePath = $"mbti_tab_{result.ToLower()}.png";
                                    SaveTabPageWithLabels(tabPage, filePath);
                                    MessageBox.Show($"{userName}님의 MBTI 저장 완료!", "완료", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // 🔸 다시 원래대로 복구
                                    mainForm.Size = originalFormSize;
                                    mainForm.Tab_Main.Size = originalTabMainSize;
                                    mainForm.Tab_MBTI.Size = originalTabMbtiSize;
                                    tabPage.Size = originalTabPageSize;
                                    if (backPanel != null && originalBackPanelSize.HasValue)
                                        backPanel.Size = originalBackPanelSize.Value;
                                    if (panel != null && originalPanelSize.HasValue)
                                        panel.Size = originalPanelSize.Value;

                                    Application.DoEvents(); // 복구 적용
                                }
                            };
                            tabPage.Controls.Add(printBtn);
                            printBtn.BringToFront();
                        }
                    }
                    mainForm.Opacity = 1.0;
                    mainForm.Visible = true;
                    mainForm.Size = new Size(1196, 849);
                    mainForm.setMenuChgane(1);
                    mainForm.Tab_Main.SelectedIndex = 1;
                    mainForm.setMbtiMenuChange(mbtiIndex);
                }));
            }


            this.Close(); // 현재 폼 종료
        }

        private void SaveTabPageWithLabels(TabPage tabPage, string filePath)
        {
            using (Bitmap bmp = new Bitmap(tabPage.Width, tabPage.Height))
            {
                // 기본 컨트롤 렌더링
                tabPage.DrawToBitmap(bmp, new Rectangle(0, 0, bmp.Width, bmp.Height));

                // 라벨 텍스트 수동 렌더링
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    foreach (Control ctrl in tabPage.Controls)
                    {
                        if (ctrl is Label lbl && lbl.Visible)
                        {
                            Rectangle bounds = lbl.Bounds;

                            using (SolidBrush bg = new SolidBrush(lbl.BackColor))
                                g.FillRectangle(bg, bounds);

                            TextRenderer.DrawText(
                                g,
                                lbl.Text,
                                lbl.Font,
                                bounds,
                                lbl.ForeColor,
                                TextFormatFlags.Left | TextFormatFlags.VerticalCenter
                            );
                        }
                    }
                }

                bmp.Save(filePath);
            }
        }

        private void LoadNextQuestion()
        {
            if (currentQuestionIndex < mbtiQuestions.Count)
                lbl_MBTI_Q.Text = $"Q{currentQuestionIndex + 1}. {mbtiQuestions[currentQuestionIndex]}";
            else
                ShowResult();
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && selectedValue > 0)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnNext.PerformClick();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lbl_MBTI_Q.Focus();
            StyleNavigationButtons();
        }
        private void StyleNavigationButtons()
        {
            Button[] navButtons = { btnPrev, btnNext };

            foreach (var btn in navButtons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(52, 152, 219);  // 파란색
                btn.ForeColor = Color.White;
                btn.Font = new Font("서울남산체", 35, FontStyle.Bold);

                btn.FlatAppearance.MouseOverBackColor = ControlPaint.Light(Color.FromArgb(52, 152, 219), 0.3f);
                btn.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(Color.FromArgb(52, 152, 219), 0.2f);

                // 둥근 모서리
                btn.Paint += (s, e) =>
                {
                    Button b = s as Button;
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    Rectangle rect = new Rectangle(0, 0, b.Width, b.Height);
                    GraphicsPath path = new GraphicsPath();
                    path.AddArc(rect.X, rect.Y, 20, 20, 180, 90);
                    path.AddArc(rect.Right - 20, rect.Y, 20, 20, 270, 90);
                    path.AddArc(rect.Right - 20, rect.Bottom - 20, 20, 20, 0, 90);
                    path.AddArc(rect.X, rect.Bottom - 20, 20, 20, 90, 90);
                    path.CloseAllFigures();

                    b.Region = new Region(path);
                };
            }
        }


        List<string> mbtiQuestions = new List<string>
        {
            // ✅ E vs I
            "모임에 나가면 에너지가 생긴다", //EI
            "사람 많은 곳이 즐겁다",  //EI
            "낯선 사람과 대화가 어렵지 않다", //EI
            "대화보다 혼자 생각하는 게 편하다", //IE
            "즉흥적인 만남을 좋아한다", //EI
            "조용한 장소를 선호한다", //IE
            "말하면서 생각을 정리하는 편이다",// IE
            "혼자 있는 시간이 필요하다",// IE
            "관심 받는 걸 즐기는 편이다", //EI
            "깊은 관계 소수가 좋다", //IE

            // 🔴 S vs N
            "사실 위주로 말하는 걸 선호한다",           // SN → 감각형(S): 구체적 사실 중시
            "상상하는 것을 즐긴다",                   // NS → 직관형(N): 상상, 추상적 사고
            "계획을 구체적으로 세운다",               // SN → 감각형(S): 실용적, 계획적 접근
            "큰 그림을 먼저 그린다",                 // NS → 직관형(N): 전체 구조, 미래상
            "실제 경험을 중시한다",                  // SN → 감각형(S): 직접 경험, 현실 기반
            "가능성에 대해 자주 생각한다",            // NS → 직관형(N): 가능성 탐색
            "현재에 집중하는 편이다",                // SN → 감각형(S): 지금 이 순간에 집중
            "미래를 자주 상상한다",                  // NS → 직관형(N): 장래의 변화 예측
            "현실적인 조언을 선호한다",              // SN → 감각형(S): 실용적, 실행 가능한 조언
            "새로운 아이디어가 떠오르곤 한다",         // NS → 직관형(N): 창의성, 추상적 아이디어


            // 🟡 T vs F
            "논리적으로 말하는 걸 선호한다",             // TF → 사고형(T): 논리, 분석 중시
            "감정을 중요하게 생각한다",                 // FT → 감정형(F): 감성, 공감 중시
            "옳고 그름이 더 중요하다",                  // TF → 사고형(T): 원칙, 사실 판단 중시
            "사람이 상처받지 않는 게 더 중요하다",        // FT → 감정형(F): 배려, 관계 중시
            "문제를 해결하는 게 우선이다",              // TF → 사고형(T): 효율, 결과 중심
            "먼저 공감하는 게 중요하다",                // FT → 감정형(F): 감정적 연결 우선
            "이성적으로 판단하려 한다",                // TF → 사고형(T): 감정보다 이성 우선
            "상황에 따라 유연하게 판단한다",            // FT → 감정형(F): 상황적 고려 중시
            "단호한 결정이 가능하다",                  // TF → 사고형(T): 결단력, 명확한 입장
            "관계 유지를 더 중요하게 생각한다",         // FT → 감정형(F): 조화와 인간관계 중시

            // 🟢 J vs P
            "계획대로 움직이는 걸 좋아한다",            // JP → 판단형(J): 계획적, 구조적
            "계획 없이 자유롭게 행동한다",             // PJ → 인식형(P): 즉흥적, 유연한 태도
            "정해진 순서대로 해야 마음이 편하다",        // JP → 판단형(J): 체계와 순서 중시
            "즉흥적으로 결정하는 게 좋다",             // PJ → 인식형(P): 자유로운 선택 선호
            "마감 전에 미리 끝내는 편이다",            // JP → 판단형(J): 시간 관리 철저
            "마감 직전에 집중력이 오른다",             // PJ → 인식형(P): 유연한 마감 선호
            "정리정돈이 잘 되어 있어야 한다",           // JP → 판단형(J): 질서, 구조 선호
            "어지러워도 괜찮다",                     // PJ → 인식형(P): 환경보다 유연성 중시
            "일정이 있어야 안심된다",                // JP → 판단형(J): 예측 가능한 흐름 선호
            "일정 없이 유연하게 움직이고 싶다",         // PJ → 인식형(P): 자유로운 계획 중시


            // ⚪ 기타
            "실수를 잘 기억한다",
            "아이디어를 자주 메모한다",
            "타인의 감정에 민감하다",
            "감정보다 사실을 먼저 본다",
            "말을 아끼는 편이다",
            "침묵이 어색하지 않다",
            "갑작스런 약속도 괜찮다",
            "일정이 바뀌면 스트레스 받는다",
            "상황보다 원칙을 중시한다",
            "주변 반응에 민감하게 반응한다"
        };
        List<string> mbtiTypes = new List<string>
        {
            // E vs I (10개)
            "EI", "EI", "EI", "IE", "EI", "IE", "IE", "IE", "EI", "IE",
            // S vs N (10개)
            "SN", "NS", "SN", "NS", "SN", "NS", "SN", "NS", "SN", "NS",
            // T vs F (10개)
            "TF", "FT", "TF", "FT", "TF", "FT", "TF", "FT", "TF", "FT",
            // J vs P (10개)
            "JP", "PJ", "JP", "PJ", "JP", "PJ", "JP", "PJ", "JP", "PJ",
            // 기타 (무시하거나 결과에 반영 X)
            "X", "X", "X", "X", "X", "X", "X", "X", "X", "X"
        };
    }
}
