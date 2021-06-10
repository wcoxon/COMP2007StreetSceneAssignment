using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class winScript : MonoBehaviour
{
    public Text timer;
    public float endTime;
    // Start is called before the first frame update
    void Start()
    {
        endTime = Time.time;

        timer.text = "you took " + Mathf.RoundToInt((endTime - PlayerMovement.startTime)*100f)/100f + " seconds";
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time >= endTime + 5)
        {
            SceneManager.LoadScene(0);
        }
    }
}
