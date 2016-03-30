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

    public static string TimeToString(float time)
    {
        System.TimeSpan timespan = System.TimeSpan.FromSeconds(time);

        return
            timespan.Hours > 0      ? string.Format("{0}h {1:D2}m", timespan.Hours, timespan.Minutes) :
            timespan.Minutes > 0    ? string.Format("{0}m {1:D2}s", timespan.Minutes, timespan.Seconds) :
                                      string.Format("{0}s",timespan.Seconds);
    }
}
