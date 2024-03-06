using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttController : MonoBehaviour
{
    public List<bool> SetActive;
    public List<GameObject> Children;
    public int ChildCount;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = this.gameObject.transform;
        foreach (Transform child in transform)
        {
            Children.Add(child.gameObject);
            SetActive.Add(transform.gameObject.activeSelf);
        }
        ChildCount = Children.Count;
        //SetActive = new List<bool>();
       // for (int i = 0; i < ChildCount; i++)
       // {
        //    GameObject.Find("GameController").GetComponent<gameController>().toolTips[i] = SetActive[i];
       // }
        activeTT();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void activeTT()
    {
        // Transform transform = this.gameObject.transform;
        //List<bool> SetActive = new List<bool>();
        for (int i = 0; i < ChildCount; i++)
        {
            if (i == 6 && !Children[i].activeSelf)
            {
                for (int j = 0; j < ChildCount; j++)
                {
                    Children[j].SetActive(false);
                    SetActive[j] = false;
                    GameObject.Find("GameController").GetComponent<gameController>().toolTips[j] = false;
                    GameObject.Find("GameController").GetComponent<gameController>().tt_Clear = true;
                }
            }
            //Children[i] =
            else
            { 
            SetActive[i] = Children[i].activeSelf;
            }
        }

    }

    public void ClearTT()
    {
        this.gameObject.SetActive(false);
    }
}
