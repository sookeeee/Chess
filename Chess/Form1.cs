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

            //δ����Ǯ�������а�ť
            foreach (var item in ButtonList)
            {
                foreach (var m in item.Value)
                {
                    m.Enabled = false;
                }
            }

            //���ý����غϰ�ť
            ConfirmButton.Enabled = false;
        }

        private void Click(object sender, EventArgs e)
        {
            //��ȡ��ǰbutton
            Button button = (Button)sender;

            //��ȡ��ǰ��
            var row = ButtonList.Where(item => item.Value.Any(x => x == button)).Select(item => item.Key).FirstOrDefault();

            //�жϵ�ǰ�����Ƿ��ѱ�ѡ��
            if (button.BackColor != SystemColors.Control)
            {

                button.BackColor = SystemColors.Control;

                PickButtonList.Remove(button);

                //�ж��Ƿ���ѡ�е�ǰ��������ť
                if (!PickButtonList.Any(item => item != button))
                {
                    //���������а�ť
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
                //���������а�ť
                foreach (var item in ButtonList.Where(item => item.Key != row))
                {
                    foreach (var m in item.Value)
                    {
                        m.Enabled = false;
                    }
                }

                //�жϵ�ǰ�����û�
                if (CurrentUser == "�췽")
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
        /// ��ʼ/���¿�ʼ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BeginButton_Click(object sender, EventArgs e)
        {
            var num = new Random().Next(1, 6);
            if (num <= 3)
            {
                CurrentUser = "�췽";
            }
            else
            {
                CurrentUser = "����";
            }
            UserLabel.Text = CurrentUser;

            //�������а�ť
            foreach (var item in ButtonList)
            {
                foreach (var m in item.Value)
                {
                    m.Enabled = true;
                    m.BackColor = SystemColors.Control;
                }
            }

            //���ý����غϰ�ť
            ConfirmButton.Enabled = true;
        }

        /// <summary>
        /// �����غ�,�ж�ʤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {

            if(!PickButtonList.Any())
            {
                MessageBox.Show("������ѡ��һ����ť�ٽ���");
                return;
            }

            var leftButtonCount = 0;
            //�ж��Ƿ��ʣһ����ť
            foreach (var key in ButtonList)
            {
                leftButtonCount += key.Value.Count(item => item.BackColor == SystemColors.Control);
            }

            if (leftButtonCount == 1)
            {
                MessageBox.Show($"{CurrentUser}ʤ��");
                ConfirmButton.Enabled = false;
            }

            if (leftButtonCount == 0)
            {
                MessageBox.Show($"{(CurrentUser == "�췽" ? "����" : "�췽")}ʤ��");
                ConfirmButton.Enabled = false;
            }


            PickButtonList = new List<Button>();
            CurrentUser = CurrentUser == "�췽" ? "����" : "�췽";
            UserLabel.Text = CurrentUser;

            //����������ť
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