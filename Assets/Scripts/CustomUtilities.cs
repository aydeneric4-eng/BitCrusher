using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class CustomUtilities
{
    public static Vector2 Vec2ToVec3(Vector2 vect2, float zValue = 0)
    {
        return new Vector3(vect2.x, vect2.y, zValue);
    }
    public static Vector3 Vec3ToVec2(Vector3 vect3)
    {
        return new Vector2(vect3.x, vect3.y);
    }
    public static float GetAngleOf2DVect(Vector2 startPos, Vector2 targetPos)
    {
        Vector2 directonVect = targetPos - startPos; //Ai
        return Mathf.Atan2(directonVect.y, directonVect.x) * Mathf.Rad2Deg; //Ai
    }
    public static Vector3 GetVectorByAngleAndDistance(float distance, float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad) * distance, Mathf.Sin(angle * Mathf.Deg2Rad) * distance, 0);
    }

    private static Vector2 collisionAverageNormal;
    public static Vector3 GetAverageCollisionNormal(Collision2D collision)
    {
        collisionAverageNormal = Vector2.zero;
        foreach (ContactPoint2D contact in collision.contacts)
        {
            collisionAverageNormal += contact.normal;
        }
        return collisionAverageNormal / collision.contactCount;
    }

    public static bool HasTimeElapsed(float startTime, float requiredTime)
    {
        if (Time.time - startTime > requiredTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static float Sign(float value)
    {
        if (value == 0)
            return 0;
        else if (value > 0)
            return 1;
        else
            return -1;
    }
    public static float IsDesiredSign(float value, float desiredSign, float fallBack = 0)
    {
        if (Mathf.Sign(value) != desiredSign)
            return fallBack;
        else
            return value;
    }
    public static float AbsMin(float a, float b)
    {
        if (Mathf.Abs(a) == Mathf.Min(Mathf.Abs(a), Mathf.Abs(b)))
            return a;
        else
            return b;
    }
    public static float AbsMax(float a, float b)
    {
        if (Mathf.Abs(a) == Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)))
            return a;
        else
            return b;
    }


    public static IList<T> ShuffleList<T>(IList<T> ts) //Taken from https://discussions.unity.com/t/clever-way-to-shuffle-a-list-t-in-one-line-of-c-code/535113
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
        return ts;
    }
}
