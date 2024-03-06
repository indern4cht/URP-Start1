using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ttController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> Children = new List<GameObject>();
        List<bool> SetActive = new List<bool>();
        Transform transform = this.gameObject.transform;
        foreach (Transform child in transform)
        {
            Children.Add(child.gameObject);
            SetActive.Add(child.gameObject.activeSelf);
        }
        for(int i= 0; i<Children.Count; i++)
        {
            GameObject.Find("GameController").GetComponent<gameController>().toolTips[i] = SetActive[i];
        }   
    }

    public void ClearTT()
    {
        this.gameObject.SetActive(false);
    }
}
