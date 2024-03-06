using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class querryBones : MonoBehaviour
{

    String pullScore;
    public TMP_Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pullScore = GameObject.Find("GameController").GetComponent<gameController>().bones.ToString();

       
        
        //Debug.Log("Score is" + scoreText);
        gameObject.GetComponent<TMPro.TMP_Text>().text = pullScore; 


    }
}
