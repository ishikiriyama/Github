using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    [RequireComponent(typeof(CanvasGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class AnimationModalView : ModalView
    {
        [SerializeField]
        [Tooltip("出現アニメーションの時間")]
        private float appearDuration = 0.5f;
        [SerializeField]
        [Tooltip("出現スタートのY座標")]
        private float appearPosition = 0;
        [SerializeField]
        [Tooltip("出現アニメーションのイージング")]
        private Ease appearEase = Ease.OutBack;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        public override IEnumerator SetActive(bool value)
        {
            if (value)
            {
                yield return Appear();
            }
            else
            {
                yield return Disappear();
            }
        }

        private IEnumerator Appear()
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0;
            rectTransform.anchoredPosition = new Vector2(0, appearPosition);
            Sequence appearSequence = DOTween.Sequence();
            appearSequence.Append(canvasGroup.DOFade(1, appearDuration).SetEase(appearEase));
            appearSequence.Join(rectTransform.DOAnchorPosY(0, appearDuration).SetEase(appearEase));
            appearSequence.SetLink(gameObject);
            yield return appearSequence.WaitForCompletion();
            OnModalShown?.Invoke();
        }

        private IEnumerator Disappear()
        {
            Sequence disappearSequence = DOTween.Sequence();
            disappearSequence.Append(canvasGroup.DOFade(0, appearDuration).SetEase(appearEase));
            disappearSequence.Join(rectTransform.DOAnchorPosY(appearPosition, appearDuration).SetEase(appearEase));
            disappearSequence.SetLink(gameObject);
            yield return disappearSequence.WaitForCompletion();
            gameObject.SetActive(false);
            OnModalHidden?.Invoke();
        }



    }
}
