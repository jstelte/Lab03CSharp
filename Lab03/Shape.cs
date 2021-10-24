using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIDrawer;
using System.Drawing;

namespace Lab03
{
    public abstract class Shape : IRender
    {
        internal PointF shapePos;
        protected Color shapeColour;
        protected Shape _thing;

        public Shape(PointF pos, Color colour, Shape thing)
        {
            shapePos = pos;
            shapeColour = colour;
            _thing = thing;
        }

        public virtual void Render(CDrawer dr)
        {
            if(_thing != null)
            {
                dr.AddLine((int)shapePos.X, (int)shapePos.Y, (int)_thing.shapePos.X, (int)_thing.shapePos.Y, Color.White);
                dr.Render();
            }
        }
    }

    public class FixedSquare : Shape
    {
        public FixedSquare(PointF pos, Color colour, Shape thing)
            :base(pos,colour, thing)
        {

        }

        public override void Render(CDrawer dr)
        {
            dr.AddCenteredRectangle((int)shapePos.X, (int)shapePos.Y, 20, 20, shapeColour);
            base.Render(dr);
        }
    }

    public abstract class AniShape : Shape, IAnimate
    {
        protected double sequenceValue;
        protected double sequenceDelta;

        public AniShape(PointF pos, Color colour, Shape thing, double value, double delta)
            :base(pos, colour, thing)
        {
            sequenceValue = value;
            sequenceDelta = delta;
        }

        virtual public void Tick()
        {
            sequenceValue += sequenceDelta;
        }

    } 
    
    public class AniPoly : AniShape
    {
         protected int _sideCount = 5;

        public AniPoly(PointF pos, Color colour, Shape thing, double value, double delta, int sideCount)
            :base(pos, colour, thing, value, delta)
        {
            if (sideCount < 3)
                throw new ArgumentException("Not enough sides");
            _sideCount = sideCount;
        }

        public override void Render(CDrawer dr)
        {
            dr.AddPolygon((int)shapePos.X, (int)shapePos.Y, 25, _sideCount, sequenceValue, shapeColour);
            base.Render(dr);
        }

    }

    public abstract class AniChild : AniShape
    {
        protected double _distance = 0;

        public AniChild(PointF pos, Color colour, Shape thing, double value, double delta)
            :base(pos,colour,thing,value,delta)
        {
            if (thing == null)
                throw new ArgumentException("Parent cannot be null");
            //Calculate distance between shape and its parent
            _distance = Math.Sqrt(Math.Pow(Math.Abs(pos.X - thing.shapePos.X),2) + Math.Pow(Math.Abs(pos.Y - thing.shapePos.Y),2));
        }
    }

    public class AniHighlight : AniChild
    {
        public AniHighlight(Color colour, Shape thing, double value, double delta)
            :base(thing.shapePos,colour,thing,value,delta)
        {

        }

        public override void Render(CDrawer dr)
        {
            dr.AddPolygon((int)shapePos.X - 26, (int)shapePos.Y - 25, 25, 8, sequenceValue, Color.FromArgb(0), 3, shapeColour);
            base.Render(dr);
        }
    }

    public abstract class AniBall : AniChild
    {
        public AniBall(Color colour, Shape thing, double value, double delta)
            :base(thing.shapePos, colour, thing, value, delta)
        {

        }

        public override void Render(CDrawer dr)
        {
            dr.AddCenteredEllipse((int)shapePos.X, (int)shapePos.Y, 20, 20, shapeColour);
            base.Render(dr);
        }
    }

    public class OrbitBall : AniBall
    {
        int _radius;
        public OrbitBall( Color colour, Shape thing, double value, double delta, int radius)
            :base(colour, thing, value, delta)
        {
            _radius = radius;
        }

        public override void Tick()
        {
            base.Tick();
            float X = _thing.shapePos.X + _radius * (float)(Math.Cos(sequenceValue));
            float Y = _thing.shapePos.Y + _radius * (float)(Math.Sin(sequenceValue));
            shapePos = new PointF(X,Y);
        }
    }

    public class VWobbleBall : AniBall
    {
        int _radius = 0;
        public VWobbleBall(Color colour, Shape thing, double value, double delta, int radius)
            :base(colour, thing, value, delta)
        {
            _radius = radius;
        }

        public override void Tick()
        {
            base.Tick();
            float Y = _thing.shapePos.Y + _radius * (float)(Math.Sin(sequenceValue));
            shapePos = new PointF(_thing.shapePos.X, Y);
        }
    }

    public class HWobbleBall : AniBall
    {
        int _radius = 0;
        public HWobbleBall(Color colour, Shape thing, double value, double delta, int radius)
            : base(colour, thing, value, delta)
        {
            _radius = radius;
        }

        public override void Tick()
        {
            base.Tick();
            float X = _thing.shapePos.X + _radius * (float)(Math.Cos(sequenceValue));
            shapePos = new PointF(X, _thing.shapePos.Y);
        }
    }
    public interface IRender
    {
        // render instance to the supplied drawer
        void Render(CDrawer dr);
    }
    public interface IAnimate
    {
        // cause per-tick state changes to instance (movement, animation, etc...)
        void Tick();
    }

}
