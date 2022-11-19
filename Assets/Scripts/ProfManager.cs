using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProfManager : MonoBehaviour
{
    public GameObject bionda;
    public BiondeManager biondeManager;

    // Start is called before the first frame update
    void Start()
    {
        bionda = GameObject.FindGameObjectWithTag("Bionda");
    }

    // Update is called once per frame
    void Update()
    {
        FindClosestBionda();

        if (biondeManager.isBlond)
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
}
