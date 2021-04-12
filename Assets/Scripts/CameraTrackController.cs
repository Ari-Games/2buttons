using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraTrackController : MonoBehaviour
{
    PlayableDirector director;
    [SerializeField]
    PlayableDirector CutSceneDirector;
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }
    public void Play(GameObject startButton)
    {
        director.Play();
        StartCoroutine(UIButtonsController.DisableFromSeconds(startButton, 0.2f));
    }
    public void Stop()
    {
        //если один раз запускали то пропустить
        if (PlayerPrefs.HasKey("Scene") && PlayerPrefs.GetInt("Scene") == 1)
        {
            return;
        }
        director.Pause();
        CutSceneDirector.Play();
        PlayerPrefs.SetInt("Scene", 1);

    }
    void Update()
    {
        
    }
}
