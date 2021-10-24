using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GDIDrawer;
using System.Threading;

namespace Lab03
{
    public partial class Form3 : Form
    {
        CDrawer _canvas = new CDrawer(1000, 1000, false);
        List<Shape> _shapes = new List<Shape>();
        Fungus spores;
        public Form3()
        {
            InitializeComponent();
            //Add fungus things
            spores = new Fungus(new Point(999, 0), _canvas, Fungus.colors.red);
            spores = new Fungus(new Point(0, 999), _canvas, Fungus.colors.blue);
            spores = new Fungus(new Point(0, 0), _canvas, Fungus.colors.green);
            // center solid anchor things
            _shapes.Add(new FixedSquare(new Point(450, 500), Color.Red, null));
            _shapes.Add(new FixedSquare(new Point(550, 500), Color.Red, _shapes[0]));
            AddOrbits();
            AddWobbleBalls();
            AddTopRow();
            AddBlockOrbit();
            //Adds the three triangle things
            _shapes.Add(new AniPoly(new PointF(100, 300), Color.Tomato, null, 3, 0.1, 3));
            _shapes.Add(new AniPoly(new PointF(135, 300), Color.Tomato, null, 3, -0.1, 3));
            _shapes.Add(new AniPoly(new PointF(170, 300), Color.Tomato, null, 3, 0.1, 3));
            AddHBalls();
            AddHighlight();



            //Renders and Ticks
            while (true)
            {
                _canvas.Clear();
                _shapes.ForEach((o) => {

                    if(o is AniShape)
                    {
                        ((AniShape)o).Tick();
                    }
                    o.Render(_canvas);
                });

                Thread.Sleep(20);
            }
            


        }

        //Adds the two things in the middle with orbiting ball things
        public void AddOrbits()
        {
            List<Shape> local = new List<Shape>();
            local.Add(new OrbitBall(Color.Yellow, _shapes[1], 1, 0.05,50));
            local.Add(new OrbitBall(Color.Pink,local[0],1, 0.075,50));
            local.Add(new OrbitBall(Color.Blue,local[1], 1,  0.1, 50));
            local.Add(new OrbitBall(Color.Green, local[2],1, 0.125,50));
            _shapes.AddRange(local);            local.Clear();            local.Add(new OrbitBall(Color.Yellow, _shapes[0], Math.PI, -0.1, 50));
            local.Add(new OrbitBall(Color.Pink, local[0], Math.PI, -0.15, 50));
            local.Add(new OrbitBall(Color.Blue, local[1], Math.PI, -0.2, 50));
            local.Add(new OrbitBall(Color.Green, local[2], Math.PI, -0.25, 50));
            _shapes.AddRange(local);
        }

        //Adds the thing in the bottom left corner with the wobble ball things
        public void AddWobbleBalls()
        {
            List<Shape> local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(200, 500), Color.Cyan, null));
            local.Add(new VWobbleBall(Color.Red,local[0],1, 0.1,100));
            local.Add(new HWobbleBall(Color.Red, local[1], 1, 0.15,100));
            local.Add(new OrbitBall(Color.LightBlue, local[2], 1, 0.2, 25));
            _shapes.AddRange(local);
        }

        //Adds the top row thing
        public void AddTopRow()
        {
            List<Shape> localA = new List<Shape>();
            List<Shape> localB = new List<Shape>();
            for (int i = 50; i < 1000; i += 50)
                localA.Add(new FixedSquare(new PointF(i, 100), Color.Cyan, null));
            _shapes.AddRange(localA);
            double so = 0;
            foreach (Shape s in localA)
                localB.Add(new VWobbleBall(Color.Purple, s, so += 0.7, 0.1, 50));
            _shapes.AddRange(localB);
        }

        //Adds that weird thing in the bottom right corner with orbiting ball things
        public void AddBlockOrbit()
        {
            List<Shape> local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(800, 500), Color.GreenYellow, null));
            local.Add(new OrbitBall(Color.Yellow, local[0],0, 0.1, 30));
            local.Add(new OrbitBall(Color.Yellow, local[0],Math.PI / 2, 0.1, 30));
            local.Add(new OrbitBall(Color.Yellow, local[0], Math.PI, 0.1, 30));
            local.Add(new OrbitBall(Color.Yellow, local[0], 3 * Math.PI / 2, 0.1, 30));
            local.Add(new OrbitBall(Color.Yellow, local[0], 0, -0.05, 60));
            local.Add(new OrbitBall(Color.Yellow, local[0], Math.PI / 2, -0.05, 60));
            local.Add(new OrbitBall(Color.Yellow, local[0], 3 * Math.PI, -0.05, 60));
            local.Add(new OrbitBall(Color.Yellow, local[0], 3 * Math.PI / 2, -0.05, 60));
            local.Add(new OrbitBall(Color.Yellow, local[0],0, 0.025, 90));
            local.Add(new OrbitBall(Color.Yellow, local[0], Math.PI / 2, 0.025, 90));
            local.Add(new OrbitBall(Color.Yellow, local[0], Math.PI, 0.025, 90));
            local.Add(new OrbitBall(Color.Yellow, local[0], 3 * Math.PI / 2, 0.025, 90));
            _shapes.AddRange(local);
        }

        //Adds the HWobbleBall things
        public void AddHBalls()
        {
            List<Shape> local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(500, 200), Color.Wheat, null));
            for (int i = 1; i < 20; ++i)
                local.Add(new HWobbleBall(Color.Orange,local[i - 1], 1, 0.1, 25));
            _shapes.AddRange(local);
        }

        //Adds the highlight thing
        public void AddHighlight()
        {
            List<Shape> local = new List<Shape>();
            local.Add(new FixedSquare(new PointF(800, 300), Color.LightCoral, null));
            local.Add(new AniHighlight(Color.Yellow, local[0], 30, -0.2));
            _shapes.AddRange(local);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
