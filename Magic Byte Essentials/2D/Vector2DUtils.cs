using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EssentialMechanics.Logic
{
    public static class Vector2D
    {
        public static Vector2 add(this Vector2 vec, float x)
        {
            return new Vector2(vec.x + x, vec.y + x);
        }
        public static Vector2 add(this Vector2 vec, Vector2 x)
        {
            return new Vector2(vec.x + x.x, vec.y + x.y);
        }
        public static Vector2 remove(this Vector2 vec, float x)
        {
            return new Vector2(vec.x - x, vec.y - x);
        }
        public static Vector2 remove(this Vector2 vec, Vector2 x)
        {
            return new Vector2(vec.x - x.x, vec.y - x.y);
        }
        public static Vector2 setX(this Vector2 vec, float x)
        {
            return new Vector2(x, vec.y);
        }
        public static Vector2 setY(this Vector2 vec, float y)
        {
            return new Vector2(vec.x, y);
        }
        public static Vector2 addX(this Vector2 vec, float x)
        {
            return new Vector2(vec.x + x, vec.y);
        }
        public static Vector2 addY(this Vector2 vec, float y)
        {
            return new Vector2(vec.x, vec.y + y);
        }
        public static Vector2 removeX(this Vector2 vec, float x)
        {
            return new Vector2(vec.x - x, vec.y);
        }
        public static Vector2 removeY(this Vector2 vec, float y)
        {
            return new Vector2(vec.x, vec.y - y);
        }
        public static Vector2 multiply(this Vector2 vec, float x, float y)
        {
            return new Vector2(vec.x * x, vec.y * y);
        }
        public static Vector2 multiply(this Vector2 vec, Vector2 other)
        {
            return multiply(vec, other.x, other.y);
        }
        public static Vector2 clampVector2(this Vector2 vec, Vector2 min, Vector2 max)
        {
            vec.x = Mathf.Clamp(vec.x, min.x, max.x);
            vec.y = Mathf.Clamp(vec.y, min.y, max.y);

            return vec;
        }
        public static Vector2 absVector(this Vector2 vec)
        {
            return new Vector2(Mathf.Abs(vec.x), Mathf.Abs(vec.y));
        }
        public static Vector2 arithmeticAverage(this Vector2 vec, Vector2 other)
        {
            float x = (vec.x + other.x) / 2;
            float y = (vec.y + other.y) / 2;

            return new Vector2(x, y);
        }
        public static Vector2 Vec3toVec2(Vector3 vector)
        {
            return new Vector2(vector.x, vector.y);
        }
        public static Vector2 Vec2Int(Vector2 vector)
        {
            return new Vector2((int)vector.x, (int)vector.y);
        }
    }
}
