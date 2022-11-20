using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaManager : MonoBehaviour
{
    Image timerBar;
    public float maxTime = 100f;
    public float timeLeft;
    public BiondeManager biondeManager;

    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
            //Inscrease();
            //Decrease();
            Debug.Log("Time left " + timeLeft);
        } else
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void Inscrease()
    {
        if (biondeManager.isHit)
        {
            timeLeft += 20f;
            Debug.Log("Prize");
            if(timeLeft > maxTime)
            {
                timeLeft = maxTime;
            }
        }
    }
    void Decrease()
    {
        if (biondeManager.isHealed)
        {
            timeLeft -= 5f;
        }
    }
}
