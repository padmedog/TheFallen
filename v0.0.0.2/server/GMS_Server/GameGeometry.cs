using System;

namespace sharpserver_test0_0
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
        public static bool rectangle_in_rectangle(GamePoint2D a1, GamePoint2D a2, GamePoint2D b1, GamePoint2D b2)
        {
            return a1.X < b2.X && a2.Y > b1.X && a1.Y < b2.Y && a2.Y > b1.Y;
        }
        public static bool cube_in_cube(GamePoint3D a1, GamePoint3D a2, GamePoint3D b1, GamePoint3D b2)
        {
            /*if (rectangle_in_rectangle(new GamePoint2D(a1.X, a1.Y), new GamePoint2D(a2.X, a2.Y), new GamePoint2D(b1.X, b1.Y), new GamePoint2D(b2.X, b2.Y)))
                if (rectangle_in_rectangle(new GamePoint2D(a1.X, a1.Z), new GamePoint2D(a2.X, a2.Z), new GamePoint2D(b1.X, b1.Z), new GamePoint2D(b2.X, b2.Z)))
                    if (rectangle_in_rectangle(new GamePoint2D(a1.Y, a1.Z), new GamePoint2D(a2.Y, a2.Z), new GamePoint2D(b1.Y, b1.Z), new GamePoint2D(b2.Y, b2.Z)))
                        return true;
            return false;*/
            return a1.X < b2.X && a2.Y > b1.X && a1.Y < b2.Y && a2.Y > b1.Y && a1.X < b2.X && a2.Z > b1.X && a1.Z < b2.Z && a2.Z > b1.Z && a1.Y < b2.Y && a2.Z > b1.Y && a1.Z < b2.Z && a2.Z > b1.Z;
        }
        public static bool[] parse_binary(ulong value, byte size)
        {
            /*
            bool[] output_ = new bool[size];
            for( int i = size-1; i >= 0; i -= 1 )
            {
                ulong tmp_ = 2 ^ (ulong)i;
                if (value >= tmp_)
                {
                    output_[size - i - 1] = true;
                    value -= tmp_;
                }
                else output_[size - i - 1] = false;
            }
            return output_;*/

            bool[] output_ = new bool[size];
            for( int i = 0; i < size; i += 1 )
            {
                byte tmp_ = (byte)(value % 2);
                output_[i] = (tmp_ == 1) ? true : false;
                value = (value - tmp_) >> 1;
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
            return new GamePoint2D(Math.Cos(direction) * length, Math.Cos(direction) * length);
        }
        public static GamePoint2D lengthdir(int length, float direction)
        {
            return new GamePoint2D(Math.Cos(direction) * length, Math.Cos(direction) * length);
        }
        public static GamePoint3D lengthdir(float length, float direction, float pitch)
        {
            double len = Math.Cos(pitch) * length;
            return new GamePoint3D(Math.Cos(direction) * len, Math.Cos(direction) * len, Math.Sin(pitch) * length);
        }
        public static GamePoint3D lengthdir(int length, float direction, float pitch)
        {
            double len = Math.Cos(pitch) * length;
            return new GamePoint3D(Math.Cos(direction) * len, Math.Cos(direction) * len, Math.Sin(pitch) * length);
        }
        public static float lengthdir_x(float length, float direction)
        {
            return (float)Math.Cos(direction) * length;
        }
        public static float lengthdir_y(float length, float direction)
        {
            return (float)Math.Sin(direction) * length;
        }
        public static float lengthdir_x(int length, float direction)
        {
            return (float)Math.Cos(direction) * length;
        }
        public static float lengthdir_y(int length, float direction)
        {
            return (float)Math.Sin(direction) * length;
        }
        public static float lengthdir_x(float length, float direction, float pitch)
        {
            return (float)Math.Cos(direction) * (float)Math.Cos(pitch) * length;
        }
        public static float lengthdir_y(float length, float direction, float pitch)
        {
            return (float)Math.Sin(direction) * (float)Math.Cos(pitch) * length;
        }
        public static float lengthdir_z(float length, float pitch)
        {
            return (float)Math.Sin(pitch) * length;
        }
        public static float lengthdir_x(int length, float direction, float pitch)
        {
            return (float)Math.Cos(direction) * (float)Math.Cos(pitch) * length;
        }
        public static float lengthdir_y(int length, float direction, float pitch)
        {
            return (float)Math.Sin(direction) * (float)Math.Cos(pitch) * length;
        }
        public static float lengthdir_z(int length, float pitch)
        {
            return (float)Math.Sin(pitch) * length;
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

        public void Add(GamePoint2D a)
        {
            GamePoint2D tmp = Add(this, a);
            X = tmp.X;
            Y = tmp.Y;
        }
        public void Subtract(GamePoint2D a)
        {
            GamePoint2D tmp = Subtract(this, a);
            X = tmp.X;
            Y = tmp.Y;
        }
        public void Multiply(GamePoint2D a)
        {
            GamePoint2D tmp = Multiply(this, a);
            X = tmp.X;
            Y = tmp.Y;
        }
        public void Divide(GamePoint2D a)
        {
            GamePoint2D tmp = Divide(this, a);
            X = tmp.X;
            Y = tmp.Y;
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

        public void Add(GamePoint3D a)
        {
            GamePoint3D tmp = Add(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Subtract(GamePoint3D a)
        {
            GamePoint3D tmp = Subtract(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Multiply(GamePoint3D a)
        {
            GamePoint3D tmp = Multiply(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Divide(GamePoint3D a)
        {
            GamePoint3D tmp = Divide(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Add(GamePoint2D a)
        {
            GamePoint3D tmp = Add(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Subtract(GamePoint2D a)
        {
            GamePoint3D tmp = Subtract(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Multiply(GamePoint2D a)
        {
            GamePoint3D tmp = Multiply(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
        }
        public void Divide(GamePoint2D a)
        {
            GamePoint3D tmp = Divide(this, a);
            X = tmp.X;
            Y = tmp.Y;
            Z = tmp.Z;
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
            return new GamePoint3D(a.X / b.X, a.Y / b.Y, a.Z + b.Z);
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
    }
}
