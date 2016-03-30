using UnityEngine;
using UnityEngine.UI;

public class LevelUpBuildingButtonTooltip : AResourcePrerequisiteTooltip
{
    #region Fields
    public override void SetContent(ResourcePrerequisite[] content)
    {
        this.resourcePrerequisite = content;
        this.rectTransform.SetHeight(140.0f + this.grid.GetHeight(this.resourcePrerequisite.Length - 1 + this.grid.NumberOfElementsPerLine));
        
        this.EnableTheUsedResourcePrerequisite();

        ResourceHelper.SetResourcePrerequisiteUIGameObject(this.resourcePrerequisGameObject, this.resourcePrerequisite, this.playerResources);
    }
    #endregion
}