using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PickupSystem : MonoBehaviour
{
    public float collectedCount=0f;
    public float pickupsCount;
    public Text collectableUI;
    public AudioSource UISource;
    public AudioClip pickupSound;

    // Start is called before the first frame update
    void Start()
    {
        pickupsCount = GameObject.FindGameObjectsWithTag("Pickup").Length;

        collectableUI.text = collectedCount + "/" + pickupsCount;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            Destroy(other.gameObject);
            collectedCount += 1;
            collectableUI.text = collectedCount + "/" + pickupsCount;
            StartCoroutine(PlayerHealth.pulseText(collectableUI, new Color(0.14f, 0.78f, 0.85f)));
            UISource.PlayOneShot(pickupSound);

            if(collectedCount == pickupsCount)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
