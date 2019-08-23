using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    /// <summary>
    /// Adds space for every group of 3 zeros. F.x.: 1000000 => 1 000 000.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public static string GetSpaceSeparatedNumberText(int number)
    {
        return number.ToString("N0");
    }
}
