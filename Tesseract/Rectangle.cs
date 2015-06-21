using System;
using System.Globalization;

namespace Tesseract
{
    public struct Rectangle
    {
        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Empty"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Stores the location and size of a rectangular region. For
        ///             more advanced region functions use a
        ///             <see cref="System.Drawing.Region">
        ///                 object.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public static readonly Rectangle Empty = new Rectangle();

        private float height;
        private float width;
        private float x;
        private float y;

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Rectangle"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Initializes a new instance of the
        ///             <see cref="System.Drawing.Rectangle">
        ///                 class with the specified location and size.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public Rectangle(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.X"]/*">
        ///     <devdoc>
        ///         Gets or sets the x-coordinate of the
        ///         upper-left corner of the rectangular region defined by this
        ///         <see cref="System.Drawing.Rectangle">
        ///             .
        ///         </see>
        ///     </devdoc>
        public float X
        {
            get { return x; }
            set { x = value; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Y"]/*">
        ///     <devdoc>
        ///         Gets or sets the y-coordinate of the
        ///         upper-left corner of the rectangular region defined by this
        ///         <see cref="System.Drawing.Rectangle">
        ///             .
        ///         </see>
        ///     </devdoc>
        public float Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Width"]/*">
        ///     <devdoc>
        ///         Gets or sets the width of the rectangular
        ///         region defined by this
        ///         <see cref="System.Drawing.Rectangle">
        ///             .
        ///         </see>
        ///     </devdoc>
        public float Width
        {
            get { return width; }
            set { width = value; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Height"]/*">
        ///     <devdoc>
        ///         Gets or sets the width of the rectangular
        ///         region defined by this
        ///         <see cref="System.Drawing.Rectangle">
        ///             .
        ///         </see>
        ///     </devdoc>
        public float Height
        {
            get { return height; }
            set { height = value; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Left"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Gets the x-coordinate of the upper-left corner of the
        ///             rectangular region defined by this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 .
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public float Left
        {
            get { return X; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Top"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Gets the y-coordinate of the upper-left corner of the
        ///             rectangular region defined by this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 .
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public float Top
        {
            get { return Y; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Right"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Gets the x-coordinate of the lower-right corner of the
        ///             rectangular region defined by this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 .
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public float Right
        {
            get { return X + Width; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Bottom"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Gets the y-coordinate of the lower-right corner of the
        ///             rectangular region defined by this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 .
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public float Bottom
        {
            get { return Y + Height; }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.IsEmpty"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Tests whether this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 has a
        ///                 <see cref="System.Drawing.Rectangle.Width">
        ///                     or a
        ///                     <see cref="System.Drawing.Rectangle.Height">
        ///                         of 0.
        ///                     </see>
        ///                 </see>
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public bool IsEmpty
        {
            get
            {
                return height == 0 && width == 0 && x == 0 && y == 0;
                // C++ uses this definition:
                // return(Width <= 0 )|| (Height <= 0);
            }
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.FromLTRB"]/*">
        ///     <devdoc>
        ///         Creates a new
        ///         <see cref="System.Drawing.Rectangle">
        ///             with
        ///             the specified location and size.
        ///         </see>
        ///     </devdoc>
        // !! Not in C++ version 
        public static Rectangle FromLTRB(float left, float top, float right, float bottom)
        {
            return new Rectangle(left,
                top,
                right - left,
                bottom - top);
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Equals"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Tests whether
        ///             <paramref name="obj">
        ///                 is a
        ///                 <see cref="System.Drawing.Rectangle">
        ///                     with
        ///                     the same location and size of this Rectangle.
        ///                 </see>
        ///             </paramref>
        ///         </para>
        ///     </devdoc>
        public override bool Equals(object obj)
        {
            if (!(obj is Rectangle))
                return false;

            var comp = (Rectangle) obj;

            return (comp.X == X) &&
                   (comp.Y == Y) &&
                   (comp.Width == Width) &&
                   (comp.Height == Height);
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.operator=="]/*">
        ///     <devdoc>
        ///         <para>
        ///             Tests whether two
        ///             <see cref="System.Drawing.Rectangle">
        ///                 objects have equal location and size.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public static bool operator ==(Rectangle left, Rectangle right)
        {
            return (left.X == right.X
                    && left.Y == right.Y
                    && left.Width == right.Width
                    && left.Height == right.Height);
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.operator!="]/*">
        ///     <devdoc>
        ///         <para>
        ///             Tests whether two
        ///             <see cref="System.Drawing.Rectangle">
        ///                 objects differ in location or size.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public static bool operator !=(Rectangle left, Rectangle right)
        {
            return !(left == right);
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Contains"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Determines if the specfied pofloat is contained within the
        ///             rectangular region defined by this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 .
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public bool Contains(float x, float y)
        {
            return X <= x &&
                   x < X + Width &&
                   Y <= y &&
                   y < Y + Height;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Contains2"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Determines if the rectangular region represented by
        ///             <paramref name="rect">
        ///                 is entirely contained within the rectangular region represented by
        ///                 this
        ///                 <see cref="System.Drawing.Rectangle">
        ///                     .
        ///                 </see>
        ///             </paramref>
        ///         </para>
        ///     </devdoc>
        public bool Contains(Rectangle rect)
        {
            return (X <= rect.X) &&
                   ((rect.X + rect.Width) <= (X + Width)) &&
                   (Y <= rect.Y) &&
                   ((rect.Y + rect.Height) <= (Y + Height));
        }

        // !! Not in C++ version 
        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.GetHashCode"]/*">
        ///     <devdoc>
        ///         <para>[To be supplied.]</para>
        ///     </devdoc>
        public override int GetHashCode()
        {
            return (int) ((uint) X ^
                          (((uint) Y << 13) | ((uint) Y >> 19)) ^
                          (((uint) Width << 26) | ((uint) Width >> 6)) ^
                          (((uint) Height << 7) | ((uint) Height >> 25)));
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Inflate"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Inflates this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 by the specified amount.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public void Inflate(float width, float height)
        {
            X -= width;
            Y -= height;
            Width += 2*width;
            Height += 2*height;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Inflate2"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Creates a
        ///             <see cref="System.Drawing.Rectangle">
        ///                 that is inflated by the specified amount.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        // !! Not in C++ 
        public static Rectangle Inflate(Rectangle rect, float x, float y)
        {
            var r = rect;
            r.Inflate(x, y);
            return r;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.floatersect"]/*">
        ///     <devdoc>
        ///         Creates a Rectangle that represents the floatersection between this Rectangle and rect.
        ///     </devdoc>
        public void floatersect(Rectangle rect)
        {
            var result = floatersect(rect, this);

            X = result.X;
            Y = result.Y;
            Width = result.Width;
            Height = result.Height;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.floatersect1"]/*">
        ///     <devdoc>
        ///         Creates a rectangle that represents the floatersetion between a and
        ///         b. If there is no floatersection, null is returned.
        ///     </devdoc>
        public static Rectangle floatersect(Rectangle a, Rectangle b)
        {
            var x1 = Math.Max(a.X, b.X);
            var x2 = Math.Min(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Max(a.Y, b.Y);
            var y2 = Math.Min(a.Y + a.Height, b.Y + b.Height);

            if (x2 >= x1
                && y2 >= y1)
            {
                return new Rectangle(x1, y1, x2 - x1, y2 - y1);
            }
            return Empty;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.floatersectsWith"]/*">
        ///     <devdoc>
        ///         Determines if this rectangle floatersets with rect.
        ///     </devdoc>
        public bool floatersectsWith(Rectangle rect)
        {
            return (rect.X < X + Width) &&
                   (X < (rect.X + rect.Width)) &&
                   (rect.Y < Y + Height) &&
                   (Y < rect.Y + rect.Height);
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Union"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Creates a rectangle that represents the union between a and
        ///             b.
        ///         </para>
        ///     </devdoc>
        public static Rectangle Union(Rectangle a, Rectangle b)
        {
            var x1 = Math.Min(a.X, b.X);
            var x2 = Math.Max(a.X + a.Width, b.X + b.Width);
            var y1 = Math.Min(a.Y, b.Y);
            var y2 = Math.Max(a.Y + a.Height, b.Y + b.Height);

            return new Rectangle(x1, y1, x2 - x1, y2 - y1);
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.Offset1"]/*">
        ///     <devdoc>
        ///         Adjusts the location of this rectangle by the specified amount.
        ///     </devdoc>
        public void Offset(float x, float y)
        {
            X += x;
            Y += y;
        }

        /// <include file="doc\Rectangle.uex" path="docs/doc[@for=" Rectangle.ToString"]/*">
        ///     <devdoc>
        ///         <para>
        ///             Converts the attributes of this
        ///             <see cref="System.Drawing.Rectangle">
        ///                 to a
        ///                 human readable string.
        ///             </see>
        ///         </para>
        ///     </devdoc>
        public override string ToString()
        {
            return "{X=" + X.ToString(CultureInfo.CurrentCulture) + ",Y=" + Y.ToString(CultureInfo.CurrentCulture) +
                   ",Width=" + Width.ToString(CultureInfo.CurrentCulture) +
                   ",Height=" + Height.ToString(CultureInfo.CurrentCulture) + "}";
        }
    }
}