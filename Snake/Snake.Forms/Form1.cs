using Snake.Logic;
using Snake.Logic.Base;
using Snake.Logic.Enums;
using Snake.Logic.EventArgs;
using Snake.Logic.Graphic;
using Snake.Logic.Graphic.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = Snake.Logic.Base.Point;

namespace Snake.Forms
{
    public partial class Form1 : Form
    {

        GraphicGamePlataform plataform => gameUI.GamePlataform;
        GameUI gameUI;
        int Width = 10;
        int Height = 10;
        int Velocity = 750;
        Random rd;
        Timer tm;
        public Form1()
        {
            InitializeComponent();
            rd = new Random();
            gameUI = new GameUI(null, 800, 600)
            {
                GamePlataform = new GraphicGamePlataform(Width, Height, Velocity)
            };
            gameUI.GamePlataform.LoseGame += new GraphicGamePlataform.LoseGameHandler(Lose);

            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                gameUI.GamePlataform.AddObject(new DefaultObject(gameUI.GamePlataform.Size, new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree));
            }
            tm = new Timer();
            tm.Interval = 1000;
            tm.Tick += new EventHandler(Refresh);
            tm.Start();
            gameUI.GamePlataform.Play();
        }
        private void Lose(object sender, LoseGameArgs LoseArg)
        {
            gameUI.GamePlataform = new GraphicGamePlataform(Width, Height, Velocity);

            gameUI.GamePlataform.LoseGame += new GraphicGamePlataform.LoseGameHandler(Lose);
            for (int i = 0; i < rd.Next(0, Width / 2); i++)
            {
                gameUI.GamePlataform.AddObject(new GraphicObject(new DefaultObject(plataform.Size, new Point(rd.Next(2, Width), rd.Next(2, Width)), ObjectContent.Solid, ObjectType.Tree)));
            }
            gameUI.GamePlataform.Play();
        }

        private void Refresh(object sender, EventArgs args)
        {
            resultPic.Image = gameUI.Draw();
        }
    }
}
