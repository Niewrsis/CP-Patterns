using System.Drawing;

namespace First
{
    interface ICloneable3D { ICloneable3D Clone(); }
    abstract class Shape3D : ICloneable3D
    {
        public Point3D Position { get; set; }
        public Color Color { get; set; }

        public abstract ICloneable3D Clone();

        public override string ToString()
        {
            return $"{GetType().Name}: Position = {Position}, Color = {Color}";
        }
    }

    public struct Point3D
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public Point3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }

    class Cube : Shape3D
    {
        public double Side { get; set; }

        public Cube(Point3D position, Color color, double side)
        {
            Position = position;
            Color = color;
            Side = side;
        }

        public override ICloneable3D Clone()
        {
            return new Cube(new Point3D(Position.X, Position.Y, Position.Z), Color, Side);
        }
    }

    class Sphere : Shape3D
    {
        public double Radius { get; set; }

        public Sphere(Point3D position, Color color, double radius)
        {
            Position = position;
            Color = color;
            Radius = radius;
        }

        public override ICloneable3D Clone()
        {
            return new Sphere(new Point3D(Position.X, Position.Y, Position.Z), Color, Radius);
        }
    }
    public class Program
    {
        public static void main(string[] args)
        {
            Sphere originalSphere = new Sphere(new Point3D(0, 0, 0), Color.Red, 5);
            Console.WriteLine($"Оригинал: {originalSphere}");

            Sphere clonedSphere1 = (Sphere)originalSphere.Clone();
            Sphere clonedSphere2 = (Sphere)originalSphere.Clone();

            clonedSphere1.Position = new Point3D(10, 10, 10);
            clonedSphere1.Color = Color.Blue;
            clonedSphere2.Position = new Point3D(20, 20, 20);
            clonedSphere2.Radius = 10;

            Console.WriteLine($"Клон 1: {clonedSphere1}");
            Console.WriteLine($"Клон 2: {clonedSphere2}");
            Console.WriteLine($"Оригинал (после клонирования): {originalSphere}");

            Cube originalCube = new Cube(new Point3D(1, 1, 1), Color.Green, 2);
            Console.WriteLine($"Оригинал куб: {originalCube}");
            Cube clonedCube = (Cube)originalCube.Clone();
            clonedCube.Position = new Point3D(100, 100, 100);
            Console.WriteLine($"Клон куба: {clonedCube}");
            Console.WriteLine($"Оригинал куба (после клонирования): {originalCube}");

        }
    }
}