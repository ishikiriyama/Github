using System;
using System.Collections;
using UnityEngine;

namespace Projects.Presentation.SceneEntity.GameLevel
{
    public class GameLevelGenerator : MonoBehaviour
    {
        private Func<GameLevelObject> prefabFactory;
        private GameLevelObject currentGameLevelObject;
        private Action onGameLevelObjectGenerated;

        public void Construct(Func<GameLevelObject> prefabFactory, Action onGameLevelObjectGenerated)
        {
            this.prefabFactory = prefabFactory;
            this.onGameLevelObjectGenerated = onGameLevelObjectGenerated;
        }

        public GameLevelObject Generate()
        {
            StartCoroutine(GenerateAndInvoke());
            return currentGameLevelObject;
        }

        private IEnumerator GenerateAndInvoke()
        {
            if (currentGameLevelObject != null) // 既に生成されている場合は削除して1フレーム待つ
            {
                Destroy(currentGameLevelObject.gameObject);
                yield return new WaitForEndOfFrame();
            }

            currentGameLevelObject = Instantiate(prefabFactory());
            yield return new WaitForEndOfFrame(); // 1フレーム後にイベントを発火
            onGameLevelObjectGenerated?.Invoke();
        }

        private void OnDestroy()
        {
            if (currentGameLevelObject != null)
            {
                Destroy(currentGameLevelObject.gameObject);
            }

            onGameLevelObjectGenerated = null;
        }
    }
}