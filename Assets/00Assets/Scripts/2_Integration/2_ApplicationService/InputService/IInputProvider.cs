using UnityEngine.InputSystem;

namespace Projects.Integration.ApplicationService.InputService
{
    /// <summary>
    /// ユーザーからのの入力を提供するインターフェース
    /// タッチ開始 Action<Vector2>
    /// ドラッグ中 Action<Vector2>
    /// タッチ終了 Action<Vector2>
    /// を提供する
    /// </summary>
    public interface IInputProvider
    {
        public InputAction OnDragAction { get; }

        /// <summary>
        /// 入力を有効にします。
        /// </summary>
        void Enable();

        /// <summary>
        /// 入力を無効にします。
        /// </summary>
        void Disable();

    }
}