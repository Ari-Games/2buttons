using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
[RequireComponent(typeof(PlayableDirector))]
public class CutSceneManager : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector CameraDirector;
    private PlayableDirector director;
    private bool isPaused = false;
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void Pause()
    {
        director.Pause();
        isPaused = true;
    }
    public void Play()
    {
        director.Play();
        isPaused = false;
    }

    public void CameraStartPosition()
    {
        CameraDirector.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPaused)
        {
            Play();
        }
    }
}
