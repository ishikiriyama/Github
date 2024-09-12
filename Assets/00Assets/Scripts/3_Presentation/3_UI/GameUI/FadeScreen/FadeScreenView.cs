using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.Presentation.UI.GameUI.FadeScreen
{
    /// <summary>
    /// フェードインとフェードアウトの処理を行うクラス。
    /// </summary>
    public class FadeScreenView : MonoBehaviour
    {
        [SerializeField, Tooltip("フェード処理を行うImageコンポーネント")]
        private Image fadeImage;

        private void Awake()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// フェードイン処理を開始します。
        /// </summary>
        /// <param name="duration">フェードインにかかる時間（秒）</param>
        public void StartFadeIn(float duration)
        {
            InitiateFade(duration, 1f, 0f, () => gameObject.SetActive(false));
        }

        /// <summary>
        /// フェードアウト処理を開始します。
        /// </summary>
        /// <param name="duration"></param>

        public void StartFadeOut(float duration)
        {
            InitiateFade(duration, 0f, 1f, () => gameObject.SetActive(false));
        }

        /// <summary>
        /// フェードイン処理を開始します。
        /// </summary>
        /// <param name="duration">フェードインにかかる時間（秒）</param>
        /// <param name="callback">フェードイン完了後に呼び出されるコールバック</param>
        public void StartFadeIn(float duration, System.Action callback)
        {
            InitiateFade(duration, 1f, 0f, () =>
            {
                gameObject.SetActive(false);
                callback?.Invoke();
            });
        }

        /// <summary>
        /// フェードアウト処理を開始します。
        /// </summary>
        /// <param name="duration">フェードアウトにかかる時間（秒）</param>
        /// <param name="callback">フェードアウト完了後に呼び出されるコールバック</param>
        public void StartFadeOut(float duration, System.Action callback)
        {
            InitiateFade(duration, 0f, 1f, () =>
            {
                gameObject.SetActive(false);
                callback?.Invoke();
            });
        }

        /// <summary>
        /// フェードインを遅延後に開始します。
        /// </summary>
        /// <param name="duration">フェードインにかかる時間（秒）</param>
        /// <param name="delaySeconds">フェードを開始するまでの遅延時間（秒）</param>
        public void StartFadeInWithDelay(float duration, float delaySeconds)
        {
            gameObject.SetActive(true);
            SetFadeImageAlpha(1f);
            StartCoroutine(DelayedFadeIn(duration, delaySeconds));
        }

        /// <summary>
        /// フェードアウトを遅延後に開始します。
        /// </summary>
        /// <param name="duration">フェードアウトにかかる時間（秒）</param>
        /// <param name="delaySeconds">フェードを開始するまでの遅延時間（秒）</param>
        public void StartFadeOutWithDelay(float duration, float delaySeconds)
        {
            gameObject.SetActive(true);
            SetFadeImageAlpha(0f);
            StartCoroutine(DelayedFadeOut(duration, delaySeconds));
        }

        private void SetFadeImageAlpha(float alpha)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        }

        private void InitiateFade(float duration, float startAlpha, float endAlpha, System.Action callback)
        {
            gameObject.SetActive(true);
            SetFadeImageAlpha(startAlpha);
            StartCoroutine(Fade(duration, endAlpha, callback));
        }

        private IEnumerator DelayedFadeIn(float duration, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            StartFadeIn(duration);
        }

        private IEnumerator DelayedFadeOut(float duration, float delaySeconds)
        {
            yield return new WaitForSeconds(delaySeconds);
            StartFadeOut(duration);
        }

        private IEnumerator Fade(float duration, float targetAlpha, System.Action callback = null)
        {
            float elapsedTime = 0f;
            float initialAlpha = fadeImage.color.a;

            while (elapsedTime < duration)
            {
                // アルファ値を徐々に変化させることでフェード処理を実現
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(initialAlpha, targetAlpha, elapsedTime / duration);
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                yield return null;
            }
            // フェード処理が完了したときにアルファ値を明示的に設定
            SetFadeImageAlpha(targetAlpha);
            callback?.Invoke(); // フェード処理が完了したらコールバックを呼び出す

            if (targetAlpha == 0f)  // フェードイン完了時
            {
                gameObject.SetActive(false);
            }
        }
    }
}