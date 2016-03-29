using UnityEngine;

public static class StuffHelper
{
    public static EStuffQuality GenerateStuffQuality()
    {
        int randomNumber = MathHelper.GenerateRandomBeetweenTwoInts(1, 2500);

        return  randomNumber <= 2000 ? EStuffQuality.Common :
                randomNumber <= 2200 ? EStuffQuality.Good :
                randomNumber <= 2350 ? EStuffQuality.Great :
                randomNumber <= 2415 ? EStuffQuality.Flawless :
                randomNumber <= 2445 ? EStuffQuality.Epic :
                randomNumber <= 2480 ? EStuffQuality.Legendary :
                                       EStuffQuality.Mythical;
    }
}