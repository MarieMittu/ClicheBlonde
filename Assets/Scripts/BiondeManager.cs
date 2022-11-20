using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BiondeManager : MonoBehaviour
{

    public StaminaManager staminaManager;
    public GameObject player;
    public GameObject professor;
    public GenerateBionde genBionde;
    public bool isHit = false;
    public bool isHealed;
    public bool isBlond;
    public bool isSmart;
    private NavMeshAgent navAgent;
    public float PlayerDistanceRun = 4.0f;
    public float ProfDistanceRun = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        professor = GameObject.FindGameObjectWithTag("Prof");
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(!isBlond)
        {
            FleeFromPlayer();
        }
        if(isBlond)
        {
            FleeFromProf();
        }
       
    }

    void FleeFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distance: " + distance);

        if (distance < PlayerDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            navAgent.SetDestination(newPos);
        }
    }

    void FleeFromProf()
    {
        float distance = Vector3.Distance(transform.position, professor.transform.position);
        Debug.Log("Distance: " + distance);

        if (distance < ProfDistanceRun)
        {
            Vector3 dirToProf = transform.position - professor.transform.position;
            Vector3 newPos = transform.position + dirToProf;
            navAgent.SetDestination(newPos);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && !isBlond)
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
            isHit = true;
            staminaManager.timeLeft += Time.deltaTime * 20;
            Debug.Log("Increase");
            becomeBlond();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Prof" && isBlond) //change for "Book"
        {
            isHealed = true;
            staminaManager.timeLeft -= Time.deltaTime * 5;
            becomeSmart();
        }
    }

    public void becomeBlond()
    {
        
            //become blond
            Debug.Log("I'm BLOND!");
            genBionde.biondeCount += 1;
            isBlond = true;
            isSmart = false;
        
    }

    public void becomeSmart()
    {
        
            //become smart
            Debug.Log("I'm SMART!");
            isSmart = true;
            isBlond = false;
        
    }
}
