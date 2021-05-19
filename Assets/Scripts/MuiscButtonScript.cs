using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MuiscButtonScript : MonoBehaviour
{
    public Button button;
    public Text buttonText;

    private MusicController musicController;

    private void Awake()
    {
        musicController = FindObjectOfType<MusicController>();
        buttonText = button.GetComponentInChildren<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (musicController.play)
        {
            buttonText.text = "Music: ON";
        }
        else
        {
            buttonText.text = "Music: OFF";
        }
    }

    public void ToggleMusic()
    {
        if(musicController.play)
        {
            musicController.PauseMusic(false);
            buttonText.text = "Music: OFF";
        } 
        else
        {
            musicController.PauseMusic(true);
            buttonText.text = "Music: ON";
        }
            
    }
}
