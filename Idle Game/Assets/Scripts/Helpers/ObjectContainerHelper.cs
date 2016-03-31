using UnityEngine;
using System.Collections;
using System;

public static class ObjectContainerHelper
{
    public static void InitializeHashIds(string[] names, ref int[] hashIDs)
    {
        hashIDs = new int[names.Length];

        for (ushort index = 0; index < names.Length; index++)
        {
            if (null == names[index])
                Debug.LogErrorFormat("ObjectContainerHelper : Index {0} is null", names[index]);
            else
                hashIDs[index] = names[index].GetHashCode();
        }
    }

    public static int GetHashCodeIndex(string name, ref int[] hashIDs)
    {
        int hashCodeID = name.GetHashCode();
        int hashIndex = Array.FindIndex(hashIDs, hashId => hashId == hashCodeID);

        if (-1 == hashIndex)
            Debug.LogError("ObjectContainerHelper:  The reference of name \"" + name + "\" don't exist");

        return hashIndex;
    }
}
