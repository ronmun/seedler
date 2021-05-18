using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiamondEndingScript : MonoBehaviour
{
    public float bonusTime;
    public GameObject corruptlevel;
    public GameObject level;
    public GameObject corruptParticles;
    public GameObject particles;
    public GameObject powerUps;
    public GameObject gems;
    public GameObject enemies;
    public MeshRenderer mesh;
    public BoxCollider collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(StartBonusScene());
            corruptlevel.SetActive(false);
            level.SetActive(true);
            corruptParticles.SetActive(false);
            particles.SetActive(true);
            powerUps.SetActive(true);
            gems.SetActive(true);
            enemies.SetActive(false);
            mesh.enabled = false;
            collider.enabled = false;
        }
    }

    IEnumerator StartBonusScene()
    {
        yield return new WaitForSeconds(bonusTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
