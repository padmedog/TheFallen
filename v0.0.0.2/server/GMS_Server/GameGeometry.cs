using System;

namespace GMS_Server
{
    public static class GameGeometry //didn't know what to call it, so I called it the only thing that I could think of for it
    {
        public static double point_direction(GamePoint2D a, GamePoint2D b)
        {
            return Math.Atan2(b.Y-a.Y,b.X-a.X);
        }
        public static double point_distance(GamePoint2D a, GamePoint2D b)
        {
            double xx = b.X - a.X;
            double yy = b.Y - a.Y;
            return Math.Sqrt((xx * xx) + (yy * yy));
        }
        public static double point_distance(GamePoint3D a, GamePoint3D b)
        {
            double xx = b.X - a.X;
            double yy = b.Y - a.Y;
            double zz = b.Z - a.Z;
            return Math.Sqrt((xx * xx) + (yy * yy) + (zz * zz));
        }
        public static bool rectangle_in_rectangle(GamePoint2D s1, GamePoint2D s2, GamePoint2D d1, GamePoint2D d2)
        {
            GamePoint2D a1 = s1.Min(s2);
            GamePoint2D a2 = s2.Max(s1);
            GamePoint2D b1 = d1.Min(d2);
            GamePoint2D b2 = d2.Max(d1);
            return a1.X < b2.X && a2.X > b1.X && a1.Y < b2.Y && a2.Y > b1.Y;
        }
        public static bool cube_in_cube(GamePoint3D s1, GamePoint3D s2, GamePoint3D d1, GamePoint3D d2)
        {
            GamePoint3D a1 = s1.Min(s2); //the switching isn't required, but it might smooth processing function in the long run
            GamePoint3D a2 = s2.Max(s1);
            GamePoint3D b1 = d1.Min(d2);
            GamePoint3D b2 = d2.Max(d1);
            return a1.X < b2.X && a2.Y > b1.Y && a1.Y < b2.Y && a2.Y > b1.Y &&
                a1.X < b2.X && a2.X > b1.X && a1.Z < b2.Z && a2.Z > b1.Z &&
                a1.Y < b2.Y && a2.Y > b1.Y && a1.Z < b2.Z && a2.Z > b1.Z;
        }
        public static bool point_in_cube(GamePoint3D p,GamePoint3D d1,GamePoint3D d2)
        {
            GamePoint3D a1 = d1.Min(d2);
            GamePoint3D a2 = d2.Max(d1);
            return p.X >= a1.X && p.X <= a2.X && p.Y >= a1.Y && p.Y <= a2.Y && p.Z >= a1.Z && p.Z <= a2.Z;
        }
        public static bool[] parse_binary(ulong value, byte size)
        {
            bool[] output_ = new bool[size];
            for( int i = 0; i < size; i += 1 )
            {
                output_[i] = ((value >> i) & 1) == 1;
            }
            return output_;
        }
        public static bool[] parse_binary(byte value)
        {
            return parse_binary(value, 8);
        }
        public static bool[] parse_binary(ushort value)
        {
            return parse_binary(value, 16);
        }
        public static bool[] parse_binary(uint value)
        {
            return parse_binary(value, 32);
        }
        public static bool[] parse_binary(ulong value)
        {
            return parse_binary(value, 64);
        }
        public static GamePoint2D lengthdir(float length, float direction)
        {
            double dir = direction * (Math.PI / 180);
            return new GamePoint2D(Math.Cos(dir) * length, Math.Sin(dir) * length);
        }
        public static GamePoint2D lengthdir(int length, float direction)
        {
            double dir = direction * (Math.PI / 180);
            return new GamePoint2D(Math.Cos(dir) * length, Math.Sin(dir) * length);
        }
        public static GamePoint3D lengthdir(float length, float direction, float pitch)
        {
            double dir = direction * (Math.PI / 180);
            double pit = pitch * (Math.PI / 180);
            double len = Math.Cos(pit) * length;
            return new GamePoint3D(Math.Cos(dir) * len, Math.Sin(dir) * len, Math.Sin(pit) * length);
        }
        public static GamePoint3D lengthdir(int length, float direction, float pitch)
        {
            double dir = direction * (Math.PI / 180);
            double pit = pitch * (Math.PI / 180);
            double len = Math.Cos(pit) * length;
            return new GamePoint3D(Math.Cos(dir) * len, Math.Sin(dir) * len, Math.Sin(pit) * length);
        }
        public static float lengthdir_x(float length, float direction)
        {
            double dir = direction * (Math.PI / 180);
            return (float)Math.Cos(dir) * length;
        }
        public static float lengthdir_y(float length, float direction)
        {
            double dir = direction * (Math.PI / 180);
            return (float)Math.Sin(dir) * length;
        }
        public static float lengthdir_x(int length, float direction)
        {
            double dir = direction * (Math.PI / 180);
            return (float)Math.Cos(dir) * length;
        }
        public static float lengthdir_y(int length, float direction)
        {
            double dir = direction * (Math.PI / 180);
            return (float)Math.Sin(dir) * length;
        }
        public static float lengthdir_x(float length, float direction, float pitch)
        {
            double dir = direction * (Math.PI / 180);
            double pit = pitch * (Math.PI / 180);
            return (float)Math.Cos(dir) * (float)Math.Cos(pit) * length;
        }
        public static float lengthdir_y(float length, float direction, float pitch)
        {
            double dir = direction * (Math.PI / 180);
            double pit = pitch * (Math.PI / 180);
            return (float)Math.Sin(dir) * (float)Math.Cos(pit) * length;
        }
        public static float lengthdir_z(float length, float pitch)
        {
            double pit = pitch * (Math.PI / 180);
            return (float)Math.Sin(pit) * length;
        }
        public static float lengthdir_x(int length, float direction, float pitch)
        {
            double dir = direction * (Math.PI / 180);
            double pit = pitch * (Math.PI / 180);
            return (float)Math.Cos(dir) * (float)Math.Cos(pit) * length;
        }
        public static float lengthdir_y(int length, float direction, float pitch)
        {
            double dir = direction * (Math.PI / 180);
            double pit = pitch * (Math.PI / 180);
            return (float)Math.Sin(dir) * (float)Math.Cos(pit) * length;
        }
        public static float lengthdir_z(int length, float pitch)
        {
            double pit = pitch * (Math.PI / 180);
            return (float)Math.Sin(pit) * length;
        }
    }

    public class GamePoint2D
    {
        public double X, Y;
        public GamePoint2D()
        {
            X = 0d;
            Y = 0d;
        }
        public GamePoint2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public GamePoint2D Add(GamePoint2D a)
        {
            GamePoint2D tmp = Add(this, a);
            X = tmp.X;
            Y = tmp.Y;
            return this;
        }
        public GamePoint2D Subtract(GamePoint2D a)
        {
            GamePoint2D tmp = Subtract(this, a);
            X = tmp.X;
            Y = tmp.Y;
            return this;
        }
        public GamePoint2D Multiply(GamePoint2D a)
        {
            GamePoint2D tmp = Multiply(this, a);
            X = tmp.X;
            Y = tmp.Y;
            return this;
        }
        public GamePoint2D Divide(GamePoint2D a)
        {
            GamePoint2D tmp = Divide(this, a);
            X = tmp.X;
            Y = tmp.Y;
            return this;
        }
        public GamePoint2D Min(GamePoint2D a)
        {
            GamePoint2D tmp = Min(this, a);
            X = tmp.X;
            Y = tmp.Y;
            return this;
        }
        public GamePoint2D Max(GamePoint2D a)
        {
            GamePoint2D tmp = Max(this, a);
            X = tmp.X;
            Y = tmp.Y;
            return this;
        }

        public static GamePoint2D Add(GamePoint2D a, GamePoint2D b)
        {
            return new GamePoint2D(a.X + b.X, a.Y + b.Y);
        }
        public static GamePoint2D Subtract(GamePoint2D a, GamePoint2D b)
        {
            return new GamePoint2D(a.X - b.X, a.Y - b.Y);
        }
        public static GamePoint2D Multiply(GamePoint2D a, GamePoint2D b)
        {
            return new GamePoint2D(a.X * b.X, a.Y * b.Y);
        }
        public static GamePoint2D Divide(GamePoint2D a, GamePoint2D b)
        {
            return new GamePoint2D(a.X / b.X, a.Y / b.Y);
        }
        public static GamePoint2D Min(GamePoint2D a, GamePoint2D b)
        {
            return new GamePoint2D((a.X > b.X) ? b.X : a.X, (a.Y > b.Y) ? b.Y : a.Y);
        }
        public static GamePoint2D Max(GamePoint2D a, GamePoint2D b)
        {
            return new GamePoint2D((a.X < b.X) ? b.X : a.X, (a.Y < b.Y) ? b.Y : a.Y);
        }
        public static GamePoint2D operator +(GamePoint2D left, GamePoint2D right)
        {
            return GamePoint2D.Add(left, right);
        }
        public static GamePoint2D operator -(GamePoint2D left, GamePoint2D right)
        {
            return GamePoint2D.Subtract(left, right);
        }
        public static GamePoint2D operator *(GamePoint2D left, GamePoint2D right)
        {
            return GamePoint2D.Multiply(left, right);
        }
        public static GamePoint2D operator /(GamePoint2D left, GamePoint2D right)
        {
            return GamePoint2D.Divide(left, right);
        }
    }
    public class GamePoint3D
    {
        public double X, Y, Z;
        public GamePoint3D()
        {
            X = 0d;
            Y = 0d;
            Z = 0d;
        }
        public GamePoint3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public GamePoint3D Add(GamePoint3D a)
        {
            GamePoint3D tmp = Add(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Subtract(GamePoint3D a)
        {
            GamePoint3D tmp = Subtract(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Multiply(GamePoint3D a)
        {
            GamePoint3D tmp = Multiply(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Divide(GamePoint3D a)
        {
            GamePoint3D tmp = Divide(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Min(GamePoint3D a)
        {
            GamePoint3D tmp = Min(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Max(GamePoint3D a)
        {
            GamePoint3D tmp = Max(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Add(GamePoint2D a)
        {
            GamePoint3D tmp = Add(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Subtract(GamePoint2D a)
        {
            GamePoint3D tmp = Subtract(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Multiply(GamePoint2D a)
        {
            GamePoint3D tmp = Multiply(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Divide(GamePoint2D a)
        {
            GamePoint3D tmp = Divide(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Min(GamePoint2D a)
        {
            GamePoint3D tmp = Min(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }
        public GamePoint3D Max(GamePoint2D a)
        {
            GamePoint3D tmp = Max(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
            return this;
        }

        public static GamePoint3D Add(GamePoint3D a, GamePoint3D b)
        {
            return new GamePoint3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static GamePoint3D Subtract(GamePoint3D a, GamePoint3D b)
        {
            return new GamePoint3D(a.X - b.X, a.Y - b.Y, a.Z + b.Z);
        }
        public static GamePoint3D Multiply(GamePoint3D a, GamePoint3D b)
        {
            return new GamePoint3D(a.X * b.X, a.Y * b.Y, a.Z + b.Z);
        }
        public static GamePoint3D Divide(GamePoint3D a, GamePoint3D b)
        {
            return new GamePoint3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
        }
        public static GamePoint3D Add(GamePoint3D a, GamePoint2D b)
        {
            return new GamePoint3D(a.X + b.X, a.Y + b.Y, a.Z);
        }
        public static GamePoint3D Subtract(GamePoint3D a, GamePoint2D b)
        {
            return new GamePoint3D(a.X - b.X, a.Y - b.Y, a.Z);
        }
        public static GamePoint3D Multiply(GamePoint3D a, GamePoint2D b)
        {
            return new GamePoint3D(a.X * b.X, a.Y * b.Y, a.Z);
        }
        public static GamePoint3D Divide(GamePoint3D a, GamePoint2D b)
        {
            return new GamePoint3D(a.X / b.X, a.Y / b.Y, a.Z);
        }
        public static GamePoint3D Min(GamePoint3D a, GamePoint3D b)
        {
            return new GamePoint3D((a.X > b.X) ? b.X : a.X, (a.Y > b.Y) ? b.Y : a.Y, (a.Z > b.Z) ? b.Z : a.Z);
        }
        public static GamePoint3D Max(GamePoint3D a, GamePoint3D b)
        {
            return new GamePoint3D((a.X < b.X) ? b.X : a.X, (a.Y < b.Y) ? b.Y : a.Y, (a.Z < b.Z) ? b.Z : a.Z);
        }
        public static GamePoint3D Min(GamePoint3D a, GamePoint2D b)
        {
            return new GamePoint3D((a.X > b.X) ? b.X : a.X, (a.Y > b.Y) ? b.Y : a.Y, a.Z);
        }
        public static GamePoint3D Max(GamePoint3D a, GamePoint2D b)
        {
            return new GamePoint3D((a.X < b.X) ? b.X : a.X, (a.Y < b.Y) ? b.Y : a.Y, a.Z);
        }
        public static GamePoint3D operator +(GamePoint3D left, GamePoint3D right)
        {
            return GamePoint3D.Add(left, right);
        }
        public static GamePoint3D operator -(GamePoint3D left, GamePoint3D right)
        {
            return GamePoint3D.Subtract(left, right);
        }
        public static GamePoint3D operator *(GamePoint3D left, GamePoint3D right)
        {
            return GamePoint3D.Multiply(left, right);
        }
        public static GamePoint3D operator /(GamePoint3D left, GamePoint3D right)
        {
            return GamePoint3D.Divide(left, right);
        }
        public static GamePoint3D operator +(GamePoint3D left, GamePoint2D right)
        {
            return GamePoint3D.Add(left, right);
        }
        public static GamePoint3D operator -(GamePoint3D left, GamePoint2D right)
        {
            return GamePoint3D.Subtract(left, right);
        }
        public static GamePoint3D operator *(GamePoint3D left, GamePoint2D right)
        {
            return GamePoint3D.Multiply(left, right);
        }
        public static GamePoint3D operator /(GamePoint3D left, GamePoint2D right)
        {
            return GamePoint3D.Divide(left, right);
        }
    }
}
