using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// 数値データを視覚的に表示するための抽象ベースクラス（ジェネリック版）。
    /// ゲーム内で動的に変更される数値（ステータスの値、所持コインなど）を表示するために使用される。
    /// 外部からの数値の更新はSetValueメソッドを通じて行い、実際の表示更新処理はこのクラスを継承した子クラスで実装する。
    /// </summary>
    /// <typeparam name="T">このビューで表示する数値の型。</typeparam>
    public abstract class ValueView<T> : MonoBehaviour where T : struct
    {

        /// <summary>
        /// 外部から数値をセットするメソッド。数値がセットされると、自動的に表示が更新される。
        /// </summary>
        /// <param name="newValue">表示したい新しい数値。</param>
        public abstract void SetValue(T newValue);
    }
}
