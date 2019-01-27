using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoundManager : MonoBehaviour
    {
        public GameObject SourcePrefab;
        
        public List<AudioClip> Audios;

        private Dictionary<string, AudioClip> audioDic;

        private List<AudioSource> sources;

        public static SoundManager Instance;

        private int iterator = 0;

        public void PlaySound(string soundName)
        {
            var soundSource = sources[iterator];
            
            soundSource.clip = audioDic[soundName];
            
            soundSource.Play();

            iterator++;
        }
        
        private void Awake()
        {
            Instance = this;
            
            DontDestroyOnLoad(gameObject);
            
            audioDic = new Dictionary<string, AudioClip>();
            
            sources = new List<AudioSource>();
            
            foreach (var audio in Audios)
            {
                audioDic.Add(audio.name, audio);
            }

            for (int i = 0; i < 10; i++)
            {
                var sourceInstance = Instantiate(SourcePrefab);
                DontDestroyOnLoad(SourcePrefab);
                sources.Add(sourceInstance.GetComponent<AudioSource>());
            }
        }
    }
}