using System;
using System.Collections;
using UnityEngine;

namespace Projects.Presentation.UI.Component
{
    /// <summary>
    /// モーダルウィンドウを表示するためのビュークラスの抽象ベースクラス。
    /// </summary>
    public abstract class ModalView : MonoBehaviour
    {
        /// <summary>
        /// 表示、非表示を提供するメソッド。
        /// 表示、非表示の際にアニメーションや演出を仕込みたいときに実装する。
        /// </summary>
        public abstract IEnumerator SetActive(bool value);

        /// <summary>
        /// モーダルの表示処理が完了したときに呼び出したい外部処理を登録するためのイベント。
        /// </summary>
        public Action OnModalShown;

        /// <summary>
        /// モーダルの非表示処理が完了したときに呼び出したい外部処理を登録するためのイベント。
        /// </summary>
        public Action OnModalHidden;
    }
}
