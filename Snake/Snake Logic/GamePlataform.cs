using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using Snake.Logic.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Snake.Logic
{
    /// <summary>
    /// Plataforma de jogo.
    /// </summary>
    public abstract class GamePlataform
    {
        #region Properties
        /// <summary>
        /// Largura é altura da plataforma.
        /// </summary>
        public Size Size { get; set; }
        /// <summary>
        /// Velocidade do jogo.
        /// </summary>
        /// 
        public virtual int Velocity
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
        public Timer UpdateViewTimer { get; set; }
        public virtual List<IPlataformObject> Objects { get; set; }
        /// <summary>
        /// Cobra do jogo.
        /// </summary>
        public virtual Snake Snake
        {
            get => (Snake)Objects.FirstOrDefault(fs => fs.Type == ObjectType.Snake);
            set
            {
                if (Objects.FirstOrDefault(fs => fs.Type == ObjectType.Snake) != null)
                {
                    Objects[GetPositionByID(value.ID)] = value;
                }
                else
                {
                    AddObject(value);
                }
            }
        }

        private int GetPositionByID(Guid id)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                if (Objects[0].ID == id)
                {
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        /// Maçãs da plataforma.
        /// </summary>
        public virtual List<Apple> Apples
        {
            get
            {

                List<IPlataformObject> plataformObjects = Objects.Where(wh => wh.Type == ObjectType.Apple).ToList();
                List<Apple> apples = new List<Apple>();

                foreach (var item in plataformObjects)
                {
                    apples.Add((Apple)item);
                }
                return apples;
            }
            set
            {
                if (value != null)
                {

                }
            }

        }
        /// <summary>
        /// "Poder" das Maçãs (quando irá adicionar no tamanho da cobbra).
        /// </summary>
        public virtual int ApplePower { get; set; }
        /// <summary>
        /// Thread responsável pelo movimento da cobra na Plataforma.
        /// </summary>
        public virtual Timer MoveTimer { get; private set; }
        /// <summary>
        /// Quantidade de Maçãs coletadas.
        /// </summary>
        public virtual Nullable<int> CollectedApples { get; protected internal set; }
        /// <summary>
        /// Maxímo de Maçãs na Plataforma.
        /// </summary>
        public virtual int MaxApples { get; set; }
        /// <summary>
        /// Objetos distintos na Plataforma.
        /// </summary>
        #endregion

        #region Constants

        public const int DefaultVelocity = 750;
        public const Direction DefaultDirection = Direction.Right;

        #endregion

        #region Events
        public virtual int AppleDeacreaseSpeed { get; set; }
        private Random rd;

        public delegate void MoveSnakeEventHandler(object sender, MoveSnakeArgs args);
        /// <summary>
        /// Evento do movimento da cobra pela plataforma (Acontece no tempo estipulando em "Velocity").
        /// </summary>
        public event MoveSnakeEventHandler MoveSnake;

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

        public delegate void UpdateViewHandler(object sender, UpdateViewArgs args);
        /// <summary>
        /// Evento que "força" uma atualização da View.
        /// </summary>
        public virtual event UpdateViewHandler UpdateView;
        #endregion

        #region Constructors
        /// <summary>
        /// Construtor da Plataforma.
        /// </summary>
        /// <param name="width">Largura da Plataforma.</param>
        /// <param name="height">Altura da Plataforma.</param>
        /// <param name="velocity">Velocidade do jogo.</param>
        public GamePlataform(int width, int height, int velocity) : this(width, height, velocity, 2, DefaultDirection, new Point())
        {
        }
        /// <summary>
        /// Construtor da Plataforma.
        /// </summary>
        /// <param name="width">Largura da Plataforma.</param>
        /// <param name="height">Altura da Plataforma.</param>
        /// <param name="velocity">Velocidade do jogo.</param>
        /// <param name="snake_direction">Direção inicial do cobra na Plataforma.</param>
        /// <param name="snake_point">Localização incial da cobra na Plataforma.</param>
        public GamePlataform(int width, int height, int velocity, int apples, Direction snake_direction, Point snake_point)
        {
            Size = new Size(width, height);
            Velocity = velocity;
            ApplePower = 2;
            CollectedApples = 1;
            MaxApples = 2;
            AppleDeacreaseSpeed = 200;
            UpdateViewTimer = GetRefreshTimer();
            Objects = new List<IPlataformObject>();
            UpdateView += new UpdateViewHandler((object sender, UpdateViewArgs args) =>
            {
                _ = sender;
            });
            ObjectInteraction += new ObjectInteractionHandler((object sender, ObjectInteractionArgs args) =>
            {

            });
            MoveSnake += new MoveSnakeEventHandler((object sender, MoveSnakeArgs args) =>
            {
                ObjectInteraction.Invoke(sender, new ObjectInteractionArgs(null, Snake));
            });
            CreatePlataform(snake_direction, snake_point);
            for (int i = 0; i < apples; i++)
            {
                AddObject(new Apple(Size, new Point(rd.Next(Size.Width), rd.Next(Size.Height)), ApplePower, AppleDeacreaseSpeed));
            }
        }
        #endregion

        /// <summary>
        /// Obtém o contéudo em um local da plataforma.
        /// </summary>
        /// <param name="point">Localização na Plataforma.</param>
        /// <returns>Retorna a enumeração do contéudo neste ponto.</returns>
        public virtual PointCotent GetContentInPoint(Point point)
        {
            if (point.X > Size.Width)
            {
                return PointCotent.Wall;
            }

            if (point.X < 0)
            {
                return PointCotent.Wall;
            }

            if (point.Y > Size.Height)
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
        public virtual Apple GetApple(Point location)
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
        public virtual void Play()
        {
            MoveTimer.Start();
            UpdateViewTimer.Start();
        }

        /// <summary>
        /// Pausa o Timer da Plataforma.
        /// </summary>
        public virtual void Pause()
        {
            MoveTimer.Stop();
        }
        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            MoveTimer.AutoReset = false;
            MoveTimer.Enabled = false;
            MoveTimer.Stop();
        }

        /// <summary>
        /// Re-instâcia todos os objetos.
        /// </summary>
        protected internal virtual void Restart(Direction snake_direction, Point snake_point)
        {
            MoveTimer.Stop();
            CreatePlataform(snake_direction, snake_point);
            MoveTimer.Start();
        }
        protected internal virtual void MoveSnakeInvoke(object sender, MoveSnakeArgs args)
        {
            MoveSnake.Invoke(sender, args);
        }
        protected internal virtual void LoseInvoke(object sender, LoseGameArgs args)
        {
            LoseGame.Invoke(sender, args);
        }
        protected internal virtual void ObjectInteractionInvoke(object sender, ObjectInteractionArgs args)
        {
            ObjectInteraction.Invoke(sender, args);
        }
        protected internal virtual void CollectAppleInvoke(object sender, CollectAppleArgs args)
        {
            CollectApple.Invoke(sender, args);
        }
        public virtual void UpdateViewInvoke(object sender, UpdateViewArgs args)
        {
            UpdateView.Invoke(sender, args);
        }
        protected internal virtual Timer GetTimer()
        {
            Timer tm = new Timer()
            {
                Enabled = false,
                Interval = _Velocity
            };
            tm.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs args) =>
            {
                Snake.Move();
            });
            return tm;
        }
        protected internal virtual Timer GetRefreshTimer()
        {
            Timer tm = new Timer()
            {
                Enabled = false,
                Interval = 500
            };
            tm.Elapsed += new ElapsedEventHandler((object sender, ElapsedEventArgs args) =>
            {
                UpdateView.Invoke(this, new UpdateViewArgs());
            });
            return tm;
        }
        public virtual void RemoveObject(IPlataformObject @object)
        {
            Objects.RemoveAll(fs => fs.ID == @object.ID);
        }
        public virtual void AddObject(IPlataformObject @object)
        {
            RemoveObject(@object);
            Objects.Add(@object);
        }
        protected internal virtual void CreatePlataform(Direction snake_direction, Point snake_point)
        {
            Snake = new Snake(this, snake_direction, snake_point);
            rd = new Random();
            for (int i = 0; i < MaxApples; i++)
            {
                AddObject(new Apple(Size,
                    new Point(
                        rd.Next(0, Size.Width),
                        rd.Next(0, Size.Height)),
                    ApplePower,
                    AppleDeacreaseSpeed));
            }
            LoseGame += new LoseGameHandler((object sender, LoseGameArgs args) =>
            {
                _ = args;
            });
            ObjectInteraction += new ObjectInteractionHandler((object sender, ObjectInteractionArgs args) =>
            {
                _ = args;
            });
            CollectApple += new CollectAppleHandler((object sender, CollectAppleArgs args) =>
            {
                Point previosPoint = new Point();
                int index = Snake.Blocks.Count;
                Direction previosDirection;
                SnakeBlock previos;
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
                    SnakeBlock block = new SnakeBlock(previos.Plataform, previosPoint, previos.Direction, newTurn, index);
                    Snake.Blocks.Add(block);
                }
                RemoveObject(apple);
                Snake.Plataform.CollectedApples++;
                AddObject(new Apple(Size,
                    new Point(
                        rd.Next(0, Size.Width),
                        rd.Next(0, Size.Height)),
                    ApplePower,
                    AppleDeacreaseSpeed));
            });
        }

    }
}
