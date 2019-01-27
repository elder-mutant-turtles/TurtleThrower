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

        private static SoundManager instance;
        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SoundManager();
                }

                return instance;
            }
        }

        private AudioSource bgmSource;

        private int iterator = 0;

        public Transform PlaySound(string soundName)
        {
            if (sources == null)
            {
                return null;
            }
            
            if (iterator >= sources.Count)
            {
                iterator = 0;
            }
            
            var soundSource = sources[iterator];

            AudioClip clip;

            if (!audioDic.TryGetValue(soundName, out clip))
            {
                return null;
            }

            soundSource.clip = clip;
            
            soundSource.Play();

            iterator++;

            return soundSource.transform;
        }

        public void PlayBGM(string bgmName)
        {
            if (audioDic == null)
            {
                return;
            }
            
            AudioClip clip;

            if (!audioDic.TryGetValue(bgmName, out clip))
            {
                return;
            }

            bgmSource.clip = clip;
            bgmSource.Play();
        }
        
        private void Awake()
        {
            instance = this;
            
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
                DontDestroyOnLoad(sourceInstance.gameObject);
                var source = sourceInstance.GetComponent<AudioSource>();
                source.volume = 0.8f;
                sources.Add(source);
                
            }

            var bgmSourceInstance = Instantiate(SourcePrefab);
            DontDestroyOnLoad(bgmSourceInstance);

            bgmSource = bgmSourceInstance.GetComponent<AudioSource>();

            bgmSource.loop = true;
        }
    }
}