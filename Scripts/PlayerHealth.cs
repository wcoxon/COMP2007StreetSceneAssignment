using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerHealth : MonoBehaviour
{
    public Image blackScreen;
    public Text warning;
    public Animation screenimation;

    public float maxHealth = 100;
    public float health = 100;

    public Text healthUI;

    // Start is called before the first frame update
    void Start()
    {
        screenimation.Play();
        //warning.enabled = true;
        StartCoroutine(fadeTextAfterDuration(warning, 3f));

    }

    public IEnumerator fadeTextAfterDuration(Text text,float delay)
    {
        text.enabled = true;
        Color col = text.color;
        col.a = 1;
        text.color = col;
        yield return new WaitForSeconds(delay);
        //text.fade
        //for(float a = 1f; a>0; a -= Time.fixedDeltaTime)
        //{
        //    text.CrossFadeAlpha(1,1)
        //    yield return new WaitForFixedUpdate();
        //}
        text.CrossFadeAlpha(0, 1f, false);
        yield return new WaitForSeconds(1);
        text.enabled = false;
        
        yield break;
    }

    public static IEnumerator pulseText(Text text,Color initialColour)
    {
        
        for (float t = 0; t < 1; t += Time.fixedDeltaTime)
        {
            text.color = Color.Lerp(new Color(1, 1, 1), initialColour, t);
            yield return new WaitForFixedUpdate();
        }
        text.color = initialColour;
        yield break;
    }

    void gameOver()
    {

        SceneManager.LoadScene(0);
        //screenimation.Play();
    }

    // Update is called once per frame
    void Update()
    {
        healthUI.text = Mathf.RoundToInt(health) + "/" + Mathf.RoundToInt(maxHealth);
        if(health <= 0)
        {
            gameOver();
        }

    }
}
