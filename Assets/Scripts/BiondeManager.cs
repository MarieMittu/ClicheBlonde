using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BiondeManager : MonoBehaviour
{


    public GameObject player;
    public GenerateBionde genBionde;
    public bool isHit = false;
    public bool isHealed;
    public bool isBlond;
    public bool isSmart;
    private NavMeshAgent navAgent;
    public float EnemyDistanceRun = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
       

        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log("Distance: " + distance);

        if (distance < EnemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            navAgent.SetDestination(newPos);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
            //isHit = true;
            becomeBlond();
        }
        else if (collision.gameObject.tag == "Book")
        {
            //isHealed = true;
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
