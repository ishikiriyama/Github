using UnityEngine;

namespace Projects.Presentation.SceneEntity.Effect
{
    public class ParticleCallbackRemover : MonoBehaviour
    {
        private void OnParticleSystemStopped()
        {
            Destroy(gameObject);
        }
    }
}
