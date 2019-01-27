using UnityEngine;

namespace DefaultNamespace
{
    public class SoundAnimator : MonoBehaviour
    {
        public void PlaySound(string soundName)
        {
            SoundManager.Instance.PlaySound(soundName).position = transform.position;
        }
    }
}