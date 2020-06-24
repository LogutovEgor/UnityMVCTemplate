using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class SupportFunctions
{
    public static int RandomChance(float[] probs)
    {
        float total = probs.Sum();

        float randomPoint = Random.value * total;

        for (int i = 0; i < probs.Length; i++)
            if (randomPoint < probs[i])
                return i;
            else
                randomPoint -= probs[i];
         
        return probs.Length - 1;
    }

    public static List<Vector3> ConvertLineToBrokenLine(Vector3 from, Vector3 to, int segmentCount, float distance)
    {
        List<Vector3> result = new List<Vector3>()
        {
            from
        };
        bool switchSide = true;
        for (float t = 0; t <= 1; t += 1f / segmentCount)
        {
            Vector2 pointOnLine = Vector2.Lerp(from, to, t);
            Vector2 perpendicular = Vector2.Perpendicular(switchSide ? pointOnLine : -pointOnLine);// + new Vector2(0, 0.2f));
            result.Add(pointOnLine + perpendicular.normalized * distance);
            switchSide = !switchSide;
        }

        result.Add(to);

        //Debug.DrawLine(result.First(), result.Last(), Color.red, Time.deltaTime);
        //for (int i = 0; i < result.Count - 1; i++)
        //{
        //    Debug.DrawLine(result[i], result[i + 1], Color.Lerp(Color.green, Color.red, (float)((float)i / (float)result.Count)), 100);
        //}

        return result;
    }

    public static List<Vector3> GetPointsOnCircle(Vector3 center, float r, int segmentCount)
    {
        List<Vector3> points = new List<Vector3>();
        //
        for (float t = 0; t <= 2 * Mathf.PI; t += (float)(Mathf.PI / (float)segmentCount))
        {
            float x = center.x + r * Mathf.Cos(t);
            float y = center.y + r * Mathf.Sin(t);
            Vector3 point = new Vector3(x, y);
            points.Add(point);
        }

        //for (int i = 0; i < points.Count - 1; i++)
        //    Debug.DrawLine(points[i], points[i + 1], Color.Lerp(Color.green, Color.red, (float)((float)i / (float)points.Count)), 100);

        return points;
    }

    public static bool InRange(float min, float max, float value)
        => value >= min && value <= max;

    public static int[] Vector2IntToArray(Vector2Int vector3Int)
    {
        int[] result = new int[2]
        {
            vector3Int.x,
            vector3Int.y
        };
        return result;
    }

    public static Vector2Int ArrayToVector2Int(int[] array)
    {
        Vector2Int result = new Vector2Int(array[0], array[1]);
        return result;
    }


    public static int[] Vector3IntToArray(Vector3Int vector3Int)
    {
        int[] result = new int[3]
        {
            vector3Int.x,
            vector3Int.y,
            vector3Int.z
        };
        return result;
    }

    public static Vector3Int ArrayToVector3Int(int[] array)
    {
        Vector3Int result = new Vector3Int(array[0], array[1], array[2]);
        return result;
    }
}
