namespace SagaLib
{
    using System;

    /// <summary>
    /// Defines the <see cref="Vector2D" />.
    /// </summary>
    public class Vector2D
    {
        /// <summary>
        /// Defines the x.
        /// </summary>
        private ushort x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private ushort y;

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public ushort X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public ushort Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2D"/> class.
        /// </summary>
        /// <param name="x">The x<see cref="ushort"/>.</param>
        /// <param name="y">The y<see cref="ushort"/>.</param>
        public Vector2D(ushort x, ushort y)
        {
            this.x = x;
            this.y = y;
        }


        public static Vector2D operator +(Vector2D a, Vector2D b)
        {
            return new Vector2D((ushort)((uint)a.X + (uint)b.X), (ushort)((uint)a.Y + (uint)b.Y));
        }

        public static Vector2D operator -(Vector2D a, Vector2D b)
        {
            return new Vector2D((ushort)((uint)b.X - (uint)a.X), (ushort)((uint)b.Y - (uint)a.Y));
        }
        /// <summary>
        /// The GetDistance.
        /// </summary>
        /// <param name="a">The a<see cref="Vector2D"/>.</param>
        /// <param name="b">The b<see cref="Vector2D"/>.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        public static ushort GetDistance(Vector2D a, Vector2D b)
        {
            Vector2D vector2D = b - a;
            return (ushort)Math.Sqrt((double)((int)vector2D.X * (int)vector2D.X + (int)vector2D.Y * (int)vector2D.Y));
        }
    }
}
