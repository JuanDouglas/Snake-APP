using System.Drawing;

namespace Snake.Logic.Graphic.EventArgs
{
    public class NewViewCreateEventArgs
    {
       public Image Result { get; set; }
        public GamePlataform Plataform { get; set; }
        public GameUI GameUI { get; set; }
        public NewViewCreateEventArgs(Image image, GamePlataform plataform, GameUI gameUI)
        {
            Result = image;
            Plataform = plataform;
            GameUI = gameUI;
        }
    }
}