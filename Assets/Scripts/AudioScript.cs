using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour {
    public static AudioScript main;
    private AudioSource audioSource;
    
    void Awake(){
        DontDestroyOnLoad(gameObject);
        main = this;
        audioSource = GetComponent<AudioSource>();
        GameObject[] audioObjs = GameObject.FindGameObjectsWithTag ("Audio");
        if(audioObjs.Length > 1){
            for(int i = 0; i < audioObjs.Length - 1; i++){
                Destroy (audioObjs[i]);
            }

        }
        StartCoroutine(WaitFor(audioSource.clip.length + 2));
    }

    private IEnumerator WaitFor(float t){
        yield return new WaitForSeconds(t);
        audioSource.Play();
        StartCoroutine(WaitFor(t));
    }

    public void ToggleAudio(){
        audioSource.volume = (audioSource.volume == 0)? 0.2f : 0;
    }

}
