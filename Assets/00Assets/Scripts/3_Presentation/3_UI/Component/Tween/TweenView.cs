using DG.Tweening;
using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// Tweenアニメーションを実行するためのビュークラスの抽象ベースクラス。
    /// 具体的なTweenアニメーションの実行処理はこのクラスを継承した子クラスで実装する。
    /// </summary>
    public abstract class TweenView : MonoBehaviour
    {
        /// <summary>
        /// 外部でTweenを連結したり、再生を制御するためのTweenの参照を取得するメソッド。
        /// </summary>
        /// <returns></returns>
        public abstract Tween GetTween();

        /// <summary>
        /// Tweenアニメーションを再生するメソッド。
        /// </summary>
        /// <returns></returns>
        public abstract Tween Play();

        public abstract void ResetTween();

    }
}
