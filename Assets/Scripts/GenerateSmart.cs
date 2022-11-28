using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSmart : MonoBehaviour
{
    public GameObject smart;
    public int xPos;
    public int zPos;
    public int smartCount;

    private static GenerateSmart _instance;

    private GenerateSmart() { }

    public static GenerateSmart Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GenerateSmart();

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
        StartCoroutine(SmartDrop());
    }

    IEnumerator SmartDrop()
    {
        while (smartCount < 10)
        {
            xPos = Random.Range(-7, 16);
            zPos = Random.Range(5, -24);
            Instantiate(smart, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
            smartCount += 1;
        }
    }

    public void DecreaseSmart()
    {
        smartCount--;
    }
}
