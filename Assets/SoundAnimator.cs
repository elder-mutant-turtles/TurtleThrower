using UnityEngine;

namespace DefaultNamespace
{
    public class SoundAnimator : MonoBehaviour
    {
        public void PlaySound(string soundName)
        {

            if (SoundManager.Instance == null)
            {
                return;
            }
            
            var trans = SoundManager.Instance.PlaySound(soundName);
            if (trans != null)
            {
                trans.position = transform.position;   
            }
        }
    }
}