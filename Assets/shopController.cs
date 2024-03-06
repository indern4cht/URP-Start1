using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class shopController : MonoBehaviour
{
    public static shopController ShopController;
    gameController GameController;
    public TMP_Text bonesText;
    public int bones;
    public List<string> item_Names;
    public List<bool> item_Status;
    public int itemselect;
    public List<GameObject> tier1_items;
    public List<GameObject> tier2_items;
    public List<GameObject> tier3_items;
    public List<GameObject> tier4_items;
    public List<GameObject> tier5_items;
    public List<List<GameObject>> itemTiersList;
    public List<bool> activeTiers;
    public int prevScene = 0;


    // Start is called before the first frame update
    void Start()
    {
        //string[] names = { 
        //"New Die 1",
        //"Ones",
        //"Trips",
        //"Fours",
        //"Fives", //5
        //"Sixes",
        //"FourOakPair",
        //"ThreePair",
        //"TwoTrips",
        //"Straight", //10
        //"Runes",
        //"Tier 2",
        //"New Die 2",
        //"Tier 3",
        //"New Die 3", //15
        //"Tier 4",
        //"New Die 4",
        //"Tier 5",
        //"New Die 5"
        //};
        //item_Names = new List<string>(names);

        //bool[] status =
        //{
        //    true,
        //    true,
        //    true,
        //    true,
        //    true, //5
        //    true,
        //    true,
        //    true,
        //    true,
        //    true, //10
        //    true,
        //    true,
        //    true,
        //    true,
        //    true, //15
        //    true,
        //    true,
        //    true,
        //    true,
        //};
        //item_Status = new List<bool>(status);

        activeTiers[0] = true;
        activeTiers[1] = false;
        activeTiers[2] = false;
        activeTiers[3] = false;
        activeTiers[4] = false;

        Debug.Log("Run Me Down");
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex ==0)
        {
            prevScene = 0;
        }
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bonesText = GameObject.Find("Bones Counter txt").GetComponent<TMPro.TMP_Text>();
            bones = GameController.bones;
            bonesText.text = bones.ToString();

            if (prevScene == 0)
            {
                tier1_items = getTiers(1);
                tier2_items = getTiers(2);
                tier3_items = getTiers(3);
                tier4_items = getTiers(4);
                tier5_items = getTiers(5);
                itemTiersList = new List<List<GameObject>>();
                itemTiersList.Add(tier1_items);
                itemTiersList.Add(tier2_items);
                itemTiersList.Add(tier3_items);
                itemTiersList.Add(tier4_items);
                itemTiersList.Add(tier5_items);
                prevScene = 1;
            }

            for (int i = 1; i < itemTiersList.Count; i++) //needs to run always
            {
                foreach (GameObject item in itemTiersList[i])
                {
                    if (!activeTiers[i])
                    {
                        item.SetActive(false); 
                        
                    }
                    if (activeTiers[i])
                    {
                        item.SetActive(true);
                        //item.transform.parent = GameObject.Find("Tier (0)").transform;
                        item.transform.SetParent(GameObject.Find("Tier (0)").transform);
                    }
                    
                }
            }
        }

        for (int i = 0; i < item_Status.Count; i++)
        {
            GameObject item = GameObject.Find("Item Template (" + i + ")");
            if (item != null)
            {
               // item_Status[i] = item.GetComponent<itemController>().item_status;
                if (item_Status[i] != true)
                {
                    item.SetActive(false);
                   // itemselect = i + 1;
                  //  itemResolve();
                }
            }
        }

    }

    private void Awake()
    {
        GameController = GameObject.Find("GameController").GetComponent<gameController>();
        ShopController = GameObject.Find("shopController").GetComponent<shopController>();
        
        if (ShopController == null)
        {
            ShopController = this;
        }
        else if (ShopController != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
       
    }



    public void buyDie()
    {
        //GameController.numberDice++;
        //GameController.DiceNumberCheck();
    }

    public void itemPurchase()
    {
        for (int i = 0; i < item_Status.Count; i++)
        {
            GameObject item = GameObject.Find("Item Template (" + i + ")");
            if (item != null)
            {
                Debug.Log("Item is found");
                item_Status[i] = item.GetComponent<itemController>().item_status;
                if (item_Status[i] != true)
                {
                    item.SetActive(false);
                    itemselect = i + 1;
                    itemResolve();
                }
            }
        }
    }    

    public void itemResolve()
    {
        scoreBook[] scoreBookArray = GameController.scoreBookArray;
        for (int i = 0; i<scoreBookArray.Length; i++)
        {

        }
        //scoreBook[] scoreBookArray = scorebookKeeper;
        switch (itemselect)
        {
            case 0:
                Console.WriteLine("Case 0");
                break;
            case 1: //New Die
                Console.WriteLine("Case 1");
                GameController.numberDice++;
                GameController.DiceNumberCheck();
                break;
            case 2: //Enable SingTwp
                Console.WriteLine("Case 2");
                scoreBookArray[1].enable = true;
                GameController.GetComponent<gameController>().enabledDice[2] = true;
                break;
            case 3:
                Console.WriteLine("Case 3");
                scoreBookArray[10].enable = true;
                break;
            case 4:
                Console.WriteLine("Case 4");
                scoreBookArray[3].enable = true;
                break;
            case 5:
                Console.WriteLine("Case 5");
                scoreBookArray[5].enable = true;
                break;
            case 6:
                Console.WriteLine("Case 6");
                scoreBookArray[6].enable = true;
                break;
            case 7:
                Console.WriteLine("Case 7");
                scoreBookArray[4].enable = true;
                break;
            case 8:
                Console.WriteLine("Case 8");
                scoreBookArray[8].enable = true;
                break;
            case 9:
                Console.WriteLine("Case 9");
                scoreBookArray[9].enable = true;
                break;
            case 10:
                Console.WriteLine("Case 10");
                scoreBookArray[7].enable = true;
                break;
            case 11:
                Console.WriteLine("Case 11");
  
                //GameController.numberDice = 6;
                //GameController.DiceNumberCheck();
                break;
            case 12: //Tier 2 Shop
                activeTiers[1] = true;
                break;
            case 13:
                GameController.numberDice++;
                GameController.DiceNumberCheck();
                break;
            case 14: //Tier 3 shop
                activeTiers[2] = true;
                break;
            case 15:
                GameController.numberDice++;
                GameController.DiceNumberCheck();
                break;
            case 16: //Tier 4 Shop
                activeTiers[3] = true;
                break;
            case 17:
                GameController.numberDice++;
                GameController.DiceNumberCheck();
                break;
            case 18: //Tier 5 shop
                activeTiers[4] = true;
                break;
            case 19:
                GameController.numberDice++;
                GameController.DiceNumberCheck();
                break;
            case 20:
                scoreBookArray[11].enable = true;
                break;
            case 21:
                GameController.enabledDice[3] = true;
                break;
            case 22:
                GameController.enabledDice[4] = true;
                break;
            case 23:
                GameController.enabledDice[5] = true;
                break;
            case 24:
                GameController.enabledDice[6] = true;
                break;
            default:
                Console.WriteLine("Default case");
                break;
            //itemselect = 0;
        }
    }

    public List<GameObject> getTiers(int tierValue)
    {
        GameObject container = GameObject.Find("Tier (" + (tierValue-1) + ")");
        Transform transform = container.transform;
        List<GameObject> Children = new List<GameObject>();

        //int i = 0;
        foreach (Transform child in transform)
        {
        Children.Add(child.gameObject);
            item_Status.Add( child.gameObject.GetComponent<itemController>().item_status);
            item_Names.Add( child.gameObject.GetComponent<itemController>().itemTitle);
        }
        return Children;
    }    
}
