using DG.Tweening; // DOTweenを使用するために必要なusingディレクティブ
using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// ゲームクリア時の星のTweenアニメーションを実行するためのビュークラス。
    /// 3個の星がそれぞれ異なるタイミングで出現するアニメーションを再生する。
    /// また再生する星の数は、外部から指定可能
    /// </summary>
    public class WinStarTweenView : TweenView
    {
        [SerializeField] private Transform[] stars; // 星のTransformを格納する配列
        [SerializeField] private float delayBetweenStars = 0.1f; // 星が現れる間のディレイ時間
        [SerializeField] private float scaleDuration = 0.2f; // スケールアニメーションの持続時間
        [SerializeField] private float scaleMultiplier = 1.5f; // 最大スケールの倍率
        [SerializeField] private Ease scaleEase = Ease.OutSine; // スケールアニメーションのイージング

        Sequence sequence;

        private void Awake()
        {
            // 数に関係なくすべての星を非表示にしておく
            foreach (var star in stars)
            {
                star.gameObject.SetActive(false);
            }
        }

        public Tween GetTween(int numberOfStars)
        {
            return CreateTween(numberOfStars);
        }

        public override Tween GetTween()
        {
            return GetTween(stars.Length);
        }

        /// <summary>
        /// 外部から再生する星の数を設定するためのメソッド。
        /// </summary>
        /// <param name="numberOfStars">再生する星の数。</param>
        public Tween Play(int numberOfStars)
        {
            sequence = CreateTween(numberOfStars);

            sequence.Play();
            return sequence;
        }

        // 元の抽象メソッドもオーバーライドし、デフォルトの挙動を定義
        public override Tween Play()
        {
            return Play(stars.Length); // デフォルトでは全ての星を再生
        }


        private Sequence CreateTween(int numberOfStars)
        {
            // numberが配列の最大値もしくは0未満の場合は、配列の最大値もしくは0にする
            // あとコンソールに警告（日本語）を表示
            if (numberOfStars > stars.Length)
            {
                Debug.LogWarning("指定された星の数が配列の要素数を超えています。配列の要素数に合わせて再生します。");
                numberOfStars = stars.Length;
            }
            else if (numberOfStars < 0)
            {
                Debug.LogWarning("指定された星の数が0未満です。0に設定して再生します。");
                numberOfStars = 0;
            }



            Sequence sequence = DOTween.Sequence();

            // 指定された数だけ星を順番に表示するアニメーションをシーケンスに追加
            for (int i = 0; i < numberOfStars && i < stars.Length; i++)
            {
                var star = stars[i];

                // 星が現れるアニメーションをシーケンスに追加
                sequence.AppendCallback(() => star.gameObject.SetActive(true))
                        .Append(star.DOScale(Vector3.one * scaleMultiplier, scaleDuration).SetEase(scaleEase)) // 最大にスケールアップ
                        .Append(star.DOScale(Vector3.one, scaleDuration).SetEase(scaleEase)) // 元のサイズに戻す
                        .AppendInterval(delayBetweenStars); // 次の星が現れるまでのディレイ
            }
            sequence.Pause(); // シーケンスを一時停止状態にしておく
            sequence.SetLink(gameObject); // シーケンスをこのゲームオブジェクトにリンクして、シーン遷移時に自動的に破棄されるようにする
            return sequence;
        }

        public override void ResetTween()
        {
            // 現在再生中のsequenceがnullでない場合は、シーケンスを停止してリセットして開放
            if (sequence != null)
            {
                sequence.Kill();
                sequence = null;
            }

            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].localScale = Vector3.one; // スケールを元のサイズに戻す
                stars[i].gameObject.SetActive(false); // 星を非表示にする
            }
        }

    }
}
