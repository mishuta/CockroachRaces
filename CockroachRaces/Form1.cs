using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CockroachRaces
{
    public partial class Form1 : Form
    {
        GameController Game;

        public System.Windows.Forms.PictureBox getPictureBox(int n)
        {
            return (System.Windows.Forms.PictureBox)Controls["PictureBox" + n.ToString()];
        }

        public Form1()
        {
            InitializeComponent();
            Game = new GameController();
            writePlayerMoney();
        }
        public void writePlayerMoney()
        {
            radioButton1.Text = "Игрок " + Game.Gamblers[0].id + " (" + Game.Gamblers[0].money + "р.)";
            radioButton2.Text = "Игрок " + Game.Gamblers[1].id + " (" + Game.Gamblers[1].money + "р.)";
            radioButton3.Text = "Игрок " + Game.Gamblers[2].id + " (" + Game.Gamblers[2].money + "р.)";
        }
        public void writeCurrentBets()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Текущие ставки:");
            foreach (Gambler x in Game.Gamblers)
            {
                if (x.currentBet.betSize != 0)
                    listBox1.Items.Add("Игрок " + x.id + " на Участник " + x.currentBet.bugId + " - " + x.currentBet.betSize + "р.");
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }
        public void writeCurrentResult(int wonPlayerId)
        {
            listBox2.Items.Clear();
            listBox2.Items.Add("Результаты забега:");
            int countWinners = Game.Gamblers.FindAll(delegate(Gambler g){return g.currentBet.bugId==wonPlayerId;}).Count;
            int sizePrize = (countWinners==0)?0:Game.Gamblers.Select(x => x.currentBet.betSize).Sum()/countWinners;
            foreach (Gambler x in Game.Gamblers)
            { 
                if ( x.getPrize(wonPlayerId, sizePrize))
                {
                    if (sizePrize - x.currentBet.betSize == 0)
                        listBox2.Items.Add("Игрок " + x.id + " вышел в ноль");
                    else
                        listBox2.Items.Add("Игрок " + x.id + " выиграл - " + (sizePrize-x.currentBet.betSize) + "р.");
                }
                else
                {
                    listBox2.Items.Add("Игрок " + x.id + " проиграл - " + x.currentBet.betSize + "р.");
                }
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
            listBox1.SelectedIndex = -1;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        private void resetGame()
        {
            Game.resetBugs(21);
            animation();
            listBox2.Items.Clear();
        }
        public void animation(){
            foreach (Bug x in Game.Bugs)
                ((System.Windows.Forms.PictureBox)Controls["PictureBox" + x.id]).Left = x.Move();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Game.Gamblers.Select(g => g.currentBet.betSize).Sum() == 0)
            {
                label3.Text = "Сделайте ваши ставки";
                label3.Visible = true;
                return;
            }
            button2.Enabled = false;
            button1.Enabled = false;
            resetGame();
            Random rnd = new Random();
            int first = 0;
            int fstIndex = new int();
            while (first + 93 <= 535)
            {
                foreach (Bug x in Game.Bugs)
                    x.speed = rnd.Next(1, 10);
                animation();
                first = Game.Bugs.Select(w => w.X).Max();
                fstIndex = Game.Bugs.Find(delegate(Bug b) { return b.X == first; }).id;
                System.Threading.Thread.Sleep(50);
            }
            writeCurrentResult(fstIndex);
            label3.Text = "Победил участник №" + fstIndex + ". Переиграем?";
            label3.Visible = true;
            Game.resetBets();
            listBox1.Items.Clear();
            listBox1.Items.Add("Текущие ставки:");
            writePlayerMoney();
            button2.Enabled = true;
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Regex rgx = new Regex(@"^\d+$");
            int playerId = 0;
            foreach (Control control in groupBox1.Controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.RadioButton))
                {
                    RadioButton rbControl = (RadioButton)control;
                    if (rbControl.Checked)
                        playerId = rbControl.TabIndex + 1;
                }
            }
            int bugId = 0;
            foreach (Control control in groupBox2.Controls)
            {
                if (control.GetType() == typeof(System.Windows.Forms.RadioButton))
                {
                    RadioButton rbControl = (RadioButton)control;
                    if (rbControl.Checked)
                        bugId = rbControl.TabIndex + 1;
                }
            }
            int sizeBet = 0;
            if (rgx.IsMatch(textBox1.Text))
            {
                sizeBet = Int32.Parse(textBox1.Text);
            }
            if (playerId > 0 && bugId > 0 && sizeBet > 0)
            {
                switch (Game.Gamblers[playerId - 1].bet(bugId, sizeBet))
                {
                    case 0:
                        label3.Text = "У вас не хватает денег";
                        label3.Visible = true;
                        break;
                    case 1:
                        label3.Text = "Игрок №" + playerId.ToString() + " сделал ставку " + sizeBet.ToString()
                        + " р на участника №" + bugId.ToString();
                        label3.Visible = true;
                        break;
                    case 2:
                        label3.Text = "Cтавка уже сделана!";
                        label3.Visible = true;
                        break;
                }
            }
            else
            {
                label3.Text = "Проверьте правильность введенных данных";
                label3.Visible = true;
            }
            writeCurrentBets();
            writePlayerMoney();
        }
    }
}
