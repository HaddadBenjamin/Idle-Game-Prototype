using UnityEngine;
using System.Collections;

public class MinimumMaximumBuilding
{
    #region Fields
    private int currentValue;
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
    public MinimumMaximumBuilding(int maximumValue = 3, int currentValue = 0)
    {
        this.Initialize(maximumValue, currentValue);
    }

    public void Initialize(int maximumValue, int currentValue)
    {
        this.maximumValue = maximumValue;
        this.currentValue = currentValue;
    }
    #endregion

    #region Behaviour
    public bool CanAdd(int valueAdded = 1)
    {
        return (valueAdded + this.currentValue) <= this.maximumValue;
    }

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

    public bool CanRemove(int valueRemove = 1)
    {
        return (this.currentValue - valueRemove) >= 0;
    }

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

    public void CallDelegate()
    {
        if (null != this.CurrentValueModificationDelegate)
            this.CurrentValueModificationDelegate(this.currentValue, this.maximumValue);
    }
    #endregion
}
