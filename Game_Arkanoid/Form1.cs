using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Arkanoid
{
    public partial class Form1 : Form
    {
        int enemySpeed = 1;
        int enemyVolume = 6;
        bool enemyRigth = false;
        bool enemyLeft = false;
        bool goLeft = false;
        bool goRigth = false;
        int enemyKill = 0;
        int speedUnit = 25;

        public Form1()
        {
            InitializeComponent();
            spawnEnemy(enemyVolume);
            enemyRigth = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
                unit.Left -= speedUnit;
            if (e.KeyCode == Keys.Right)
                unit.Left += speedUnit;
            if (e.KeyCode == Keys.Space)
                shoot();
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            labelScore.Text = "Score: " + enemyKill;
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "shoot")
                {
                    x.Top -= 15;
                    foreach (Control y in this.Controls)
                    {
                        if (y is PictureBox && y.Tag == "enemy")
                        {
                            if (x.Bounds.IntersectsWith(y.Bounds))
                            {
                                enemyKill++;
                                x.Dispose();
                                y.Dispose();
                                if (enemyKill % 6 == 0)
                                {
                                    enemySpeed++;
                                    spawnEnemy(enemyVolume);
                                }
                            }
                        }
                    }
                }
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && x.Tag == "enemy")
                {
                    moveEnemy(x);
                }
            }
        }

        private void shoot()
        {
            PictureBox shoot = new PictureBox();
            shoot.Tag = "shoot";
            shoot.BackgroundImage = Properties.Resources.rocket;
            shoot.Left = unit.Left + 20;
            shoot.Top = unit.Top;
            shoot.Size = new Size(10, 10);
            this.Controls.Add(shoot);
            shoot.BringToFront();
        }

        private void moveEnemy(Control y)
        {
            if (enemyRigth)
            {
                y.Left += enemySpeed;
                if (y.Left > 700)
                {
                    enemyRigth = false;
                    enemyLeft = true;
                }

            }
            if (enemyLeft)
            {
                y.Left -= enemySpeed;
                if (y.Left < 100)
                {
                    enemyRigth = true;
                    enemyLeft = false;
                }

            }
        }
        private void spawnEnemy(int x)
        {
            int temp = 75;
            for (int i = 0; i < x; i++)
            {
                PictureBox enemy = new PictureBox();
                enemy.Tag = "enemy";
                enemy.BackgroundImage = Properties.Resources.enemy;
                enemy.Left = i + temp;
                enemy.Top = 50;
                enemy.Size = new Size(50, 50);
                this.Controls.Add(enemy);
                enemy.BringToFront();
                temp += 75;
            }
        }
    }
}
