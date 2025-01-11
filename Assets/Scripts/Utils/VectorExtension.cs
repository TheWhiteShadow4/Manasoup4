using UnityEngine;

public static class VectorExtension
{
    public static Vector2 Floor(this Vector2 v)
    {
        return new Vector2(Mathf.Floor(v.x), Mathf.Floor(v.y));
    }

    public static Vector2 Abs(this Vector2 v)
    {
        return new Vector2(Mathf.Abs(v.x), Mathf.Abs(v.y));
    }

    public static Vector2 MinComponents(this Vector2 v, Vector2 other)
    {
        return new Vector2(Mathf.Min(v.x, other.x), Mathf.Min(v.y, other.y));
    }

    public static Vector2 MaxComponents(this Vector2 v, Vector2 other)
    {
        return new Vector2(Mathf.Max(v.x, other.x), Mathf.Max(v.y, other.y));
    }
}