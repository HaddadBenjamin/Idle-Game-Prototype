using System;

/// <summary>
/// Permet de s'abonner à des delegate ayant pour prototype : delegate void function().
/// </summary>
/// <typeparam name="EnumType"></typeparam>
public class EventManager<EnumType> where EnumType : struct, IConvertible
{
    #region Fields
    protected Action[] events;
    #endregion

    #region Constructor
    public EventManager()
    {
        if (!typeof(EnumType).IsEnum)
            throw new ArgumentException("EnumType must be an enumerated type");

        this.events = new Action[EnumHelper.Count<EnumType>()];
    }
    #endregion

    #region Behaviour Methods
    /// <summary>
    /// Permet de récupérer l'event lié à l'énumération EnumType.
    /// </summary>
    /// <param name="enumeration"></param>
    /// <returns></returns>
    protected Action GetEvent(EnumType enumeration)
    {
        return this.events[EnumHelper.GetIndex<EnumType>(enumeration)];
    }

    /// <summary>
    /// S'abonne à l'action ayant pour identifiant un EnumType.
    /// </summary>
    /// <param name="enumeration"></param>
    /// <param name="action"></param>
    public void SubcribeToEvent(EnumType enumeration, Action action)
    {
        this.events[EnumHelper.GetIndex<EnumType>(enumeration)] += action;
    }

    /// <summary>
    /// Se désabonne d'une action ayant pour identifiant un EnumType
    /// </summary>
    /// <param name="enumeration"></param>
    /// <param name="action"></param>
    public void UnsubcribeToEvent(EnumType enumeration, Action action)
    {
        this.events[EnumHelper.GetIndex<EnumType>(enumeration)] -= action;
    }

    /// <summary>
    /// Appele les actions lié à l'identifiant EnumType.
    /// </summary>
    /// <param name="enumeration"></param>
    public void CallEvent(EnumType enumeration)
    {
        Action action = this.GetEvent(enumeration);

        if (null != action)
            action();
    }
    #endregion
}