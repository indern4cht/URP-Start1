using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public int[] testVals;
    public bool LoadTests;
    public int[] currentDice;
    public bool[] selectedDice;
    public bool[] keptDice;
    public bool[] validDiceSelect;
    public bool[] enabledDice;
    public bool[] toolTips;
    public bool tt_Clear;
    public int tt_count;
    //public int[] sortDice;
    public int[] counts;
    public int numberDice;
    public int diceShown;
    public int selectCount = 0;
    public int validCount = 0;
    public int tally = 0;
    public int tally_r = 0;
    public int score;
    public int roundCount = 0;
    public float roundMult=1;
    public float diceMult = 0.7f;
    public int s1 = 0;
    public bool fallthrough;
    public bool terminalCase = false;
    public bool shopswitch =false;
    public int bones;
    public int rollPenalty = 10;
    public int rollCount = 0;
    //public TMP_Text  scoretext;
    bool valid;
    Dictionary<int, bool> dic_score = new Dictionary<int, bool>();
    //int awake;
    public static gameController GameController;
    public GameObject prefabDice;
    public Transform prefabDiceParent;
    public GameObject shop;
    RenderDice rd;
    public scoreBook[] scoreBookArray = new scoreBook[12];
    


    void Start()    // Start is called before the first frame update
    {
        tt_count = GameObject.Find("ToolTips").GetComponent<ttController>().SetActive.Count;
        //Instance = this;
        DiceNumberCheck();
        loadScorebook();
        Array.Resize<bool>(ref toolTips, tt_count);
        //toolTips.Length = GameObject.Find("ToolTips").GetComponent<ttController>().Children.Count;
    }

    void Update()   // Update is called once per frame
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            updateCurrentDice();
            determineValid();
            Count();

            if (rollCount > 10)
            {
                // shop = this.shop;
               
                //shop.SetActive(true);
               // shopswitch = true;
               // shop = GameObject.Find("Shop Select Btn");
            }
            else if (rollCount < 10 && shopswitch)
            {
               // shop = GameObject.Find("Shop Select Btn");
               // shop.SetActive(false);
               // shopswitch = false;
            }

            if (LoadTests)
            {
                numberDice = 6;
                DiceNumberCheck();
                makeDie();
                for (int i = 0; i < numberDice; i++)
                {
                    currentDice[i] = testVals[i];
                    String find = "MakeDie (" + (i) + ")";
                    rd = GameObject.Find(find).GetComponent<RenderDice>();
                    rd.SetDie(testVals[i]);
                }
               
                
            }

            
            if (tt_Clear)
            {
                for (int i = 0; i < tt_count; i++)
                {
                    //toolTips[i] = GameObject.Find("ToolTips").GetComponent<ttController>().SetActive[i];
                    GameObject.Find("ToolTips").GetComponent<ttController>().Children[i].SetActive(false);
                }
            }
        }
        diceShown = GameObject.FindGameObjectsWithTag("Dice").Length;

       

    }

    private void Awake()    //Awake is called on load
    {
        if (GameController == null)
        {
            GameController = this;
        }
        else if (GameController != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Count() //If a dice is selected and determined valid, add it to count array to be scored
    {
        
        
            for (int j = 0; j < 7; j++)
            {
            counts[j] = 0;
                for (int i = 0; i < currentDice.Length; i++)
                    {
                    
                    // validDiceSelect[i] = (keptDice[i] ^ selectedDice[i]);
                    if (currentDice[i] == j)
                    {
                        if (validDiceSelect[i])
                        {
                            counts[j]++;
                        }
                    }
                }
            }

        
    }

    public void Score() //Preconditional check to determine if we go to Tally and calculate score
    {
        Debug.Log("Initialize Score");
        updateCurrentDice();

        if (validCount > 0)
        {
            Tally();
        }
    }   
    
    public void Tally() //Main score application function
    {
        Debug.Log("Begin Tally");
       
        int jswitch = 1;
        int diefound = 0;
       // loadScorebook();

        int singcount = 0;
        int paircount = 0;
        int tricount = 0;

        //Get Index values for scoring lookup. Object array created in loadScoreBook()
        int singTwo_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "singTwo");
        int singOne_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "singOne");
        int fourOak_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "fourOak");
        int fourOakPair_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "fourOakPair");
        int fiveOak_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "fiveOak");
        int sixOak_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "sixOak");
        int straight_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "straight");
        int triPair_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "triPair");
        int duoTriplets_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "duoTriplets");
        int trips_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "trips");
        int pairs_index = Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "pair");


        while (jswitch != 0)
        {
            switch (jswitch)
            {
                case 1: //Six of a Kind
                    {
                        //Debug.Log(jswitch);
                        bool sixfound = CountCheck(6, 1, 7, out diefound);
                        if (sixfound && scoreBookArray[sixOak_index].enable)
                        {
                            //Debug.Log("We found six?");
                            //tally_r += sixOak;
                            tally_r += scoreBookArray[sixOak_index].value;
                            jswitch = 0;

                        }
                        else
                        {
                            //Debug.Log("Moving to 2");
                            jswitch = 2;
                        }
                       // Debug.Log("The cake is a lie");
                        break;
                    }
                case 2: //Five of a Kind
                    {
                        //Debug.Log(jswitch);
                        bool fivefound = CountCheck(5, 1, 7, out diefound);
                        if (fivefound && scoreBookArray[fiveOak_index].enable)
                        {
                            //tally_r += fiveOak; // Five of a kind
                            tally_r += scoreBookArray[fiveOak_index].value;
                            //terminalCase = true;
                            jswitch = 14;
                        }
                        else
                        {
                            jswitch = 3;
                        }
                        break;
                    }
                case 3://Four of a Kind, or Four of a Kind plus a pair
                    {
                       // Debug.Log(jswitch);
                        bool fourfound = CountCheck(4, 1, 7, out diefound);
                        if (fourfound && scoreBookArray[fourOak_index].enable)
                        {
                            bool pairfound = CountCheck(2, 1, 7, out diefound);
                            if (pairfound && scoreBookArray[fourOakPair_index].enable)
                            {
                                //tally_r += fourOakPair;
                                tally_r += scoreBookArray[fourOakPair_index].value;
                                jswitch = 0;
                            }
                            else
                            {
                                //tally_r += fourOak;
                                tally_r += scoreBookArray[fourOak_index].value;
                                terminalCase = true;
                                jswitch = 14;
                            }
                        }
                        else
                        {
                            jswitch = 4;
                        }
                        break;
                    }
                case 4: //Straight
                    {
                       // Debug.Log(jswitch);
                        for (int i = 1; i < 7; i++)
                        {
                            bool sixsing = CountCheck(1, i, i + 1, out diefound);
                            if (sixsing) { singcount++; }
                        }

                        if (singcount == 6 && scoreBookArray[straight_index].enable)
                        {
                            //tally_r += straight;
                            tally_r += scoreBookArray[straight_index].value;
                            jswitch = 0;
                        }
                        else
                        {
                            jswitch = 5;
                        }
                        break;
                    }
                case 5: //Three Pairs
                    {
                       // Debug.Log(jswitch);
                        for (int i = 1; i < 6; i++)
                        {
                            bool threepairs = CountCheck(2, i, i + 1, out diefound);
                            if (threepairs) { paircount++; }
                        }
                        if (paircount == 3 && scoreBookArray[triPair_index])
                        {
                            //tally_r += triPair;
                            tally_r += scoreBookArray[triPair_index].value;
                            jswitch = 0;

                        }
                        else
                        {
                            jswitch = 6;
                        }
                        break;
                    }
                case 6: //Two Triplets
                    {
                        //Debug.Log(jswitch);
                        for (int i = 1; i < 6; i++)
                        {
                            bool twotriplets = CountCheck(3, i, i + 1, out diefound);
                            if (twotriplets) { tricount++; }
                        }
                        if (tricount == 2 && scoreBookArray[duoTriplets_index])
                        {
                            //tally_r += duoTriplets;
                            tally_r += scoreBookArray[duoTriplets_index].value;
                            jswitch = 0;

                        }
                        else
                        {
                            jswitch = 7;
                        }
                        break;
                    }
                case 7: //Three of a Kind
                    {
                        //Debug.Log(jswitch);
                        bool trips = CountCheck(3, 1, 7, out diefound);
                        if (trips && scoreBookArray[trips_index].enable)
                        {
                            //tally_r += diefound * 100;
                            tally_r += scoreBookArray[trips_index].value*diefound*3;
                            terminalCase = true;
                        }
                        jswitch = 13;
                        break;
                    }
                case 13: //Pairs
                    {
                        //Debug.Log(jswitch);
                        bool pair = CountCheck(2, 1, 7, out diefound);
                        //if (diefound == 5)
                        //{
                        //    tally_r += 100;
                        //}
                        //else if (diefound == 1 && scoreBookArray[singOne_index].enable)
                        //{
                        //    //tally_r += 200;
                        //    tally_r += scoreBookArray[singOne_index].value*2;
                        //}
                        if (diefound != 0 && scoreBookArray[pairs_index])
                        {
                            tally_r += scoreBookArray[pairs_index].value * diefound*2;
                            terminalCase = true;
                        }
                        jswitch = 14;
                        break;
                    }
                case 14: //Single 1 or 5
                    {
                        //Debug.Log(jswitch);
                        bool singleOne = CountCheck(1, 1, 2, out diefound);
                        if (singleOne && scoreBookArray[singOne_index].enable)
                        {
                            //tally_r += singOne;
                            tally_r += scoreBookArray[singOne_index].value ;
                        }
                        bool singleTwo = CountCheck(1, 2, 3, out diefound);
                        if (singleTwo && scoreBookArray[singTwo_index].enable)
                        {
                            //tally_r += singFive;
                            tally_r += scoreBookArray[Array.FindIndex(scoreBookArray, scoreBook => scoreBook.name == "singTwo")].value;
                        }
                    }
                    jswitch = 0;
                    break;
            }
        }

        for (int i = 0; i < counts.Length; i++)
        {
            counts[i] = 0;
        }


            //Debug.Log("New tally found");
            //valid = true; 
            if (tally_r > 0)
            {
            //RollPenalty();    
            Round();
                Debug.Log("Going to Round() Normal");
            }
            else if (fallthrough)
            {
                Round();
                Debug.Log("Test Fallthrough selected, going to Round()");
            }
        
        //valid = false;
        //Need to reset counts(incrementing all zeros as is), a call re-roll on keep condition. Probably need error catching first.
    }

    public void Round() //Postconditional checks after score is determined
    {
        determineValid();
        for (int i = 0; i < numberDice; i++)
        {
            keptDice[i] = selectedDice[i];
            if (!selectedDice[i]) //If the Dice is not selected, then re-roll it
            {
                String find = "MakeDie (" + i  + ")";
                rd = GameObject.Find(find).GetComponent<RenderDice>();
                rd.Startroll();
            }

        }
        
        for(int i =0; i <counts.Length; i++)
        {
            counts[i] = 0;
        }
        tally += tally_r;
        tally_r = 0;

        if ((selectCount == numberDice) || terminalCase) //If all dice are selected, re-roll all and reset tally, mark score
        {
            for (int i = 0; i < numberDice; i++)
            {
                rd = GameObject.Find("MakeDie (" + i + ")").GetComponent<RenderDice>();
                rd.Startroll();
                updateCurrentDice();
              //  validDiceSelect[i] = true;
                keptDice[i] = false;
            }
            roundCount++;
            selectCount = 0;
            score += Convert.ToInt32(tally*roundMult*diceMult);
            roundMult += 1;
            tally = 0;
        }
        terminalCase = false;
    }

    public bool CountCheck(int check, int start, int max, out int diefound) //Function called by Score for various cases looking at the counts array. 
    {
        bool checkfound = false;
        diefound = 0;
        for (int i = start; i < max; i++)
        {
            if(counts[i] == check)
            {
                diefound = i;
                checkfound = true;
                break;
            }
            else
            {

                checkfound = false;
            }
        }
        
        return checkfound;
    }
    
   public void DiceNumberCheck() //Resize the arrays that are associated with the number of dice in game
    {

        Array.Resize<int>(ref testVals, numberDice);
        Array.Resize<int>(ref currentDice, numberDice);
        Array.Resize<bool>(ref selectedDice, numberDice);
        Array.Resize<bool>(ref keptDice, numberDice);
        Array.Resize<bool>(ref validDiceSelect, numberDice);   
    }

    public void saveBones() //Save score to number of "bones" to load into shop
    {
        Debug.Log("Adding score to bones");
        bones += score;
        score = 0;
    }

    public void updateBones() //Pull bones from shop to store in the main game controller. Removed after making shopController always active 
    {
       // Debug.Log("Updatings bones from shop bones");
       // bones = GameObject.Find("shopController").GetComponent<shopController>().bones;
    }

    public void makeDie() //Locate the prefab for a Die and make the number of dice currently available
    {
        prefabDice = GameObject.Find("MakeDie (0)"); //Reload prefab object as target of prefabDice
        prefabDiceParent = GameObject.Find("Contents").GetComponent<RectTransform>(); //reload content holder 
        
        if (diceShown < numberDice)
        {
            for (int i = 1; i < numberDice; i++)
            {
                GameObject fn = GameObject.Find("MakeDie (" + (i) + ")");
                if (fn == null) //Check to see if the thing exists before you make a new one with a fixed name
                {
                    GameObject go = GameObject.Instantiate(prefabDice, prefabDiceParent);
                    go.name = "MakeDie (" + (i) + ")";
                }
            }
        }
    }

    public void updateCurrentDice() //Pull information from the Dice Renderer into the gamecontroller
    {
        for (int i = 0; i < numberDice; i++)
        {
            rd = GameObject.Find("MakeDie (" + i + ")").GetComponent<RenderDice>();
            currentDice[i] = rd.DiceVal; //Values of the current dice displayed
            selectedDice[i] = rd.selected; //Whether or not any of the current dice set are selected
        }
    }

    public void determineValid() //A valid dice is (Not kept in previous round) and (could be scored)
    {
        selectCount = 0;
        validCount = 0;
        for (int i = 0; i < currentDice.Length; i++)
        {
            validDiceSelect[i] = false;
            if (selectedDice[i])
            {
                selectCount++;
                if (!keptDice[i])
                {
                    validDiceSelect[i] = true;
                    validCount++;
                }
                else if (keptDice[i])
                {
                    validDiceSelect[i] = false;
                }
            }
        }
    }

    public void loadScorebook() //ScoreBook is an object array holding a name, value, and enable status for each type of dice score implemented
    {
        int singTwo = 100;
        int singOne = 50;
        int fourOak = 1000;
        int fourOakPair = 1500;
        int fiveOak = 2000;
        int sixOak = 3000;
        int straight = 1500;
        int triPair = 1500;
        int duoTriplets = 2500;
        int trips = 300;
        int pair = 150;

        GameObject scorebookKeeper = GameObject.Find("scorebookKeeper");
            if(scorebookKeeper == null)
            {
             scorebookKeeper = new GameObject("scorebookKeeper");
                for (int i = 0; i < scoreBookArray.Length; i++)
                {
                scoreBookArray[i] = scorebookKeeper.AddComponent<scoreBook>();
                }
            }

       
    
        scoreBookArray[0].name = "Empty"; scoreBookArray[0].value = 0; scoreBookArray[0].enable = false;
        scoreBookArray[1].name = "singTwo"; scoreBookArray[1].value = singTwo; scoreBookArray[1].enable = false;
        scoreBookArray[2].name = "singOne"; scoreBookArray[2].value = singOne; scoreBookArray[2].enable = true;
        scoreBookArray[3].name = "fourOak"; scoreBookArray[3].value = fourOak; scoreBookArray[3].enable = false;
        scoreBookArray[4].name = "fourOakPair"; scoreBookArray[4].value = fourOakPair; scoreBookArray[4].enable = false;
        scoreBookArray[5].name = "fiveOak"; scoreBookArray[5].value = fiveOak; scoreBookArray[5].enable = false;
        scoreBookArray[6].name = "sixOak"; scoreBookArray[6].value = sixOak; scoreBookArray[6].enable = false;
        scoreBookArray[7].name = "straight"; scoreBookArray[7].value = straight; scoreBookArray[7].enable = false;
        scoreBookArray[8].name = "triPair"; scoreBookArray[8].value = triPair; scoreBookArray[8].enable = false;
        scoreBookArray[9].name = "duoTriplets"; scoreBookArray[9].value = duoTriplets; scoreBookArray[9].enable = false;
        scoreBookArray[10].name = "trips"; scoreBookArray[10].value = trips; scoreBookArray[10].enable = false;
        scoreBookArray[11].name = "pair"; scoreBookArray[11].value = pair; scoreBookArray[11].enable = false;

        
    }

    public void RollPenalty()
    {
        bones -= rollPenalty*(numberDice-selectCount);
        rollCount++;
    }



    public void Reset() // Soft reset for Roll Button use or changing scenes
    {
        score = 0;
        tally = 0;
        selectCount = 0;
        roundCount = 0;
        roundMult = 1;

        for (int i = 0; i < numberDice; i++)
        {
            validDiceSelect[i] = false;
            keptDice[i] = false;
        }   
    }
}
