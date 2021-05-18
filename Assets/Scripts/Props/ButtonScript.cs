using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject platform;
    private bool isPlatformActive = false;
    public GameObject leaf;
    public AudioSource audio;
    private Renderer leafRenderer;
    private Color originalLeafColor;

    void Start()
    {
        leafRenderer = leaf.GetComponent<Renderer>();
        originalLeafColor = leafRenderer.material.color;
        platform.SetActive(false);
    }

    private void OnTriggerEnter(Collider obj) {
        if(obj.gameObject.tag == "Player" && !isPlatformActive) {
            isPlatformActive = !isPlatformActive;
            platform.SetActive(isPlatformActive);

            if(isPlatformActive) {
                leafRenderer.material.color = new Color32(0, 29, 0, 0);
            } else {
                leafRenderer.material.color = originalLeafColor;
            }
            audio.Play();
            
        }
     }

}
