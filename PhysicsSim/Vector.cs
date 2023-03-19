using System.Security.AccessControl;

namespace PhysicsSim
{
    public struct Vector
    {
        public float X, Y;

        /// <summary>
        /// Creates a new Vector, with all components set to zero.
        /// </summary>
        public Vector() { X = 0; Y = 0; }
        /// <summary>
        /// Creates a new vector from given components
        /// </summary>
        /// <param name="x">The vector's X component.</param>
        /// <param name="y">The vector's Y component</param>
        public Vector(float x, float y) { X = x; Y = y; }

        /// <summary>
        /// Component-wise addition.
        /// </summary>
        public static Vector operator +(Vector left, Vector right) => new Vector(left.X + right.X, left.Y + right.Y);
        /// <summary>
        /// Component-wise subtraction.
        /// </summary>
        public static Vector operator -(Vector left, Vector right) => new Vector(left.X - right.X, left.Y - right.Y);
        /// <summary>
        /// Component-wise negation.
        /// </summary>
        public static Vector operator -(Vector left) => new Vector(-left.X, -left.Y);
        /// <summary>
        /// Ninety degree counter-clockwise rotation.
        /// </summary>
        public static Vector operator ~(Vector left) => new Vector(-left.Y, left.X);
        /// <summary>
        /// Dot product.
        /// </summary>
        public static float operator *(Vector left, Vector right) => left.X * right.X + left.Y * right.Y;
        /// <summary>
        /// Component-wise multiplication by a scalar.
        /// </summary>
        public static Vector operator *(Vector left, float right) => new Vector(left.X * right, left.Y * right);
        /// <summary>
        /// Component-wise multiplication by a scalar.
        /// </summary>
        public static Vector operator *(float right, Vector left) => new Vector(left.X * right, left.X * right);
        /// <summary>
        /// Cross product (the 2D cross product is the length of its 3D counterpart).
        /// </summary>
        public static float operator %(Vector left, Vector right) => left.X * right.X - left.Y * right.Y;
        /// <summary>
        /// Component-wise multiplication.
        /// </summary>
        public static Vector operator &(Vector left, Vector right) => new Vector(left.X * right.X, left.Y * right.Y);
        /// <summary>
        /// Component-wise division.
        /// </summary>
        public static Vector operator /(Vector left, Vector right) => new Vector(left.X / right.X, left.Y / right.Y);
        /// <summary>
        /// Component-wise division by a scalar.
        /// </summary>
        public static Vector operator /(Vector left, float right) => new Vector(left.X / right, left.Y / right);
        public static bool operator ==(Vector left, Vector right) => left.X == right.X && left.Y == right.Y;
        public static bool operator !=(Vector left, Vector right) => left.X != right.X && left.Y != right.Y;
        public static bool operator <(Vector left, Vector right) => left.X < right.X && left.Y < right.Y;
        public static bool operator >(Vector left, Vector right) => left.X > right.X && left.Y > right.Y;
        public static bool operator <=(Vector left, Vector right) => left.X <= right.X && left.Y <= right.Y;
        public static bool operator >=(Vector left, Vector right) => left.X >= right.X && left.Y >= right.Y;
        public static implicit operator Vector((float, float) right) => new Vector(right.Item1, right.Item2);

        /// <summary>
        /// The square of the vector's length.
        /// </summary>
        public float SquareMagnitude { get => X * X + Y * Y; }
        /// <summary>
        /// The vector's length.
        /// </summary>
        public float Magnitude { get => MathF.Sqrt(X * X + Y * Y); }
        /// <summary>
        /// A unit length vector in the same direction
        /// </summary>
        public Vector Normalized { get => this / this.Magnitude; }

        /// <summary>
        /// Shorthand for new Vector(0, 0)
        /// </summary>
        public static readonly Vector Zero = new Vector(0, 0);
        /// <summary>
        /// Shorthand for new Vector(1, 1)
        /// </summary>
        public static readonly Vector One = new Vector(1, 1);
        /// <summary>
        /// Shorthand for new Vector(0, 1)
        /// </summary>
        public static readonly Vector Up = new Vector(0, 1);
        /// <summary>
        /// Shorthand for new Vector(0, -1)
        /// </summary>
        public static readonly Vector Down = new Vector(0, -1);
        /// <summary>
        /// Shorthand for new Vector(-1, 0)
        /// </summary>
        public static readonly Vector Left = new Vector(-1, 0);
        /// <summary>
        /// Shorthand for new Vector(1, 0)
        /// </summary>
        public static readonly Vector Right = new Vector(1, 0);

        public override bool Equals(object right) => this.X == ((Vector)right).X && this.Y == ((Vector)right).Y;
        public override int GetHashCode() => this.X.GetHashCode() ^ this.Y.GetHashCode();
        public override string ToString() => $"({X}, {Y})";
    }
}
