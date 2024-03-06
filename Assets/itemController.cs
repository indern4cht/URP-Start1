using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class itemController : MonoBehaviour
{
    public string itemTitle;
    public string itemDescription;
    public string itemPriceText;
    public int itemPrice;
    public bool item_status = false;
    public TMP_Text TMP_itemTitle;
    public TMP_Text TMP_itemDescription;
    public TMP_Text TMP_itemPriceText;
    int bones;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bones = GameObject.Find("shopController").GetComponent<shopController>().bones;
        TMP_itemTitle.text = itemTitle;
        TMP_itemDescription.text = itemDescription;
        TMP_itemPriceText.text = itemPriceText;
    }

    public void ItemClick()
    {
        if(bones >= itemPrice)
        {
           shopController ShopController = GameObject.Find("shopController").GetComponent<shopController>();
            item_status = false;
            ShopController.itemPurchase();
            Debug.Log("Buying Item");
            bones -= itemPrice;
            GameObject.Find("GameController").GetComponent<gameController>().bones = bones;
        }
    }
}
