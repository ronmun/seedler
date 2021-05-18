using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentCoins =0;

    private void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(PlayerMovement.instance) {
        //     PauseUnpause();
        // }
    }

    // public void PauseUnpause()
    // {
    //     if(UIManager.instance.pauseScreen.activeInHierarchy)
    //     {
    //         UIManager.instance.pauseScreen.SetActive(false);
    //         Time.timeScale = 1f;

    //         Cursor.visible = false;
    //         Cursor.lockState = CursorLockMode.Locked;
    //     } else
    //     {
    //         UIManager.instance.pauseScreen.SetActive(true);
    //         // UIManager.instance.CloseOptions();
    //         Time.timeScale = 0f;

    //         Cursor.visible = true;
    //         Cursor.lockState = CursorLockMode.None;
    //     }
    // }

    public void AddGems(int gemsToAdd) {
        currentCoins += gemsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }
}
