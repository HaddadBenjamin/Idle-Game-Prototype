using UnityEngine;
using System.Collections;
using System.Globalization;

public static class StringHelper
{
    /// <summary>
    /// 5000 -> 5,000
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static string PriceToText(int price)
    {
        string text = price.ToString("N", new CultureInfo("en-US"));
        
        return text.Remove(text.IndexOf('.'));
    }
}
