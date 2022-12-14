//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using static Unity.Mathematics.math;
using Unity.Mathematics;

#pragma warning disable 0660, 0661

namespace ME.ECS
{
    [DebuggerTypeProxy(typeof(fp2.DebuggerProxy))]
    [System.Serializable]
    public partial struct fp2 : System.IEquatable<fp2>, IFormattable
    {
        public fp x;
        public fp y;

        /// <summary>fp2 zero value.</summary>
        public static readonly fp2 zero;
        public static readonly fp2 one = new fp2(1f, 1f);

        public fp magnitude => fpmath.length(this);
        public fp sqrMagnitude => fpmath.lengthsq(this);
        public fp2 normalized => fpmath.normalizesafe(this);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize() {

            var v = fpmath.normalizesafe(this);
            this.x = v.x;
            this.y = v.y;

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp Dot(fp2 lhs, fp2 rhs) {

            return fpmath.dot(lhs, rhs);

        }

        /// <summary>Constructs a fp2 vector from two fp values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(fp x, fp y)
        { 
            this.x = x;
            this.y = y;
        }

        /// <summary>Constructs a fp2 vector from a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(fp2 xy)
        { 
            this.x = xy.x;
            this.y = xy.y;
        }

        /// <summary>Constructs a fp2 vector from a single fp value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(fp v)
        {
            this.x = v;
            this.y = v;
        }

        /// <summary>Constructs a fp2 vector from a single int value by converting it to fp and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(int v)
        {
            this.x = (fp)v;
            this.y = (fp)v;
        }

        /// <summary>Constructs a fp2 vector from a int2 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(int2 v)
        {
            this.x = (fp)v.x;
            this.y = (fp)v.y;
        }

        /// <summary>Constructs a fp2 vector from a single uint value by converting it to fp and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(uint v)
        {
            this.x = (fp)v;
            this.y = (fp)v;
        }

        /// <summary>Constructs a fp2 vector from a uint2 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public fp2(uint2 v)
        {
            this.x = (fp)v.x;
            this.y = (fp)v.y;
        }


        /// <summary>Implicitly converts a single fp value to a fp2 vector by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator fp2(fp v) { return new fp2(v); }

        /// <summary>Explicitly converts a single int value to a fp2 vector by converting it to fp and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator fp2(int v) { return new fp2(v); }

        /// <summary>Explicitly converts a int2 vector to a fp2 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator fp2(int2 v) { return new fp2(v); }

        /// <summary>Explicitly converts a single uint value to a fp2 vector by converting it to fp and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator fp2(uint v) { return new fp2(v); }

        /// <summary>Explicitly converts a uint2 vector to a fp2 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator fp2(uint2 v) { return new fp2(v); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator fp2(UnityEngine.Vector2 v) { return new fp2(v.x, v.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator UnityEngine.Vector2(fp2 v) { return new UnityEngine.Vector2(v.x, v.y); }


        /// <summary>Returns the result of a componentwise multiplication operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator * (fp2 lhs, fp2 rhs) { return new fp2 (lhs.x * rhs.x, lhs.y * rhs.y); }

        /// <summary>Returns the result of a componentwise multiplication operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator * (fp2 lhs, fp rhs) { return new fp2 (lhs.x * rhs, lhs.y * rhs); }

        /// <summary>Returns the result of a componentwise multiplication operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator * (fp lhs, fp2 rhs) { return new fp2 (lhs * rhs.x, lhs * rhs.y); }


        /// <summary>Returns the result of a componentwise addition operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator + (fp2 lhs, fp2 rhs) { return new fp2 (lhs.x + rhs.x, lhs.y + rhs.y); }

        /// <summary>Returns the result of a componentwise addition operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator + (fp2 lhs, fp rhs) { return new fp2 (lhs.x + rhs, lhs.y + rhs); }

        /// <summary>Returns the result of a componentwise addition operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator + (fp lhs, fp2 rhs) { return new fp2 (lhs + rhs.x, lhs + rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator + (UnityEngine.Vector2 lhs, fp2 rhs) { return new fp2 (lhs.x + rhs.x, lhs.y + rhs.y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator + (fp2 lhs, UnityEngine.Vector2 rhs) { return new fp2 (lhs.x + rhs.x, lhs.y + rhs.y); }


        /// <summary>Returns the result of a componentwise subtraction operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator - (fp2 lhs, fp2 rhs) { return new fp2 (lhs.x - rhs.x, lhs.y - rhs.y); }

        /// <summary>Returns the result of a componentwise subtraction operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator - (fp2 lhs, fp rhs) { return new fp2 (lhs.x - rhs, lhs.y - rhs); }

        /// <summary>Returns the result of a componentwise subtraction operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator - (fp lhs, fp2 rhs) { return new fp2 (lhs - rhs.x, lhs - rhs.y); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator - (UnityEngine.Vector2 lhs, fp2 rhs) { return new fp2 (lhs.x - rhs.x, lhs.y - rhs.y); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator - (fp2 lhs, UnityEngine.Vector2 rhs) { return new fp2 (lhs.x - rhs.x, lhs.y - rhs.y); }


        /// <summary>Returns the result of a componentwise division operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator / (fp2 lhs, fp2 rhs) { return new fp2 (lhs.x / rhs.x, lhs.y / rhs.y); }

        /// <summary>Returns the result of a componentwise division operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator / (fp2 lhs, fp rhs) { return new fp2 (lhs.x / rhs, lhs.y / rhs); }

        /// <summary>Returns the result of a componentwise division operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator / (fp lhs, fp2 rhs) { return new fp2 (lhs / rhs.x, lhs / rhs.y); }


        /// <summary>Returns the result of a componentwise modulus operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator % (fp2 lhs, fp2 rhs) { return new fp2 (lhs.x % rhs.x, lhs.y % rhs.y); }

        /// <summary>Returns the result of a componentwise modulus operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator % (fp2 lhs, fp rhs) { return new fp2 (lhs.x % rhs, lhs.y % rhs); }

        /// <summary>Returns the result of a componentwise modulus operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator % (fp lhs, fp2 rhs) { return new fp2 (lhs % rhs.x, lhs % rhs.y); }


        /// <summary>Returns the result of a componentwise increment operation on a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator ++ (fp2 val) { return new fp2 (++val.x, ++val.y); }


        /// <summary>Returns the result of a componentwise decrement operation on a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator -- (fp2 val) { return new fp2 (--val.x, --val.y); }


        /// <summary>Returns the result of a componentwise less than operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator < (fp2 lhs, fp2 rhs) { return (new bool2 (lhs.x < rhs.x, lhs.y < rhs.y)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise less than operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator < (fp2 lhs, fp rhs) { return (new bool2 (lhs.x < rhs, lhs.y < rhs)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise less than operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator < (fp lhs, fp2 rhs) { return (new bool2 (lhs < rhs.x, lhs < rhs.y)).Equals(new bool2(true, true)); }


        /// <summary>Returns the result of a componentwise less or equal operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <= (fp2 lhs, fp2 rhs) { return (new bool2 (lhs.x <= rhs.x, lhs.y <= rhs.y)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise less or equal operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <= (fp2 lhs, fp rhs) { return (new bool2 (lhs.x <= rhs, lhs.y <= rhs)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise less or equal operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <= (fp lhs, fp2 rhs) { return (new bool2 (lhs <= rhs.x, lhs <= rhs.y)).Equals(new bool2(true, true)); }


        /// <summary>Returns the result of a componentwise greater than operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator > (fp2 lhs, fp2 rhs) { return (new bool2 (lhs.x > rhs.x, lhs.y > rhs.y)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise greater than operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator > (fp2 lhs, fp rhs) { return (new bool2 (lhs.x > rhs, lhs.y > rhs)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise greater than operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator > (fp lhs, fp2 rhs) { return (new bool2 (lhs > rhs.x, lhs > rhs.y)).Equals(new bool2(true, true)); }


        /// <summary>Returns the result of a componentwise greater or equal operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >= (fp2 lhs, fp2 rhs) { return (new bool2 (lhs.x >= rhs.x, lhs.y >= rhs.y)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >= (fp2 lhs, fp rhs) { return (new bool2 (lhs.x >= rhs, lhs.y >= rhs)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise greater or equal operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >= (fp lhs, fp2 rhs) { return (new bool2 (lhs >= rhs.x, lhs >= rhs.y)).Equals(new bool2(true, true)); }


        /// <summary>Returns the result of a componentwise unary minus operation on a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator - (fp2 val) { return new fp2 (-val.x, -val.y); }


        /// <summary>Returns the result of a componentwise unary plus operation on a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 operator + (fp2 val) { return new fp2 (+val.x, +val.y); }


        /// <summary>Returns the result of a componentwise equality operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (fp2 lhs, fp2 rhs) { return (new bool2 (lhs.x == rhs.x, lhs.y == rhs.y)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise equality operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (fp2 lhs, fp rhs) { return (new bool2 (lhs.x == rhs, lhs.y == rhs)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise equality operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (fp lhs, fp2 rhs) { return (new bool2 (lhs == rhs.x, lhs == rhs.y)).Equals(new bool2(true, true)); }


        /// <summary>Returns the result of a componentwise not equal operation on two fp2 vectors.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (fp2 lhs, fp2 rhs) { return (new bool2 (lhs.x != rhs.x, lhs.y != rhs.y)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise not equal operation on a fp2 vector and a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (fp2 lhs, fp rhs) { return (new bool2 (lhs.x != rhs, lhs.y != rhs)).Equals(new bool2(true, true)); }

        /// <summary>Returns the result of a componentwise not equal operation on a fp value and a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (fp lhs, fp2 rhs) { return (new bool2 (lhs != rhs.x, lhs != rhs.y)).Equals(new bool2(true, true)); }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (fp2 lhs, UnityEngine.Vector2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y).Equals(new bool2(true, true)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (fp2 lhs, UnityEngine.Vector2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y).Equals(new bool2(true, true)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator != (UnityEngine.Vector2 lhs, fp2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y).Equals(new bool2(true, true)); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator == (UnityEngine.Vector2 lhs, fp2 rhs) { return new bool2 (lhs.x != rhs.x, lhs.y != rhs.y).Equals(new bool2(true, true)); }




        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 xyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(x, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yxxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yxxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yxyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yxyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yyxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yyxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yyyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp4 yyyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp4(y, y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 xxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(x, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 xxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(x, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 xyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(x, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 xyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(x, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 yxx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(y, x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 yxy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(y, x, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 yyx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(y, y, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp3 yyy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp3(y, y, y); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp2 xx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp2(x, x); }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp2 xy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp2(x, y); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { x = value.x; y = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp2 yx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp2(y, x); }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set { y = value.x; x = value.y; }
        }


        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public fp2 yy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get { return new fp2(y, y); }
        }



        /// <summary>Returns the fp element at a specified index.</summary>
        unsafe public fp this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (fp2* array = &this) { return ((fp*)array)[index]; }
            }
            set
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                if ((uint)index >= 2)
                    throw new System.ArgumentException("index must be between[0...1]");
#endif
                fixed (fp* array = &x) { array[index] = value; }
            }
        }

        /// <summary>Returns true if the fp2 is equal to a given fp2, false otherwise.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(fp2 rhs) { return x == rhs.x && y == rhs.y; }

        /// <summary>Returns true if the fp2 is equal to a given fp2, false otherwise.</summary>
        public override bool Equals(object o) { return Equals((fp2)o); }


        /// <summary>Returns a hash code for the fp2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode() { return (int)fpmath.hash(this); }


        /// <summary>Returns a string representation of the fp2.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return string.Format("fp2({0}, {1})", x, y);
        }

        /// <summary>Returns a string representation of the fp2 using a specified format and culture-specific format information.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format("fp2({0}, {1})", x.ToString(format, formatProvider), y.ToString(format, formatProvider));
        }

        internal sealed class DebuggerProxy
        {
            public fp x;
            public fp y;
            public DebuggerProxy(fp2 v)
            {
                x = v.x;
                y = v.y;
            }
        }

    }

    public static partial class fpmath
    {
        /// <summary>Returns a fp2 vector constructed from two fp values.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(fp x, fp y) { return new fp2(x, y); }

        /// <summary>Returns a fp2 vector constructed from a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(fp2 xy) { return new fp2(xy); }

        /// <summary>Returns a fp2 vector constructed from a single fp value by assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(fp v) { return new fp2(v); }

        /// <summary>Returns a fp2 vector constructed from a single int value by converting it to fp and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(int v) { return new fp2(v); }

        /// <summary>Return a fp2 vector constructed from a int2 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(int2 v) { return new fp2(v); }

        /// <summary>Returns a fp2 vector constructed from a single uint value by converting it to fp and assigning it to every component.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(uint v) { return new fp2(v); }

        /// <summary>Return a fp2 vector constructed from a uint2 vector by componentwise conversion.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 fp2(uint2 v) { return new fp2(v); }

        /// <summary>Returns a uint hash code of a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint hash(fp2 v)
        {
            return math.csum(fpmath.asuint(v) * uint2(0x6E624EB7u, 0x7383ED49u)) + 0xDD49C23Bu;
        }

        /// <summary>
        /// Returns a uint2 vector hash code of a fp2 vector.
        /// When multiple elements are to be hashes together, it can more efficient to calculate and combine wide hash
        /// that are only reduced to a narrow uint hash at the very end instead of at every step.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint2 hashwide(fp2 v)
        {
            return (fpmath.asuint(v) * uint2(0xEBD0D005u, 0x91475DF7u)) + 0x55E84827u;
        }

        /// <summary>Returns the result of specified shuffling of the components from two fp2 vectors into a fp value.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp shuffle(fp2 a, fp2 b, ShuffleComponent x)
        {
            return select_shuffle_component(a, b, x);
        }

        /// <summary>Returns the result of specified shuffling of the components from two fp2 vectors into a fp2 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp2 shuffle(fp2 a, fp2 b, ShuffleComponent x, ShuffleComponent y)
        {
            return fp2(
                select_shuffle_component(a, b, x),
                select_shuffle_component(a, b, y));
        }

        /// <summary>Returns the result of specified shuffling of the components from two fp2 vectors into a fp3 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp3 shuffle(fp2 a, fp2 b, ShuffleComponent x, ShuffleComponent y, ShuffleComponent z)
        {
            return fp3(
                select_shuffle_component(a, b, x),
                select_shuffle_component(a, b, y),
                select_shuffle_component(a, b, z));
        }

        /// <summary>Returns the result of specified shuffling of the components from two fp2 vectors into a fp4 vector.</summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static fp4 shuffle(fp2 a, fp2 b, ShuffleComponent x, ShuffleComponent y, ShuffleComponent z, ShuffleComponent w)
        {
            return fp4(
                select_shuffle_component(a, b, x),
                select_shuffle_component(a, b, y),
                select_shuffle_component(a, b, z),
                select_shuffle_component(a, b, w));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static fp select_shuffle_component(fp2 a, fp2 b, ShuffleComponent component)
        {
            switch(component)
            {
                case ShuffleComponent.LeftX:
                    return a.x;
                case ShuffleComponent.LeftY:
                    return a.y;
                case ShuffleComponent.RightX:
                    return b.x;
                case ShuffleComponent.RightY:
                    return b.y;
                default:
                    throw new System.ArgumentException("Invalid shuffle component: " + component);
            }
        }

    }
}
