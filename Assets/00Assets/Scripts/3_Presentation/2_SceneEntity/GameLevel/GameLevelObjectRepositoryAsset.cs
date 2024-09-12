using Sirenix.OdinInspector;
using UnityEngine;

namespace Projects.Presentation.SceneEntity.GameLevel
{
    [CreateAssetMenu(fileName = "GameLevelRepository", menuName = "SceneEntity/GameLevel/GameLevelRepository")]
    public class GameLevelRepositoryAsset : ScriptableObject
    {
        [SerializeField, Required]
        private GameLevelObject[] gameLevelObjects;

        public GameLevelObject[] GameLevelObjects => gameLevelObjects;
    }
}
