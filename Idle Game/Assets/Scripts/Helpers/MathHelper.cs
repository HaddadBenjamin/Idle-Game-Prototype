
using UnityEngine;

public class MathHelper : MonoBehaviour
{
    public static int GenerateRandomBeetweenTwoInts(int minimumValue, int maximumValue)
    {
        return Random.Range(minimumValue, maximumValue + 1);
    }

    public static float GenerateRandomBeetweenTwoFloats(float minimumValue, float maximumValue)
    {
        return Random.Range(minimumValue, maximumValue);
    }

    public static bool IsBeetweenOrEqual(int minimalValue, int maximumValue, int value)
    {
        return minimalValue <= value && maximumValue >= value;
    }

    public static bool IsBeetween(int minimalValue, int maximumValue, int value)
    {
        return minimalValue < value && maximumValue > value;
    }
}