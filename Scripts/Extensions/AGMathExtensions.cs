using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AGMathExtensions{


    /// <summary>
    /// Rounds the decimals of a float with a precision given.
    /// </summary>
    /// <param name="precision">The number of decimals to round. Example if float is 0.163f: with a 1 precision, would be 0.2f, and with 2 precision 0.17f.</param>
    /// <returns></returns>
    public static float RoundD(this float f, int precision = 2) {
        f = Mathf.Round(f * Mathf.Pow(10, precision)) / Mathf.Pow(10, precision);
        return f;
    }

}
