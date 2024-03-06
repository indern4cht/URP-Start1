using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotButton : MonoBehaviour
{
    public GameObject thisbutton;
    gameController GameController;
    int diceRollCount;
    // Start is called before the first frame update
    void Start()
    {
        thisbutton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();
            diceRollCount = GameController.rollCount;
        if (diceRollCount > 10)
        {
            thisbutton.SetActive(true);
        }
        else if (diceRollCount <10)
        {
            thisbutton.SetActive(false);
        }
    }
}
