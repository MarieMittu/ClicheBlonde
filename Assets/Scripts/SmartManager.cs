using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SmartManager : MonoBehaviour
{
    public GameObject player;
    public GameObject bionda;
    public GenerateSmart genSmart;
    public bool isHit { get; set; }
   
  
    private NavMeshAgent navAgent;
    private GameObject[] allSmart;
    public float PlayerDistanceRun = 4.0f;
   
    public Animator smartAnimator;
    public GameObject pinkShot;
    public ParticleSystem particleSystem;

    private static SmartManager _instance;

    private SmartManager() { }

    public static SmartManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new SmartManager();

            return _instance;
        }
    }


    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
        allSmart = GameObject.FindGameObjectsWithTag("Smart");
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
      
        foreach (GameObject currentSmart in allSmart)
        {

            if (currentSmart.tag == "Smart")
            {
                FleeFromPlayer(currentSmart);
            }
         
        }

        if(GenerateSmart.Instance.smartCount == 0)
        {
            FindObjectOfType<GameManager>().WonGame();
        }

    }

    void FleeFromPlayer(GameObject currentSmart)
    {
        smartAnimator = currentSmart.GetComponent<Animator>();
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < PlayerDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            navAgent.SetDestination(newPos);

           
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        GameObject currentSmart = collision.gameObject;
        Debug.Log("Current Game Object is : " + currentSmart);
        Debug.Log("Current Collider is : " + collision.collider);
        if (collision.gameObject.tag == "Bullet" && this.gameObject.tag == "Smart")
        //        if (collision.gameObject.tag == "Bullet")
        {
            //            staminaManager = Cam.GetComponent<StaminaManager();
            //	    staminaManager = GameObject.GetComponent<StaminaManager>();
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
            //            isHit = true;
            StaminaManager.Instance.Increase();
            ScoreManager.Instance.AddScore();
            GenerateSmart.Instance.DecreaseSmart();
            Debug.Log("Increase");
       
            Destroy(collision.gameObject);
            Instantiate(pinkShot, transform.position, Quaternion.identity);
            particleSystem.Play();
            Destroy(this.gameObject);
            GameObject newBionda = Instantiate(bionda) as GameObject;
            newBionda.transform.position = this.gameObject.transform.position;
            BiondeManager biondeManager = newBionda.GetComponent<BiondeManager>();
        }
    }


    public bool checkObjectSmart()
    {
        foreach (GameObject currentSmart in allSmart)
        {
            if (currentSmart.tag == "Smart")
            {
                return true;
            }
        }
        return false;
    }

    public GameObject[] getListOfSmartGameObjects()
    {
        if (allSmart != null)
        {
            return allSmart;
        }
        else
        {
            GameObject[] smartGameObjects;
            return smartGameObjects = GameObject.FindGameObjectsWithTag("Smart");
        }
    }
}
