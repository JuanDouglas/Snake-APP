using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;
using Snake_Logic.Enums;
using Snake_Logic.Base;
using Snake_Logic.Event_Args;
using System;

namespace Snake_Logic
{
    public class Plataform
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Velocity { get { return _Velocity; } set {
                _Velocity = value;
                MoveTimer = GetTimer();
            } }
        private int _Velocity;
        public Snake Snake { get; set; }
        public List<Apple> Apples { get; set; }
        public int ApplePower { get; set; }
        public Timer MoveTimer { get; private set; }
        public Nullable<int> CollectedApples { get; protected internal set; }
        public int MaxApples { get; set; }
        private Random rd;

        public delegate void MoveSnakeEventHandler(object sender, MoveSnakeArgs args);
        public event MoveSnakeEventHandler MoveSnakeEvent;

        public delegate void LoseGameHandler(object sender, LoseGameArgs args);
        public event LoseGameHandler LoseGame;

        public delegate void CollectAppleHandler(object sender, CollectAppleArgs args);
        public event CollectAppleHandler CollectApple;

        public Plataform(int width, int height, int velocity)
        {
            Width = width;
            Height = height;
            Velocity = velocity;
            Snake = new Snake(this,Direction.Right,new Point(width/2,height/2));
            rd = new Random();
            ApplePower = 1;
            CollectedApples = 1;
            MaxApples = 1;
            Apples = new List<Apple>();
            for (int i = 0; i < MaxApples; i++)
            {
                Apples.Add(new Apple(
                    new Point(
                        rd.Next(0, Width),
                        rd.Next(0, Height)),
                    ApplePower));
            }
            LoseGame += new LoseGameHandler((object sender, LoseGameArgs tArgs) => {
                _ = tArgs; });
            CollectApple += new CollectAppleHandler((object sender, CollectAppleArgs args) => {
                Apples.RemoveAll(remove=>remove.Location.Equals(args.Apple.Location));
                Snake.Plataform.CollectedApples++;
                Apples.Add(new Apple(
                    new Point(
                        rd.Next(0,Width),
                        rd.Next(0,Height)),
                    ApplePower));
            });
        }

        public PointCotent GetContentInPoint(Point point)
        {
            if (point.X > Width)
            {
                return PointCotent.Wall;
            }
            if (point.X < 0)
            {
                return PointCotent.Wall;
            }
            if (point.Y > Height)
            {
                return PointCotent.Wall;
            }
            if (point.Y < 0)
            {
                return PointCotent.Wall;
            }
            if (Snake.Head.Location.Equals(point))
            {
                return PointCotent.SnakeBody;
            }
            foreach (var item in Snake.Blocks)
            {
                if (item.Location.Equals(point))
                {
                    return PointCotent.SnakeBody;
                }
            }

            foreach (var item in Apples)
            {
                if (item.Location.Equals(point))
                {
                    return PointCotent.Apple;
                }
            }

            return PointCotent.Null;
        }
        public Apple GetApple(Point location) {
            foreach (var item in Apples)
            {
                if (item.Location.Equals(location))
                {
                    return item;
                }
            }
            return null;
        }
        private Timer GetTimer() {
            Timer tm = new Timer()
            {
                Enabled = false,
                Interval = _Velocity
            };
            tm.Elapsed += new ElapsedEventHandler((object sender,ElapsedEventArgs args)=> {
                try
                {
                    Snake.Move();
                }
                catch (Exceptions.SnakeBodyException e)
                {
                    LoseGame.Invoke(this, new LoseGameArgs(e, Snake.Legacy, CollectedApples));
                } catch (Exceptions.SnakeWallException e) 
                {
                    LoseGame.Invoke(this, new LoseGameArgs(e, Snake.Legacy, CollectedApples));
                }
                MoveSnakeEvent.Invoke(this, new MoveSnakeArgs());
            });
            return tm; 
        }
        public void PlayGame() {
            MoveTimer.Start();
        }
        public void PauseGame() {
            MoveTimer.Stop();
        }
        protected internal void CollectAppleInvoke(object sender,CollectAppleArgs args) {
            CollectApple.Invoke(sender,args);
        }
    }
}
