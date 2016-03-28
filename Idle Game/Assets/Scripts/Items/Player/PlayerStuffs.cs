using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStuffs : MonoBehaviour
{
    #region Fields
    private PlayerCategoryStuffs[] allCategoriesStuffs;
    #endregion
   
    #region Unity Methods
    void Awake()
    {
        int enumerationStuffCategorySize = EnumHelper.Count<EStuffCategory>();

        this.allCategoriesStuffs = new PlayerCategoryStuffs[enumerationStuffCategorySize];

        for (int categoryStuffIndex = 0; categoryStuffIndex < enumerationStuffCategorySize; categoryStuffIndex++)
        this.allCategoriesStuffs[categoryStuffIndex] = new PlayerCategoryStuffs();
    }
    #endregion

    #region Behaviour Methods
    public PlayerStuffPrerequisite GetStuffPrerequisite(string name, EStuffCategory stuffCategory, EStuffQuality stuffQuality)
    {
        return this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].Get(stuffQuality, name);
    }

    public int GetStuffNumber(string name, EStuffCategory stuffCategory, EStuffQuality stuffQuality)
    {
        return this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].GetNumber(stuffQuality, name);
    }

    public void AddStuff(string name, EStuffCategory stuffCategory, EStuffQuality stuffQuality, int numberOfStuff)
    {
        Debug.LogFormat("Add Stuff : {1}, category {1} quality {2}", name, stuffCategory, stuffQuality);
        this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].AddNumber(name, stuffCategory, stuffQuality, numberOfStuff);
    }

    public void RemovEStuff(string name, EStuffCategory stuffCategory, EStuffQuality stuffQuality, int numberOfStuff)
    {
        this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffCategory)].RemoveNumber(name, stuffCategory, stuffQuality, numberOfStuff);
    }

    public bool HaveEnoughtStuff(StuffPrerequisite[] stuffsNeed)
    {
        for (int stuffIndex = 0; stuffIndex < stuffsNeed.Length; stuffIndex++)
        {
            if (!this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffsNeed[stuffIndex].StuffCategory)].HaveEnoughtStuff(stuffsNeed[stuffIndex]))
                return false;
        }

        return true;
    }

    public bool PayIfPossible(StuffPrerequisite[] stuffsNeed)
    {
        bool haveEnoughStuff = this.HaveEnoughtStuff(stuffsNeed);

        if (haveEnoughStuff)
            this.Pay(stuffsNeed);

        return haveEnoughStuff;
    }

    public void Pay(StuffPrerequisite[] stuffsNeed)
    {
        for (int stuffIndex = 0; stuffIndex < stuffsNeed.Length; stuffIndex++)
            this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffsNeed[stuffIndex].StuffCategory)].PayIfPossible(stuffsNeed[stuffIndex]);
    }

    public void Unpay(StuffPrerequisite[] stuffsNeed)
    {
       for (byte stuffIndex = 0; stuffIndex < stuffsNeed.Length; stuffIndex++)
            this.allCategoriesStuffs[EnumHelper.GetIndex<EStuffCategory>(stuffsNeed[stuffIndex].StuffCategory)].Unpay(stuffsNeed[stuffIndex]);
    }
    #endregion
}