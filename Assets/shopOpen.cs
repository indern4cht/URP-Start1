using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class shopOpen : MonoBehaviour
{
    public static shopController Instance;
    gameController GameController;

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
        SceneManager.LoadScene(1);
        String find = "GameController";
        // Debug.Log(find);
        GameController = GameObject.Find(find).GetComponent<gameController>();
        GameController.Score();
       // GameController.saveBones();
    }
}
