using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class click : MonoBehaviour
{
    public Sprite newButtonImage;
    public Button button;
    String find;
    RenderDice rd;
    gameController GameController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeButtonImage()
    {
        button.image.sprite = newButtonImage;
    }

    public void ClickAction_Roll()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();
        int bones = GameController.bones;
        if (bones > 0)
        {
            // Debug.Log(find);
           
            GameController.DiceNumberCheck();

            int numberDice = GameController.numberDice;
            GameController.makeDie();

            for (int i = 0; i < numberDice; i++)
            {
                //Debug.Log(find);
                rd = GameObject.Find("MakeDie (" + i + ")").GetComponent<RenderDice>();
                rd.Startroll();
            }
            GameController.Reset();
            GameController.updateCurrentDice();
            GameController.determineValid();
            GameController.RollPenalty();
        }
    }

    public void ClickAction_Keep()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();
        GameController.Score();
        GameController.saveBones();
        //GameController.RollPenalty();
    }

    public void ClickAction_Exit()
    {
        Application.Quit();
    }

    public void ClickAction_Reset()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();
        GameController.DiceNumberCheck();
        int numberDice = GameController.numberDice;
       // for (int i = 1; i < numberDice; i++)
        //{
         //   GameObject fn = GameObject.Find("MakeDie (" + (i) + ")");
          //  if (fn != null) //Check to see if the thing exists before you destroy it
           // {
            //    DestroyObject(fn);
            //}
        //}
        //GameController.numberDice = 1;
        //GameController.DiceNumberCheck();
        GameController.makeDie();
        GameController.Reset();
        GameController.bones = 100;
        GameController.rollCount = 10;
    }

    public void ClickAction_X()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();
        GameController.Score();
        GameController.saveBones();

    }
    
}
