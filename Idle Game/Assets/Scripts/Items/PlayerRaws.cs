using UnityEngine;
using System.Collections;
using System;

public class PlayerRaws : MonoBehaviour
{
    #region Fields
    private PlayerRawPrerequisite[] raws;
    #endregion

    #region Constructor
    public PlayerRaws()
    {
        int enumerationRawCategorySize = EnumHelper.Count<ERaw>();

        this.raws = new PlayerRawPrerequisite[enumerationRawCategorySize];

        for (int rawIndex = 0; rawIndex < this.raws.Length; rawIndex++)
            this.raws[rawIndex] = new PlayerRawPrerequisite(0, (ERaw)rawIndex);
    }
    #endregion

    #region Behaviour Methods
    public int GetRawNumber(ERaw rawCategory)
    {
        return this.raws[EnumHelper.GetIndex<ERaw>(rawCategory)].Number;
    }

    public void AddRaw(ERaw rawCategory, int numberOfRaw)
    {
        this.raws[EnumHelper.GetIndex<ERaw>(rawCategory)].AddRaw(numberOfRaw);
    }

    public void RemoveRaw(ERaw rawCategory, int numberOfRaw)
    {
        this.raws[EnumHelper.GetIndex<ERaw>(rawCategory)].RemoveRaw(numberOfRaw);
    }

    public bool HaveEnoughtRaw(RawPrerequisite[] rawsNeed)
    {
        for (byte rawIndex = 0; rawIndex < rawsNeed.Length; rawIndex++)
        {
            if (this.raws[EnumHelper.GetIndex<ERaw>(rawsNeed[rawIndex].RawCategory)].Number <= rawsNeed[rawIndex].Number)
                return false;
        }

        return true;
    }

    public bool Pay(RawPrerequisite[] rawsNeed)
    {
        bool haveEnoughRaw = this.HaveEnoughtRaw(rawsNeed);

        if (haveEnoughRaw)
        {
            for (byte rawIndex = 0; rawIndex < rawsNeed.Length; rawIndex++)
                this.raws[EnumHelper.GetIndex<ERaw>(rawsNeed[rawIndex].RawCategory)].RemoveRaw(rawsNeed[rawIndex].Number);
        }

        return haveEnoughRaw;
    }

    public void Unpay(RawPrerequisite[] rawsNeed)
    {
        for (byte rawIndex = 0; rawIndex < rawsNeed.Length; rawIndex++)
            this.raws[EnumHelper.GetIndex<ERaw>(rawsNeed[rawIndex].RawCategory)].AddRaw(rawsNeed[rawIndex].Number);
    }
    #endregion
}
