using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProfManager : MonoBehaviour
{
    public GameObject bionda;
    public BiondeManager biondeManager;
    //public Rigidbody rigidbody;
    public bool isFollowing;
   

    // Start is called before the first frame update
    void Start()
    {
        bionda = GameObject.FindGameObjectWithTag("Bionda");
        //rigidbody = GetComponent<Rigidbody>();
        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {

      
            if (biondeManager.isBlond && isFollowing)
            {
                //GetComponent<NavMeshAgent>().destination = bionda.transform.position;
                FindClosestBionda();
            }
        
      
    }

    void FindClosestBionda()
    {
        
            float distToClosestBionda = Mathf.Infinity;
            BiondeManager closestBionda = null;
            BiondeManager[] allBionde = GameObject.FindObjectsOfType<BiondeManager>();

            foreach (BiondeManager currentBionda in allBionde)
            {
                float distToBionda = (currentBionda.transform.position - this.transform.position).sqrMagnitude;
                if (distToBionda < distToClosestBionda)
                {
                    distToClosestBionda = distToBionda;
                    closestBionda = currentBionda;
                    GetComponent<NavMeshAgent>().destination = closestBionda.transform.position;
                }
            }
              
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            isFollowing = false;
            Invoke("FollowsAgain", 5f);

        }
    
    }

    void FollowsAgain()
    {
        isFollowing = true;
    }
}
