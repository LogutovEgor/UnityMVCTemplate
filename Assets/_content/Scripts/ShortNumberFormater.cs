using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortNumberFormater
{
    public static string Format(float num)
    {
        char suffix = default;
        string numFormat = default;

        if (num >= 0 && num < 10000)
        {
            // 1 - 999
            numFormat = $"{Mathf.RoundToInt(num)}";
            suffix = default;
        }
        else if (num >= 10000 && num < 1000000)
        {
            // 1k-999k
            numFormat = $"{Mathf.RoundToInt(num / 1000)}";
            suffix = 'K';
        }
        else if (num >= 1000000 && num < 1000000000)
        {
            // 1m-999m
            numFormat = $"{Mathf.RoundToInt(num / 1000000)}";
            suffix = 'M';
        }
        else if (num >= 1000000000 && num < 1000000000000)
        {
            // 1b-999b
            numFormat = $"{Mathf.RoundToInt(num / 1000000000)}";
            suffix = 'B';
        }
        else if (num >= 1000000000000)
        {
            // 1t+
            numFormat = $"{Mathf.RoundToInt(num / 1000000000000)}";
            suffix = 'T';
        }
        return $"{numFormat}{suffix}";
    }
}
