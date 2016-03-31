using UnityEngine;

/// <summary>
/// Permet de placer nos tooltips en profondeur au dessus de tout, ce script n'est applicable que sur un rectTransform contenant toute nos infobulles.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class TooltipsContainer : MonoBehaviour
{
    #region Fields
    private Transform myTransform;
    #endregion

    #region Unity Methods
    void Awake()
    {
        this.myTransform = transform;

        name = "Tooltips Container";

        // Permet de placer cette objet tout en haut de la hiérarchie de la scène.
        myTransform.SetParent(null);
    }
    #endregion

    #region Behaviour Methods
    void Update()
    {
        // Permet de placer cette objet en tant que dernière enfant de la scène et donc faire en sorte que son contenu d'UI soit affiché en dernier.
        this.myTransform.SetAsLastSibling();
    }
    #endregion
}