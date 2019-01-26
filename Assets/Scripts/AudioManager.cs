using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hekira
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonObject<AudioManager>
    {
        AudioSource audioSource;
        void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayOneShot(AudioClip audioClip) {
            audioSource.PlayOneShot(audioClip);
        }
    }
}