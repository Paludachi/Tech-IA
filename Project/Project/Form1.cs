using System;
using System.Windows.Forms;

namespace Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnPlanner_Click(object sender, EventArgs e)
        {
            Form Form2 = new Form2();
            Form2.Show();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Show();
        }

        private void BtnComments_Click(object sender, EventArgs e)
        {
            Form Form3 = new Form3();
            Form3.Show();
        }

        private void BtnTracker_Click(object sender, EventArgs e)
        {
            Form Form4 = new Form4();
            Form4.Show();
        }
    }
}
