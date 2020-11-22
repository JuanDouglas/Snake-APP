using Snake_Logic.Base;
using Snake_Logic.Enums;
using Snake_Logic.Event_Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Snake_Logic
{
    /// <summary>
    /// Plataforma de jogo.
    /// </summary>
    public class Plataform
    {
        /// <summary>
        /// Largura da Plataforma.
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// Altura da Plataforma.
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// Velocidade do jogo.
        /// </summary>
        public int Velocity
        {
            get
            {
                return _Velocity;
            }
            set
            {
                _Velocity = value;
                MoveTimer = GetTimer();
            }
        }
        private int _Velocity;
        /// <summary>
        /// Cobra do jogo.
        /// </summary>
        public Snake Snake { get; set; }
        /// <summary>
        /// Maçãs da plataforma.
        /// </summary>
        public List<Apple> Apples { get; set; }
        /// <summary>
        /// "Poder" das Maçãs (quando irá adicionar no tamanho da cobbra).
        /// </summary>
        public int ApplePower { get; set; }
        /// <summary>
        /// Thread responsável pelo movimento da cobra na Plataforma.
        /// </summary>
        public Timer MoveTimer { get; private set; }
        /// <summary>
        /// Quantidade de Maçãs coletadas.
        /// </summary>
        public Nullable<int> CollectedApples { get; protected internal set; }
        /// <summary>
        /// Maxímo de Maçãs na Plataforma.
        /// </summary>
        public int MaxApples { get; set; }
        /// <summary>
        /// Objetos distintos na Plataforma.
        /// </summary>
        public List<PlataformObject> Objects { get; set; }
        private Random rd;

        public delegate void MoveSnakeEventHandler(object sender, MoveSnakeArgs args);
        /// <summary>
        /// Evento do movimento da cobra pela plataforma (Acontece no tempo estipulando em "Velocity").
        /// </summary>
        public event MoveSnakeEventHandler MoveSnakeEvent;

        public delegate void LoseGameHandler(object sender, LoseGameArgs args);
        /// <summary>
        /// Evento acionado quando o jogador perde o jogo (Acontece quando a cabeça da cobra se encontra no mesmo lugar que um objeto sólido). 
        /// </summary>
        public event LoseGameHandler LoseGame;

        public delegate void CollectAppleHandler(object sender, CollectAppleArgs args);
        /// <summary>
        /// Evento de coleta de Maçã (Acontece quando a cabeça da cobra se encontra no mesmo lugar de uma Maçã).
        /// </summary>
        public event CollectAppleHandler CollectApple;

        public delegate void ObjectInteractionHandler(object sender, ObjectInteractionArgs args);
        /// <summary>
        /// Evento de interação com o objecto (Acontece quando alguma parte do corpo da cobra passa por este objeto);
        /// </summary>
        public event ObjectInteractionHandler ObjectInteraction;

        /// <summary>
        /// Construtor da Plataforma.
        /// </summary>
        /// <param name="width">Largura da Plataforma.</param>
        /// <param name="height">Altura da Plataforma.</param>
        /// <param name="velocity">Velocidade do jogo.</param>
        public Plataform(int width, int height, int velocity)
        {
            Width = width;
            Height = height;
            Velocity = velocity;
            ApplePower = 2;
            CollectedApples = 1;
            MaxApples = 2;
            Objects = new List<PlataformObject>();
            CreatePlataform(Direction.Right, new Point(Width / 2, Height / 2));
        }
        /// <summary>
        /// Construtor da Plataforma.
        /// </summary>
        /// <param name="width">Largura da Plataforma.</param>
        /// <param name="height">Altura da Plataforma.</param>
        /// <param name="velocity">Velocidade do jogo.</param>
        /// <param name="snake_direction">Direção inicial do cobra na Plataforma.</param>
        /// <param name="snake_point">Localização incial da cobra na Plataforma.</param>
        public Plataform(int width, int height, int velocity, Direction snake_direction, Point snake_point)
        {
            Width = width;
            Height = height;
            Velocity = velocity;
            ApplePower = 2;
            CollectedApples = 1;
            MaxApples = 2;
            Objects = new List<PlataformObject>();
            CreatePlataform(snake_direction, snake_point);
        }

        /// <summary>
        /// Obtém o contéudo em um local da plataforma.
        /// </summary>
        /// <param name="point">Localização na Plataforma.</param>
        /// <returns>Retorna a enumeração do contéudo neste ponto.</returns>
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
                return PointCotent.SnakeHead;
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
        /// <summary>
        /// Obtém a Maçã no ponto escolhido.
        /// </summary>
        /// <param name="location">Localização na Plataforma.</param>
        /// <returns>Maçã</returns>
        public Apple GetApple(Point location)
        {
            foreach (var item in Apples)
            {
                if (item.Location.Equals(location))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// Incia o Timer da Plataforma.
        /// </summary>
        public void Play()
        {
            MoveTimer.Start();
        }

        /// <summary>
        /// Pausa o Timer da Plataforma.
        /// </summary>
        public void Pause()
        {
            MoveTimer.Stop();
        }

        /// <summary>
        /// Re-instâcia todos os objetos.
        /// </summary>
        public void Restart(Direction snake_direction, Point snake_point)
        {
            MoveTimer.Stop();
            CreatePlataform(snake_direction, snake_point);
            MoveTimer.Start();
        }
        protected internal void LoseInvoke(object sender, LoseGameArgs args)
        {
            LoseGame.Invoke(sender, args);
        }
        protected internal void ObjectInteractionInvoke(object sender, ObjectInteractionArgs args)
        {
            ObjectInteraction.Invoke(sender, args);
        }
        protected internal void CollectAppleInvoke(object sender, CollectAppleArgs args)
        {
            CollectApple.Invoke(sender, args);
        }
        private Timer GetTimer()
        {
            Timer tm = new Timer()
            {
                Enabled = false,
                Interval = _Velocity
            };
            tm.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs args) =>
            {
                try
                {
                    Snake.Move();
                }
                catch (Exceptions.SnakeBodyException e)
                {
                    LoseGame.Invoke(this, new LoseGameArgs(e, Snake.Legacy, CollectedApples));
                }
                catch (Exceptions.SnakeWallException e)
                {
                    LoseGame.Invoke(this, new LoseGameArgs(e, Snake.Legacy, CollectedApples));
                }
                MoveSnakeEvent.Invoke(this, new MoveSnakeArgs());
            });
            return tm;
        }
        private void CreatePlataform(Direction snake_direction, Point snake_point)
        {
            Apples = new List<Apple>();
            Snake = new Snake(this, snake_direction, snake_point);
            rd = new Random();
            for (int i = 0; i < MaxApples; i++)
            {
                Apples.Add(new Apple(
                    new Point(
                        rd.Next(0, Width),
                        rd.Next(0, Height)),
                    ApplePower));
            }
            LoseGame += new LoseGameHandler((object sender, LoseGameArgs args) =>
            {
                _ = args;
            });
            ObjectInteraction += new ObjectInteractionHandler((object sender,ObjectInteractionArgs args) =>
            {
                _ = args;
            });
            CollectApple += new CollectAppleHandler((object sender, CollectAppleArgs args) =>
            {
                Point previosPoint = new Point();
                int index = Snake.Blocks.Count;
                Direction previosDirection;
                Block previos;
                Apple apple = args.Apple;
                for (int i = 0; i < apple.Power; i++)
                {
                    index = Snake.Blocks.Count;
                    previos = Snake.Blocks.FirstOrDefault(fs => fs.Index.Value == (index - 1));
                    if (previos == null)
                    {
                        previos = Snake.Head;
                    }
                    else
                    {

                    }
                    previosDirection = previos.Direction;
                    switch (previosDirection)
                    {
                        case Direction.Down:
                            previosPoint = new Point(previos.Location.X - 1, previos.Location.Y);
                            break;
                        case Direction.UP:
                            previosPoint = new Point(previos.Location.X + 1, previos.Location.Y);
                            break;
                        case Direction.Left:
                            previosPoint = new Point(previos.Location.X, previos.Location.Y + 1);
                            break;
                        case Direction.Right:
                            previosPoint = new Point(previos.Location.X, previos.Location.Y - 1);
                            break;
                    }
                    var newTurn = new List<Turning>(previos.Turnings);
                    Block block = new Block(previos.Plataform, previosPoint, previos.Direction, newTurn, index);
                    Snake.Blocks.Add(block);
                }
                Apples.RemoveAll(remove => remove.Location.Equals(args.Apple.Location));
                Snake.Plataform.CollectedApples++;
                Apples.Add(new Apple(
                    new Point(
                        rd.Next(0, Width),
                        rd.Next(0, Height)),
                    args.Power));
            });
        }
    }
}
