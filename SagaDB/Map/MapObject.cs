namespace SagaDB.Map
{
    using SagaLib;
    using System;

    /// <summary>
    /// Defines the <see cref="MapObject" />.
    /// </summary>
    [Serializable]
    public class MapObject
    {
        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the width.
        /// </summary>
        private int width;

        /// <summary>
        /// Defines the height.
        /// </summary>
        private int height;

        /// <summary>
        /// Defines the centerX.
        /// </summary>
        private int centerX;

        /// <summary>
        /// Defines the centerY.
        /// </summary>
        private int centerY;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private byte y;

        /// <summary>
        /// Defines the flag.
        /// </summary>
        private BitMask flag;

        /// <summary>
        /// Defines the dir.
        /// </summary>
        private byte dir;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the Width.
        /// </summary>
        public int Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
            }
        }

        /// <summary>
        /// Gets or sets the Height.
        /// </summary>
        public int Height
        {
            get
            {
                return this.height;
            }
            set
            {
                this.height = value;
            }
        }

        /// <summary>
        /// Gets or sets the CenterX.
        /// </summary>
        public int CenterX
        {
            get
            {
                return this.centerX;
            }
            set
            {
                this.centerX = value;
            }
        }

        /// <summary>
        /// Gets or sets the CenterY.
        /// </summary>
        public int CenterY
        {
            get
            {
                return this.centerY;
            }
            set
            {
                this.centerY = value;
            }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public byte X
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
        public byte Y
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
        /// Gets or sets the Flag.
        /// </summary>
        public BitMask Flag
        {
            get
            {
                return this.flag;
            }
            set
            {
                this.flag = value;
            }
        }

        /// <summary>
        /// Gets or sets the Dir.
        /// </summary>
        public byte Dir
        {
            get
            {
                return this.dir;
            }
            set
            {
                this.dir = value;
            }
        }

        /// <summary>
        /// Gets the Clone.
        /// </summary>
        public MapObject Clone
        {
            get
            {
                return new MapObject()
                {
                    Name = this.name,
                    width = this.width,
                    height = this.height,
                    centerX = this.centerX,
                    centerY = this.centerY,
                    flag = new BitMask(this.flag.Value)
                };
            }
        }

        /// <summary>
        /// Gets the PositionMatrix.
        /// </summary>
        public int[,][] PositionMatrix
        {
            get
            {
                int[,][] numArray1 = new int[this.width, this.height][];
                for (int index1 = 0; index1 < this.width; ++index1)
                {
                    for (int index2 = 0; index2 < this.height; ++index2)
                    {
                        numArray1[index1, index2] = new int[2];
                        numArray1[index1, index2][0] = index1 - this.centerX;
                        numArray1[index1, index2][1] = index2 - this.centerY;
                    }
                }
                double num1 = (double)((int)this.dir * 45) * Math.PI / 180.0;
                double[,] numArray2 = new double[2, 2]
                {
          {
            Math.Cos(num1),
            Math.Sin(num1)
          },
          {
            -Math.Sin(num1),
            Math.Cos(num1)
          }
                };
                for (int index1 = 0; index1 < this.width; ++index1)
                {
                    for (int index2 = 0; index2 < this.height; ++index2)
                    {
                        double num2 = Math.Round(numArray2[0, 0] * (double)numArray1[index1, index2][0] + numArray2[1, 0] * (double)-numArray1[index1, index2][1], 3);
                        double num3 = Math.Round(-(numArray2[0, 1] * (double)numArray1[index1, index2][0] + numArray2[1, 1] * (double)-numArray1[index1, index2][1]), 3);
                        numArray1[index1, index2][0] = num2 <= 0.0 ? (int)Math.Floor(num2) : (int)Math.Ceiling(num2);
                        numArray1[index1, index2][1] = num3 <= 0.0 ? (int)Math.Floor(num3) : (int)Math.Ceiling(num3);
                    }
                }
                return numArray1;
            }
        }
    }
}
