using DG.Tweening;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    public float transitionDuration = 0.5f;

    public void AnimatePanelIn(RectTransform panel)
    {
        panel.localScale = Vector3.zero;
        panel.gameObject.SetActive(true);
        panel.DOScale(Vector3.one, transitionDuration).SetEase(Ease.Linear);
    }

    public void AnimatePanelOut(RectTransform panel)
    {
        panel.DOScale(Vector3.zero, transitionDuration).SetEase(Ease.Linear).OnComplete(() => panel.gameObject.SetActive(false));
    }
}
