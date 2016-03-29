using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInformationManager : AServiceComponent
{
    #region Fields
    private Queue<Text> textInformations;
    private List<RectTransform> textInformationsRectTranform;
    private Transform parentTransform;

    private readonly float UpdateTime = 0.2f;
    #endregion

    #region Abstract Initializer
    public override void InitializeByserviceContainer()
    {
        this.parentTransform = ServiceContainer.Instance.GameObjectReferenceManager.Get("Screen Center").transform;

        this.textInformations = new Queue<Text>();
        this.textInformationsRectTranform = new List<RectTransform>();

        StartCoroutine(this.UpdatePoolElementsEveryNSeconds());
    }
    #endregion

    #region Unity Methods
    void Update()
    {
        this.UpdatePositionPoolElements();
    }
    #endregion

    #region Behaviour Methods
    public void AddTextInformation(string content, ETextInformation type = ETextInformation.Information)
    {
        GameObject textInformationGameObject = ServiceContainer.Instance.GameObjectReferencesArrays.Instantiate("Text Information", EGameObjectReferences.UI);
        Text text = textInformationGameObject.GetComponent<Text>();

        text.text = content;
        text.color =
            ETextInformation.Information == type ?  ColorHelper.Blue :
            ETextInformation.RareEvent == type ?    ColorHelper.Purple :
            ETextInformation.Warning == type ?      ColorHelper.Yellow :
                                                    ColorHelper.Red;

        textInformationGameObject.transform.SetPositionAndParent(Vector3.zero, this.parentTransform);

        // Enqueue : Add
        // Dequeue : Remove
        // Peek : Get First Element
        this.textInformations.Enqueue(text);
        this.textInformationsRectTranform.Add(textInformationGameObject.GetComponent<RectTransform>());
    }

    /// <summary>
    /// Permet de lancer une fonction tous les n temps de façon propre.
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdatePoolElementsEveryNSeconds()
    {
        while (true)
        {
            this.UpdatePoolElements();

            yield return new WaitForSeconds(this.UpdateTime);
        }
    }

    private void UpdatePoolElements()
    {
        this.UpdateDeletePoolElements();
    }

    private void UpdatePositionPoolElements()
    {
        for (int rectTransformIndex = 0; rectTransformIndex < this.textInformationsRectTranform.Count; rectTransformIndex++)
            this.textInformationsRectTranform[rectTransformIndex].SetPosition
                (Vector3.Lerp(this.textInformationsRectTranform[rectTransformIndex].position, new Vector3(0.0f, rectTransformIndex * 50.0f, 0.0f), 1f));
    }

    private void UpdateDeletePoolElements()
    {
        if (this.textInformations.Count > 0)
        {
            Text text = this.textInformations.Peek();

            if (text.color.a <= 0.0f)
            {
                this.textInformations.Dequeue();
                this.textInformationsRectTranform.Remove(text.gameObject.GetComponent<RectTransform>());

                Destroy(text.gameObject);
            }
       }
    }
    #endregion
}