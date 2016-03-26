using UnityEngine;
using System.Collections;

/// <summary>
/// Permet d'obtenir des informations sur le nombre de bâtiment posé et permet aussi de limiter le nombre de bâtiment.
/// </summary>
public class BuildingAnalytic
{
    #region Fields
    /// <summary>
    /// Nombre de bâtiment du type posé.
    /// </summary>
    private int currentValue;
    /// <summary>
    /// Limite du nombre de bâtiment posé.
    /// </summary>
    private int maximumValue;

    public delegate void Delegate(int current, int maximum);
    public Delegate CurrentValueModificationDelegate;
    #endregion

    #region Properties
    public int CurrentValue
    {
        get { return currentValue; }
        private set { currentValue = value; }
    }

    public int MaximumValue
    {
        get { return maximumValue; }
        private set { maximumValue = value; }
    }
    #endregion

    #region Initialize
    public BuildingAnalytic(int maximumValue = 3, int currentValue = 0)
    {
        this.Initialize(maximumValue, currentValue);
    }

    // Permet de définir une valeur à maximumValue et currentValue.
    public void Initialize(int maximumValue, int currentValue)
    {
        this.maximumValue = maximumValue;
        this.currentValue = currentValue;
    }
    #endregion

    #region Behaviour
    /// <summary>
    /// Déterminer si il est possible de rajouter une valeur à currentValue.
    /// </summary>
    /// <param name="valueAdded"></param>
    /// <returns></returns>
    public bool CanAdd(int valueAdded = 1)
    {
        return (valueAdded + this.currentValue) <= this.maximumValue;
    }

    /// <summary>
    /// Ajoute une valeur à currentValue si cela est possible.
    /// </summary>
    /// <param name="valueAdded"></param>
    /// <returns></returns>
    public bool Add(int valueAdded = 1)
    {
        bool canAdd = this.CanAdd(valueAdded);

        if (canAdd)
        {
            this.currentValue += valueAdded;
            this.CallDelegate();
        }

        return canAdd;
    }

    /// <summary>
    /// Détermine si il est possible d'enlever une valeur à currentValue.
    /// </summary>
    /// <param name="valueRemove"></param>
    /// <returns></returns>
    public bool CanRemove(int valueRemove = 1)
    {
        return (this.currentValue - valueRemove) >= 0;
    }

    /// <summary>
    /// Enlève une valeur à currentValue si cela est possible
    /// </summary>
    /// <param name="valueRemove"></param>
    /// <returns></returns>
    public bool Remove(int valueRemove = 1)
    {
        bool canRemove = this.CanRemove(valueRemove);

        if (canRemove)
        {
            this.currentValue -= valueRemove;
            this.CallDelegate();
        }

        return canRemove;
    }

    /// <summary>
    /// Appele la delegate currentvaluemodification si elle existe.
    /// </summary>
    public void CallDelegate()
    {
        if (null != this.CurrentValueModificationDelegate)
            this.CurrentValueModificationDelegate(this.currentValue, this.maximumValue);
    }
    #endregion
}
