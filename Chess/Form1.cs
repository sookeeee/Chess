using System;
using System.Linq;

namespace Chess
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Dictionary<int, List<Button>> ButtonList = new Dictionary<int, List<Button>>();

        private List<Button> PickButtonList = new List<Button>();

        private string CurrentUser;

        private void Form1_Load(object sender, EventArgs e)
        {
            ButtonList.Add(1, new List<Button> { button1, button2, button3 });
            ButtonList.Add(2, new List<Button> { button4, button5, button6, button7, button8 });
            ButtonList.Add(3, new List<Button> { button9, button10, button11, button12, button13, button14, button15 });

            //未开局钱禁用所有按钮
            foreach (var item in ButtonList)
            {
                foreach (var m in item.Value)
                {
                    m.Enabled = false;
                }
            }

            //禁用结束回合按钮
            ConfirmButton.Enabled = false;
        }

        private void Click(object sender, EventArgs e)
        {
            //获取当前button
            Button button = (Button)sender;

            //获取当前行
            var row = ButtonList.Where(item => item.Value.Any(x => x == button)).Select(item => item.Key).FirstOrDefault();

            //判断当前棋子是否已被选中
            if (button.BackColor != SystemColors.Control)
            {

                button.BackColor = SystemColors.Control;

                PickButtonList.Remove(button);

                //判断是否已选中当前行其他按钮
                if (!PickButtonList.Any(item => item != button))
                {
                    //启用其他行按钮
                    foreach (var item in ButtonList.Where(item => item.Key != row))
                    {
                        foreach (var m in item.Value)
                        {
                            m.Enabled = true;
                        }
                    }
                }
            }
            else
            {
                //禁用其他行按钮
                foreach (var item in ButtonList.Where(item => item.Key != row))
                {
                    foreach (var m in item.Value)
                    {
                        m.Enabled = false;
                    }
                }

                //判断当前操作用户
                if (CurrentUser == "红方")
                {
                    button.BackColor = Color.Red;
                }
                else
                {
                    button.BackColor = Color.Blue;
                }

                PickButtonList.Add(button);
            }
        }

        /// <summary>
        /// 开始/重新开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginButton_Click(object sender, EventArgs e)
        {
            var num = new Random().Next(1, 6);
            if (num <= 3)
            {
                CurrentUser = "红方";
            }
            else
            {
                CurrentUser = "蓝方";
            }
            UserLabel.Text = CurrentUser;

            //启用所有按钮
            foreach (var item in ButtonList)
            {
                foreach (var m in item.Value)
                {
                    m.Enabled = true;
                    m.BackColor = SystemColors.Control;
                }
            }

            //启用结束回合按钮
            ConfirmButton.Enabled = true;
        }

        /// <summary>
        /// 结束回合,判断胜负
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {

            if(!PickButtonList.Any())
            {
                MessageBox.Show("请至少选择一个按钮再结束");
                return;
            }

            var leftButtonCount = 0;
            //判断是否仅剩一个按钮
            foreach (var key in ButtonList)
            {
                leftButtonCount += key.Value.Count(item => item.BackColor == SystemColors.Control);
            }

            if (leftButtonCount == 1)
            {
                MessageBox.Show($"{CurrentUser}胜利");
                ConfirmButton.Enabled = false;
            }

            if (leftButtonCount == 0)
            {
                MessageBox.Show($"{(CurrentUser == "红方" ? "蓝方" : "红方")}胜利");
                ConfirmButton.Enabled = false;
            }


            PickButtonList = new List<Button>();
            CurrentUser = CurrentUser == "红方" ? "蓝方" : "红方";
            UserLabel.Text = CurrentUser;

            //启用其他按钮
            foreach (var item in ButtonList)
            {
                foreach (var m in item.Value.Where(item => item.BackColor == SystemColors.Control))
                {
                    m.Enabled = true;
                }
            }
        }
    }
}