using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Timers;
using Snake_Logic.Enums;
using Snake_Logic.Base;

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
        public delegate int MoveSnakeEventHandler(Plataform plataform);
        public event MoveSnakeEventHandler MoveSnakeEvent;
        public Timer MoveTimer { get; private set; }

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
                Enabled = true,
                Interval = _Velocity
            };
            tm.Elapsed += new ElapsedEventHandler((object sender,ElapsedEventArgs args)=> {
                Snake.Move();
                MoveSnakeEvent.Invoke(this);
            });
            return tm; 
        }

    }
}
