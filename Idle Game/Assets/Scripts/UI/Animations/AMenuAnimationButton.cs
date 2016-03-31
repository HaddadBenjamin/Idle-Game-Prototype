using UnityEngine;
using UnityEngine.UI;

public abstract class AMenuAnimationButton : MonoBehaviour
{
    #region Fields & Properties
    public MenusAnimations MenusAnimations { get; private set; }
    public Button Button { get; private set; }
    #endregion

    #region Initializer
    protected void BaseStart()
    {
        this.Button = GetComponent<Button>();
        this.MenusAnimations = ServiceContainer.Instance.GameObjectReferencesArrayInScene.Get("Canvas", EGameObjectReferences.UI).GetComponent<MenusAnimations>();
    }
    #endregion
}