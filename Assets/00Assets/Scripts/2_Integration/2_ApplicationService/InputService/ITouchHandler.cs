using UnityEngine;

namespace Projects.Integration.ApplicationService.InputService
{
    public interface ITouchHandler
    {
        /// <summary>
        /// タッチ開始時の処理
        /// </summary>
        /// <param name="position"></param>
        void OnTouchStart(Vector2 position);

        /// <summary>
        /// タッチ中の処理
        /// </summary>
        /// <param name="position"></param>
        void OnTouchMove(Vector2 position);

        /// <summary>
        /// タッチ終了時の処理
        /// </summary>
        /// <param name="position"></param>
        void OnTouchEnd(Vector2 position);
    }
}