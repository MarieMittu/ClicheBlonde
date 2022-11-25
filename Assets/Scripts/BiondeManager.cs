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
    public bool isBlond { get; set; }
    public bool isSmart { get; set; }
    private bool isProfInstantiated;
    private NavMeshAgent navAgent;
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
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!isBlond)
        {
            FleeFromPlayer();
        }
        if(!isProfInstantiated && SpawnProf.Instance.isProfSpawned) 
        {
            professor = GameObject.FindGameObjectWithTag("Prof");
	    isProfInstantiated = true;
	    Debug.Log("Prof has been instantiated.");
        }
        if(isBlond && SpawnProf.Instance.isProfSpawned && isProfInstantiated)
        {
            FleeFromProf();
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
        Debug.Log("collision isBlond Value : " + isBlond);
	Debug.Log("collision isBlond Instance Value : " + BiondeManager.Instance.isBlond);
        if (collision.gameObject.tag == "Bullet" && !BiondeManager.Instance.isBlond)
//        if (collision.gameObject.tag == "Bullet")
        {
//            staminaManager = Cam.GetComponent<StaminaManager();
//	    staminaManager = GameObject.GetComponent<StaminaManager>();
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
//            isHit = true;
            StaminaManager.Instance.Increase();
            Debug.Log("Increase");
            becomeBlond();
            Destroy(collision.gameObject);
            Instantiate(pinkShot, transform.position, Quaternion.identity);
            particleSystem.Play();
        }
        else if ( (collision.gameObject.tag == "Prof" || collision.gameObject.tag == "Book") && _instance.isBlond) //change for "Book"
        {
            isHealed = true;
            StaminaManager.Instance.Decrease();
            Debug.Log("Increase");
            becomeSmart();
        }
    }

    public void becomeBlond()
    {
        
            //become blond
            Debug.Log("I'm BLOND!");
            genBionde.biondeCount -= 1;
            isBlond = true;
	    Debug.Log("Blond value : " + isBlond);
            isSmart = false;
            _instance = this;
    }

    public void becomeSmart()
    {
        
            //become smart
            Debug.Log("I'm SMART!");
            
            isSmart = true;
            isBlond = false;
            _instance = this;
    }
}
