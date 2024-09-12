using Projects.Integration.ApplicationService.InputService;
using System;
using UnityEngine.InputSystem;

/// <summary>
/// ユーザー入力を処理するクラス。ゲーム内のプレイヤーの動作を制御します。
/// </summary>
namespace Projects.Presentation.Input
{
    public class UserInput : IInputProvider, IDisposable
    {
        private readonly ProjectInputActions inputActions;

        /// <summary>
        /// リソースを解放します。
        /// </summary>
        public void Dispose()
        {
            inputActions.Dispose();
        }

        /// <summary>
        /// UserInput インスタンスを初期化します。
        /// </summary>
        public UserInput()
        {
            inputActions = new ProjectInputActions();
        }

        public InputAction OnDragAction => inputActions.Player.OnDrag;

        /// <summary>
        /// 入力を有効にします。
        /// </summary>
        public void Enable()
        {
            inputActions.Enable();
        }

        /// <summary>
        /// 入力を無効にします。
        /// </summary>
        public void Disable()
        {
            inputActions.Disable();
        }
    }
}