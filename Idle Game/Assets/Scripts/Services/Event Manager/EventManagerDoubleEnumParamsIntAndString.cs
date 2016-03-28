using System;

/// <summary>
/// Permet de s'abonner à des delegate ayant pour prototype : delegate void function().
/// </summary>
/// <typeparam name="EnumType"></typeparam>
public class EventManagerDoubleEnumParamsIntAndString<EnumTypeA, EnumTypeB>
    where EnumTypeA : struct, IConvertible
    where EnumTypeB : struct, IConvertible
{
    #region Fields
    protected Action<int, string>[][] events;
    #endregion

    #region Constructor
    public EventManagerDoubleEnumParamsIntAndString()
    {
        if (!typeof(EnumTypeA).IsEnum || !typeof(EnumTypeB).IsEnum)
            throw new ArgumentException("EnumType must be an enumerated type");

        int enumACount = EnumHelper.Count<EnumTypeA>();
        int enumBCount = EnumHelper.Count<EnumTypeB>();

        this.events = new Action<int, string>[enumACount][];

        for (int i = 0; i < this.events.Length; i++)
           this.events[i] = new Action<int, string>[enumBCount];
    }
    #endregion

    #region Behaviour Methods
    /// <summary>
    /// Permet de récupérer l'event lié à l'énumération EnumType.
    /// </summary>
    /// <param name="enumeration"></param>
    /// <returns></returns>
    protected Action<int, string> GetEvent(EnumTypeA enumerationA, EnumTypeB enumerationB)
    {
        return this.events[EnumHelper.GetIndex<EnumTypeA>(enumerationA)][EnumHelper.GetIndex<EnumTypeB>(enumerationB)];
    }

    /// <summary>
    /// S'abonne à l'action ayant pour identifiant un EnumType.
    /// </summary>
    /// <param name="enumeration"></param>
    /// <param name="action"></param>
    public void SubcribeToEvent(EnumTypeA enumerationA, EnumTypeB enumerationB, Action<int, string> action)
    {
        this.events[EnumHelper.GetIndex<EnumTypeA>(enumerationA)][EnumHelper.GetIndex<EnumTypeB>(enumerationB)] += action;
    }

    /// <summary>
    /// Se désabonne d'une action ayant pour identifiant un EnumType
    /// </summary>
    /// <param name="enumeration"></param>
    /// <param name="action"></param>
    public void UnsubcribeToEvent(EnumTypeA enumerationA, EnumTypeB enumerationB, Action<int, string> action)
    {
        this.events[EnumHelper.GetIndex<EnumTypeA>(enumerationA)][EnumHelper.GetIndex<EnumTypeB>(enumerationB)] -= action;
    }

    /// <summary>
    /// Appele les actions lié à l'identifiant EnumType.
    /// </summary>
    /// <param name="enumeration"></param>
    public void CallEvent(EnumTypeA enumerationA, EnumTypeB enumerationB, int paramInt, string paramString)
    {
        Action<int, string> action = this.GetEvent(enumerationA, enumerationB);

        if (null != action)
            action(paramInt, paramString);
    }
    #endregion
}