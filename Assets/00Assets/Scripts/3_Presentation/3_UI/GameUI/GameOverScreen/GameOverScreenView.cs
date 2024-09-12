using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.Presentation.UI.GameUI.GameOverScreen
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverScreenView : MonoBehaviour
    {
        [SerializeField, Required] private CanvasGroup canvasGroup;
        [SerializeField] private float appearDuration = 0.3f;
        [SerializeField] private float appearDelay = 0f;

        [SerializeField, Required] Button retryButton;

        public Button RetryButton => retryButton;

        private void Reset()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            StartCoroutine(Appear());
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private IEnumerator Appear()
        {
            yield return new WaitForSeconds(appearDelay);
            float elapsedTime = 0;
            while (elapsedTime < appearDuration)
            {
                elapsedTime += Time.deltaTime;
                canvasGroup.alpha = elapsedTime / appearDuration;
                yield return null;
            }
        }
    }
}
