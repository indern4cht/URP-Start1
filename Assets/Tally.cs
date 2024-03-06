using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Tally : MonoBehaviour
{
    public int[] testVals;
    public bool LoadTests;
    public int[] currentDice;
    public bool[] selectedDice;
    public bool[] keptDice;
    public bool[] validDiceSelect;
    public int[] sortDice;
    public int[] counts;
    public int tally=0;
    public int tally_r=0;
    public int score;
    public int s1 = 0;
    bool valid;
    RenderDice rd;

    //public int test = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        //GameObject d1 = GameObject.Find("MakeDie (1)");
        //RenderDice cs1 = d1.GetComponent<RenderDice>();
        //currentDice[1] = cs1.DiceVal;
        currentDice[0] = GameObject.Find("MakeDie (1)").GetComponent<RenderDice>().DiceVal;
        currentDice[1] = GameObject.Find("MakeDie (2)").GetComponent<RenderDice>().DiceVal;
        currentDice[2] = GameObject.Find("MakeDie (3)").GetComponent<RenderDice>().DiceVal;
        currentDice[3] = GameObject.Find("MakeDie (4)").GetComponent<RenderDice>().DiceVal;
        currentDice[4] = GameObject.Find("MakeDie (5)").GetComponent<RenderDice>().DiceVal;
        currentDice[5] = GameObject.Find("MakeDie (6)").GetComponent<RenderDice>().DiceVal;

        selectedDice[0] = GameObject.Find("MakeDie (1)").GetComponent<RenderDice>().selected;
        selectedDice[1] = GameObject.Find("MakeDie (2)").GetComponent<RenderDice>().selected;
        selectedDice[2] = GameObject.Find("MakeDie (3)").GetComponent<RenderDice>().selected;
        selectedDice[3] = GameObject.Find("MakeDie (4)").GetComponent<RenderDice>().selected;
        selectedDice[4] = GameObject.Find("MakeDie (5)").GetComponent<RenderDice>().selected;
        selectedDice[5] = GameObject.Find("MakeDie (6)").GetComponent<RenderDice>().selected;

        if (LoadTests)
        {
            currentDice[0] = testVals[0];
            currentDice[1] = testVals[1];
            currentDice[2] = testVals[2];
            currentDice[3] = testVals[3];
            currentDice[4] = testVals[4];
            currentDice[5] = testVals[5];
        }
    }

    public void Score()
    {

        //Table of scores, consider making public to modify in editer?
        int singFive = 50;
        int singOne = 100;
        int triOne = 300;
        int triTwo = 200;
        int triThree = 300;
        int triFour = 400;
        int triFive = 500;
        int triSix = 600;

        int fourOak = 1000;
        int fiveOak = 2000;
        int sixOak = 3000;
        int straight = 1500;
        int triPair = 1500;
        int duoTriplets = 2500;
        //int fourOakPair = 1500; handled as fourOak + 500 in pair detection

        bool firstTri = false;
        bool fourFound = false;
        int holdup = 0;
        int duoCount = 0;
        int singleCount = 0;
        int selectCount = 0;

        //Buffer current dice value into new array "sortDice". //Removed sort function so we can track dice position selection accross rounds. 
        for (int i = 0; i < currentDice.Length; i++)
        {
            validDiceSelect[i] = keptDice[i] ^ selectedDice[i]; 
            if (validDiceSelect[i])
            {
                sortDice[i] = currentDice[i];
            }
            else
            {
                sortDice[i] = 0;
            }
        }
       // Array.Sort(sortDice);

        //Count the total numnber of dice at each value 1-6 and store it in "counts"
        for (int j = 0; j < 7; j++)
        {
            for (int i = 0; i < sortDice.Length; i++)
            {
                if (sortDice[i] == j)
                {
                    counts[j]++;
                }
            }
        }

        //Run loops to look for 3,4,5,6 of one number scores. //Managed to get all logic instead, but only for single round. There is no error handling for no score though.
        for (int j = 6; j > 0; j--)
        {
            for (int i = 0; i < 7; i++)
            {
                if (counts[i] == j)
                {
                    if (j == 6 && i != 0){tally_r += sixOak;}
                    else if (j == 5 && i != 0) { tally_r += fiveOak; }
                    else if (j == 4 && i != 0)
                    {
                        tally_r += fourOak;
                        fourFound = true;
                    }
                    else if (j == 3 && i != 0)
                    {
                        if (i == 1) { tally_r += triOne; holdup += triOne; }
                        else if (i == 2) { tally_r += triTwo; holdup += triTwo; }
                        else if (i == 3) { tally_r += triThree; holdup += triThree; }
                        else if (i == 4) { tally_r += triFour; holdup += triFour; }
                        else if (i == 5) { tally_r += triFive; holdup += triFive; }
                        else if (i == 6) { tally_r += triSix; holdup += triSix; }

                        if (firstTri)
                        {
                            tally_r += duoTriplets;
                            tally_r -= holdup;
                            tally_r = 0;
                        }
                        firstTri = true;

                    }
                    else if (j == 2 && i != 0)
                    {
                        if (duoCount == 2) tally_r += triPair;
                        else if (fourFound) { tally_r += 500; } //Difference between four of a kind and fOak +pair.
                        else if (i == 5) { tally_r += (2 * singFive); }
                        else if (i == 1) { tally_r += (2 * singOne); }
                        duoCount++;
                    }
                    else if (j == 1 && i != 0)
                    {
                        if (singleCount == 4) 
                        { 
                            tally_r += straight; 
                            tally_r -= holdup;
                            tally_r = 0;
                        }
                        else if(i == 5) { tally_r += singFive;  holdup += singFive; }
                        else if (i == 1) { tally_r += singOne; holdup += singOne; }
                        singleCount++;
                    }

                }

            }
        }


        if (tally_r != tally) { valid = true; }
        //valid = false;
        //Need to reset counts(incrementing all zeros as is), a call re-roll on keep condition. Probably need error catching first.
        if (valid)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!selectedDice[i])
                {
                    String find = "MakeDie (" + (i + 1) + ")";
                    rd = GameObject.Find(find).GetComponent<RenderDice>();
                    rd.RollDie();
                }
                else { selectCount++; }
                
                keptDice[i] = selectedDice[i];
                counts[i] = 0;
            }
            tally += tally_r;
            tally_r = 0;
        if(selectCount == 6)
            {
                for (int i = 0; i < 6; i++)
                {
                    String find = "MakeDie (" + (i + 1) + ")";
                    rd = GameObject.Find(find).GetComponent<RenderDice>();
                    rd.RollDie();
                }
                score += tally;
                tally = 0;
            }
        }
    }

    public void Score2()
    {
        switch (s1)
        {
            case 1: break;
            case 2: break;
            case 3: break;
            case 4: break;
            case 5: break;
        }
    }
}
