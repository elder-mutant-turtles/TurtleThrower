using UnityEngine;

namespace DefaultNamespace
{
    public class SoundAnimator : MonoBehaviour
    {
        public void PlaySound(string soundName)
        {
            var trans = SoundManager.Instance.PlaySound(soundName);
            if (trans != null)
            {
                trans.position = transform.position;   
            }
        }
    }
}