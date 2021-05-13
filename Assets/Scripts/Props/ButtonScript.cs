using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public GameObject platform;
    private bool isPlatformActive = false;
    public GameObject leaf;
    private Renderer leafRenderer;
    private Color originalLeafColor;
    void Start()
    {
        leafRenderer = leaf.GetComponent<Renderer>();
        originalLeafColor = leafRenderer.material.GetColor("_Color");
        platform.SetActive(false);
    }

    private void OnTriggerEnter(Collider obj) {
        if(obj.gameObject.tag == "Player") {
            isPlatformActive = !isPlatformActive;
            platform.SetActive(isPlatformActive);

            if(isPlatformActive) {
                leafRenderer.material.SetColor("_Color", Color.red);
            } else {
                leafRenderer.material.SetColor("_Color", originalLeafColor);
            }
            
        }
     }

}
