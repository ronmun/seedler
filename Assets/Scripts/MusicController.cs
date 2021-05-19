using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioSource music;
    public AudioClip menu;
    public AudioClip level;
    public AudioClip credits;
    public bool play = true;

    void Awake()
    {
        int numOfSessions = FindObjectsOfType<MusicController>().Length;
        if (numOfSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        SceneManager.sceneLoaded += MusicChanger;
    }

    void Start()
    {
        music = gameObject.GetComponent<AudioSource>();
    }

    public void MusicChanger(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Level_1")
        {
            music.clip = level;
            if(play)
                music.Play();
        }
            
        if (scene.name == "MainMenu")
        {
            music.clip = menu;
            if(play)
                music.Play();
        }
    }

    public void ChangeSong(string song)
    {
        if(song == "credits")
            music.clip = credits;
        if (song == "menu")
            music.clip = level;
        if (song == "level")
            music.clip = menu;

        if (play)
            music.Play();
    }

    public void PauseMusic(bool playing)
    {
        play = playing;

        if (playing)
            music.Play();
        else
            music.Pause();
    }
}
