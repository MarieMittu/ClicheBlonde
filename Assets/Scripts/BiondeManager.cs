using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BiondeManager : MonoBehaviour
{
    public GameObject player;
    public GameObject professor;
    public GenerateBionde genBionde;
    public bool isHit { get; set; }
    public bool isHealed { get; set; }
    private bool isProfInstantiated;
    private NavMeshAgent navAgent;
    private BiondeManager[] allBionde;
    public float PlayerDistanceRun = 4.0f;
    public float ProfDistanceRun = 4.0f;
    public Animator biondaAnimator;
    public GameObject pinkShot;
    public ParticleSystem particleSystem;

    private static BiondeManager _instance;    

    private BiondeManager() { }

    public static BiondeManager Instance
    {
        get
        {
            if(_instance == null)
                _instance = new BiondeManager();

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
        isProfInstantiated = false;
        player = GameObject.FindGameObjectWithTag("Player");
        navAgent = GetComponent<NavMeshAgent>();
	allBionde = GameObject.FindObjectsOfType<BiondeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isProfInstantiated && SpawnProf.Instance.isProfSpawned) 
        {
            professor = GameObject.FindGameObjectWithTag("Prof");
	    isProfInstantiated = true;
	    Debug.Log("Prof has been instantiated.");
        }
	foreach (BiondeManager currentBionda in allBionde) 
	{
        
            if(currentBionda.tag == "Smart")
            {
                FleeFromPlayer();
            }
            if(currentBionda.tag != "Smart" && SpawnProf.Instance.isProfSpawned && isProfInstantiated)
            {
                FleeFromProf();
            }
	}
       
    }

    void FleeFromPlayer()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < PlayerDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - player.transform.position;
            Vector3 newPos = transform.position + dirToPlayer;
            navAgent.SetDestination(newPos);
            biondaAnimator.SetBool("isRunning", true);
            biondaAnimator.SetBool("isBlond", false);
        } else
        {
            biondaAnimator.SetBool("isRunning", false);
        }
    }

    void FleeFromProf()
    {
        float distance = Vector3.Distance(transform.position, professor.transform.position);
        //Debug.Log("Distance: " + distance);

        if (distance < ProfDistanceRun)
        {
            Vector3 dirToProf = transform.position - professor.transform.position;
            Vector3 newPos = transform.position + dirToProf;
            navAgent.SetDestination(newPos);
            biondaAnimator.SetBool("isRunning", true);
            biondaAnimator.SetBool("isBlond", true);
        }
        else
        {
            biondaAnimator.SetBool("isRunning", false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject currentBlonde = collision.gameObject;
        Debug.Log("Current Game Object is : " + currentBlonde);
	Debug.Log("Current Collider is : " + collision.collider);
        if (collision.gameObject.tag == "Bullet" && this.gameObject.tag != "Blond")
//        if (collision.gameObject.tag == "Bullet")
        {
//            staminaManager = Cam.GetComponent<StaminaManager();
//	    staminaManager = GameObject.GetComponent<StaminaManager>();
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
//            isHit = true;
            StaminaManager.Instance.Increase();
            Debug.Log("Increase");	    
            this.becomeBlond();
            Destroy(collision.gameObject);
            Instantiate(pinkShot, transform.position, Quaternion.identity);
            particleSystem.Play();
        }
        else if ( (collision.gameObject.tag == "Prof" || collision.gameObject.tag == "Book") && this.gameObject.tag != "Smart") //change for "Book"
        {
	    Debug.Log("prof collided with : " + this.gameObject);
            isHealed = true;
            StaminaManager.Instance.Decrease();
            Debug.Log("Increase");
            this.becomeSmart();
        }
    }

    public void becomeBlond()
    {
        
            //become blond
            Debug.Log("I'm BLOND!");
//	    this.isBlond = true;
	    this.gameObject.tag = "Blond";
            _instance = this;
    }

    public void becomeSmart()
    {
        
            //become smart
            Debug.Log("I'm SMART!");		
	    this.gameObject.tag = "Smart";
            _instance = this;
    }

    public bool checkObjectBlond()
    {
        foreach (BiondeManager currentBionda in allBionde) 
	{
           if (currentBionda.tag != "Smart")
           {
              return true;
           }
        }
	return false;
    }
}
