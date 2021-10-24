using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using GDIDrawer;

namespace Lab03
{
    class Fungus
    {
        static Random _rnd = new Random();
        Dictionary<Point, int> _pixels = new Dictionary<Point, int>();
        public Point _pos;
        public CDrawer _canvas;
        public enum colors { red = 1, green, blue}
        colors mycolor;
        Thread processing;

        public Fungus(Point pos, CDrawer canvas, colors color)
        {
            _pos = pos;
            _canvas = canvas;
            mycolor = color;
            processing = new Thread(new ThreadStart(Shroom));
            processing.IsBackground = true;
            processing.Start();
        }

        public void Shroom()
        {
            Point newPoint = _pos;
            while (true)
            {
                List<Point> points = adjacent(newPoint);
                points = Shuffle(points);

                Dictionary<Point, int> tempDict = points.ToDictionary((o) => o, (o) => _pixels.ContainsKey(o) ? _pixels[o] : 0);
                tempDict = tempDict.OrderBy((o) => o.Value).ToDictionary((o) => o.Key, (o) => o.Value);
                newPoint = tempDict.First().Key;


                    if (_pixels.ContainsKey(newPoint))
                    {
                        if (_pixels[newPoint] < 239)
                            _pixels[newPoint] += 16;
                    }
                    else
                        _pixels.Add(newPoint, 32);

                lock (_canvas)
                {
                    if (mycolor == colors.red)
                        _canvas.SetBBPixel(newPoint.X, newPoint.Y, Color.FromArgb(_pixels[newPoint], 0, 0));
                    if (mycolor == colors.green)
                        _canvas.SetBBPixel(newPoint.X, newPoint.Y, Color.FromArgb(0, _pixels[newPoint], 0));
                    if (mycolor == colors.blue)
                        _canvas.SetBBPixel(newPoint.X, newPoint.Y, Color.FromArgb(0, 0, _pixels[newPoint]));
                }
                
                Thread.Sleep(0);
            }

        }

        public List<Point> adjacent(Point pos)
        {
            List<Point> temp = new List<Point>();
            Point tempPoint;
            for (int i = -1; i < 2; i++)
            {
                for (int l = -1; l < 2; l++)
                {
                    tempPoint = new Point(pos.X + i, pos.Y + l);
                    if(!(pos.Equals(tempPoint)))
                        temp.Add(tempPoint);
                }
            }
            temp.RemoveAll((p) => p.X < 0 || p.X > 999 || p.Y < 0 || p.Y > 999);
            return temp;
        }

        public List<Point> Shuffle(List<Point> sourcelist)
        {
            int j = 0;
            List<Point> stuff = new List<Point>(sourcelist);
            for (int i = 0; i < stuff.Count; i++)
            {
                lock (_rnd)
                {
                    j = _rnd.Next(i, stuff.Count);
                }
                Point temp = stuff[i];
                stuff[i] = stuff[j];
                stuff[j] = temp;
            }

            return stuff;
        }
    }
}
