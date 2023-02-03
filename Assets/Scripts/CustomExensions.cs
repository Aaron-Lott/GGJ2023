using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CustomExtensions
{
    /// <summary>Return the closest value to a float within a range.</summary>
    /// <param name="number">The value to restrict to a range.</param>
    /// <param name="min">The smallest possible new value.</param>
    /// <param name="max">The largest possible new value.</param>
    public static float ToRange(this float number, float min, float max)
    {
        return Mathf.Min(max, Mathf.Max(min, number));
    }

    /// <summary>Return the closest value to an integer within a range.</summary>
    /// <param name="number">The value to restrict to a range.</param>
    /// <param name="min">The smallest possible new value.</param>
    /// <param name="max">The largest possible new value.</param>
    public static int ToRange(this int number, int min, int max)
    {
        return Mathf.Min(max, Mathf.Max(min, number));
    }
}
