using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
public class EnemyScript : MonoBehaviour
{

    public Transform goal;
    // Start is called before the first frame update
    NavMeshAgent agent;
    bool canSeePlayer;
    public float DPS;
    bool playerSpotted = false;

    public Transform screenShake;
    public float shakeStrength;
    bool playerLost = false;

    public PostProcessProfile ppp;
    //public PostProcessVolume ppVol;
    //IEnumerator fader;
    public PlayerHealth playerHealth;

    void Start()
    {

        agent = GetComponent<NavMeshAgent>();
        agent.destination = transform.position;
        ppp.GetSetting<Vignette>().active = false;
        //agent.destination = goal.position;
        //fader = fadeOutSource(GetComponent<AudioSource>(), 1f);
        //GetComponent<NavMeshAgent>().SetDestination(transform.position + new Vector3(10, 0, 0));
        //GetComponent<NavMeshAgent>().CalculatePath(transform.position + new Vector3(10, 0, 0), GetComponent<NavMeshAgent>().path);
    }

    // Update is called once per frame
    

    IEnumerator fadeOutSource(AudioSource source,float duration)
    {
        for(float v = source.volume; v > 0; v -= Time.fixedDeltaTime / duration)
        {
            if (canSeePlayer)
            {
                yield break;
            }
            source.volume = v;
            yield return new WaitForFixedUpdate();
        }
        source.volume = 0f;

        yield break;
    }

    void FixedUpdate()
    {


        //canSeePlayer = !Physics.Raycast(transform.position, goal.position - transform.position,maxDistance:Vector3.Distance(transform.position,goal.position));


        if (!Physics.Raycast(transform.position+(1.5f*Vector3.up), goal.position - transform.position, maxDistance: Vector3.Distance(transform.position, goal.position)))
        {
            playerSpotted = !canSeePlayer;
            playerLost = false;
        }
        else
        {
            playerLost = canSeePlayer;
            playerSpotted = false;
        }

        canSeePlayer = !Physics.Raycast(transform.position + (1.5f * Vector3.up), goal.position - transform.position, maxDistance: Vector3.Distance(transform.position, goal.position));

        if (playerSpotted)
        {
            //StopCoroutine(playerHealth.pulseText(playerHealth.healthUI));
            //StartCoroutine(playerHealth.pulseText(playerHealth.healthUI,new Color(1,0.2f,0.2f)));
            GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().volume = 1f;
            
            GetComponent<AudioSource>().Play();
            //GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
            ppp.GetSetting<Vignette>().active = true;
        }

        if (playerLost)
        {
            screenShake.localPosition = Vector3.zero;
            //fader = fadeOutSource(GetComponent<AudioSource>(), 1f);
            StartCoroutine(fadeOutSource(GetComponent<AudioSource>(), 1f));
            ppp.GetSetting<Vignette>().active = false;
        }

        if (canSeePlayer)
        {
            agent.destination = goal.position;
            playerHealth.health -= DPS * Time.fixedDeltaTime;
            screenShake.localPosition = Random.insideUnitSphere * shakeStrength;
            //StartCoroutine(playerHealth.pulseText(playerHealth.healthUI, new Color(1, 0.2f, 0.2f)));
            StartCoroutine(PlayerHealth.pulseText(playerHealth.healthUI, new Color(1, 0.2f, 0.2f)));
            //StartCoroutine(playerHealth.pulseText(playerHealth.healthUI));

        }
        else if (Vector3.Distance(transform.position, agent.destination)<3f)
        {
            NavMeshHit hit;
            NavMesh.SamplePosition(transform.position+Random.insideUnitSphere*30f, out hit, 30f, NavMesh.AllAreas);
            //Debug.Log(hit.position);
            agent.destination = hit.position;

        }
        
        //GetComponent<NavMeshAgent>().SetDestination(transform.position + new Vector3(10, 0, 0));
        //GetComponent<NavMeshAgent>().mesh
        //GetComponent<NavMeshAgent>().Move(GetComponent<NavMeshAgent>(). ;
        //GetComponent<NavMeshAgent>().updatePosition;
    }
}
