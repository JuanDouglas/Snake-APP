using Snake.Logic.Base;
using Snake.Logic.Base.Interfaces;
using Snake.Logic.Enums;
using Snake.Logic.Graphic.Base.Interfaces;
using Snake.Logic.Graphic.EventArgs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using Point = Snake.Logic.Base.Point;
using Size = Snake.Logic.Base.Size;

namespace Snake.Logic.Graphic.Base
{
    public class GraphicObject : PlataformObject, IGraphicObject
    {
        public bool isVisible { get; set; }
        public int UpdateVersion { get; set; }
        public GraphicObject(PlataformObject plataformObject) : this(plataformObject.PlataformSize, plataformObject.Location, plataformObject.Content, plataformObject.Type)
        {
            ID = plataformObject.ID;
        }
        public GraphicObject(IPlataformObject plataformObject) : this(plataformObject.PlataformSize, plataformObject.Location, plataformObject.Content, plataformObject.Type)
        {
            ID = plataformObject.ID;
        }
        public GraphicObject(Size plataformSize, Point location,  ObjectContent content,  ObjectType type) : base(plataformSize, location, content, type)
        {
            isVisible = true;
            Drawing += new DrawingHandler((object sender, DrawingEventArgs args) =>
            {

            });
            FinishDrawing += new FinishedDrawingHandler((object sender, FinishedDrawingArgs args) =>
            {
                return args.OutPut;
            });
        }

        public event DrawingHandler Drawing;
        public event FinishedDrawingHandler FinishDrawing;
        public DrawResult Draw(in Size uiSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Drawing.Invoke(this, new DrawingEventArgs());

            DrawResult drawResult = new DrawResult(ImgByType(Type, 
                new Size(uiSize.Width / PlataformSize.Width, uiSize.Height / PlataformSize.Height)),
                Location);

            stopwatch.Stop();

            FinishDrawing.Invoke(this, new FinishedDrawingArgs(drawResult.Image, stopwatch.Elapsed));
            drawResult.Elapsed = stopwatch.Elapsed;

            return drawResult;
        }

        public DrawResult Draw(in Size uiSize, in Size maxSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            DrawResult drawResult = Draw(uiSize);
            if (drawResult.Image.Width > maxSize.Width || drawResult.Image.Height > maxSize.Height)
            {
                drawResult.Image = new Bitmap(drawResult.Image, new System.Drawing.Size(maxSize.Width, maxSize.Height));
            }
            stopwatch.Stop();
            drawResult.Elapsed += stopwatch.Elapsed;
            return drawResult;
        }
        protected internal static Bitmap ImgByType(ObjectType type, in Size size)
        {
            if (ImagesByType==null)
            {
                CreateImages();
            }

            ImageByType imageByType = ImagesByType.FirstOrDefault(fs => fs.Type == type);
            if (imageByType==null)
            {
                type = PlataformObject.DefaultType;
                imageByType = ImagesByType.FirstOrDefault(fs => fs.Type == type);
            }

            lock (imageByType.Image)
            {
                ;
                using (MemoryStream ms = new MemoryStream())
                {
                    imageByType.Image.Save(ms, ImageFormat.Png);
                    Bitmap image = (Bitmap)Bitmap.FromStream(ms);
                    if (!size.Equals(new Size()))
                    {
                        if (image.Width != size.Width || image.Height != size.Height)
                        {
                            ImagesByType.FirstOrDefault(fs => fs.Type == type).Image = new Bitmap(image, new System.Drawing.Size(size.Width, size.Height));
                            image = ImagesByType.FirstOrDefault(fs => fs.Type == type).Image;
                        }
                    }
                    return image;
                }
            }
            

           
        }
        private static void CreateImages() {
            ImagesByType = new List<ImageByType>
            {
                new ImageByType(ObjectType.Apple, Properties.Resources.apple),
                new ImageByType(ObjectType.Tree, Properties.Resources.tree),
                new ImageByType(PlataformObject.DefaultType,Properties.Resources.default_image)
            };
        }
        private static List<ImageByType> ImagesByType { get; set; }
        private class ImageByType {
            public ObjectType Type { get; set; }
            public Bitmap Image { get; set; }
            public ImageByType(ObjectType type, Bitmap image)
            {
                Type = type;
                Image = image ?? throw new ArgumentNullException(nameof(image));
            }
        }
        public bool Equals(IGraphicObject other)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
           
        }
        ~GraphicObject() {
            Dispose();
        }
    }

}