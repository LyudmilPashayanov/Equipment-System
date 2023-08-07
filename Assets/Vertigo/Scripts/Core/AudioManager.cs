using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Variables
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance; } }

    [SerializeField] AudioSource _oneShotSource;
    [SerializeField] AudioSource _radioSource;

    private Queue<AudioSource> _audioSourcePool = new Queue<AudioSource>();
    #endregion

    #region Functionality

    void Awake()
    {
        _instance = this;
    }

    private AudioSource GetAudioSourceFromPool()
    {
        // Check if there's an available audio source in the pool
        if (_audioSourcePool.Count > 0)
        {
            return _audioSourcePool.Dequeue();
        }

        // If no available audio source found, create a new one and add it to the pool
        GameObject newAudioSourceObject = new GameObject();
        AudioSource newAudioSource = newAudioSourceObject.AddComponent<AudioSource>();
        
        return newAudioSource;
    }

    private void ReturnAudioSourceToPool(AudioSource audioSource)
    {
        audioSource.Stop();
        audioSource.transform.position = Vector3.zero; // Reset position
        _audioSourcePool.Enqueue(audioSource);
    }

    public void PlayOneShot(AudioClip clipToPlay)
    {
        _oneShotSource.PlayOneShot(clipToPlay);
    }

    public void PlayAtPoint(AudioClip clipToPlay, Vector3 positionToPlay)
    {
        AudioSource audioSource = GetAudioSourceFromPool();
        audioSource.transform.position = positionToPlay;
        audioSource.clip = clipToPlay;
        audioSource.Play();
        StartCoroutine(ReturnAudioSourceToPoolAfterPlaying(audioSource));
    }

    private IEnumerator ReturnAudioSourceToPoolAfterPlaying(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        ReturnAudioSourceToPool(audioSource);
    }

    #endregion
}