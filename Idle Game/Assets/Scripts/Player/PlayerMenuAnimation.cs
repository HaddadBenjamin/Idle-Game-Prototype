using UnityEngine;
using System.Collections;

public class PlayerMenuAnimation : MonoBehaviour
{
    [SerializeField]
    private AnimationClip[] animations;
    private EPlayerMenuAnimation currentMenuAnimation = EPlayerMenuAnimation.Default;

    public EPlayerMenuAnimation CurrentMenuAnimation
    {
        get { return currentMenuAnimation; }
        set { currentMenuAnimation = value; }
    }

    public AnimationClip GetAnimation(string animationName)
    {
        foreach (AnimationClip animation in animations)
        {
            if (animationName == animation.name)
                return animation;
        }

        return null;
    }
}
