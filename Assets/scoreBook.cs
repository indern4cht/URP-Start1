using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreBook : MonoBehaviour
{
    public string name;
    public int value;
    public bool enable;
    public static scoreBook ScoreBook;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (ScoreBook == null)
        {
            ScoreBook = this;
            //DontDestroyOnLoad(gameObject); //Keep this controller alive between loads of different scenes. (Rename Tally1 to controller?)


        }
        else if (ScoreBook != this)
        {
            Destroy(ScoreBook);
            //ScoreBook = this;
            //Instance = this;
            //DontDestroyOnLoad(gameObject); //Keep this controller alive between loads of different scenes. (Rename Tally1 to controller?)
        }
        //Instance = this;
        DontDestroyOnLoad(ScoreBook);

    }
    public scoreBook()
    {
        
    }
}
