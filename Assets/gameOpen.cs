using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class gameOpen : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickAction()
    {
       // GameObject.Find("GameController").GetComponent<gameController>().updateBones();
        GameObject.Find("GameController").GetComponent<gameController>().Reset();
        SceneManager.LoadScene(0);
    }
}
