using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveProf : MonoBehaviour
{
    public SpawnProf spawnProf;
    //public SpawnProf spawnProf;
    void Start()
    {
        StartCoroutine(SelfDestruct());
        
    }

    IEnumerator SelfDestruct()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(17f, 25f));
            Destroy(gameObject);
            StartCoroutine(spawnProf.ProfDrop(1));
        }

    }
}
