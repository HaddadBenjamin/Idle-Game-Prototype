using UnityEngine;

using UnityEngine;
using UnityEngine.UI;

public class SellBuildingButtonTooltip : AResourcePrerequisiteTooltip
{
    #region Behaviour Methods
    public override void SetContent(ResourcePrerequisite[] content, bool positiveColor = false)
    {
        this.resourcePrerequisite = content != null ? content : new ResourcePrerequisite[0];
        this.rectTransform.SetHeight(140.0f + this.grid.GetHeight(content != null ? this.resourcePrerequisite.Length - 1 : 0 + this.grid.NumberOfElementsPerLine));

        this.EnableTheUsedResourcePrerequisite();

        ResourceHelper.SetResourcePrerequisiteUIGameObject(this.resourcePrerequisGameObject, this.resourcePrerequisite, null, positiveColor);
    }
    #endregion
}