using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RenderDice : MonoBehaviour
{

    public Sprite[] spriteList;
    public Sprite emptySprite;
    public Vector3 locScale;

    //public bool DieCast;
    public int Val;
    public bool selected = false;
    public int DiceVal;
    public SpriteRenderer spriteRenderer;
    public float elapsed;
    public bool[] enabledDice;

    [SerializeField] private Animator myRoll = null;
    [SerializeField] private AudioClip RollSound;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {

       
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        GameObject.Find("GameController").GetComponent<gameController>().makeDie();
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Startroll()
    {

        GameObject go = this.gameObject;
        GameObject child = go.transform.GetChild(3).gameObject;
        ParticleSystem ps = child.GetComponent<ParticleSystem>();
        ps.Play();

        RollDie();
        myRoll.Play("DiceRoll", 0, 0.0f);
        audioSource.clip = RollSound;
        audioSource.Play();
        unlockCheck();
    }

  // public IEnumerator RollDie() //Removed now that animation exists. 
    //{
    //    elapsed = 0;
    //    Debug.Log("Roll the bones");
    //    while (elapsed < .4)
    //    {
    //        spriteRenderer.sprite = emptySprite;
    //        elapsed += Time.deltaTime;
    //        Val = UnityEngine.Random.Range(1, 7);
    //        DiceVal = Val;
    //       // spriteRenderer.sprite = spriteList[Val];
    //        selected = false;
    //         yield return null;
    //    }
    //    spriteRenderer.sprite = spriteList[Val];
    //}

    public void RollDie()
    {
        spriteRenderer.sprite = emptySprite;
        Val = UnityEngine.Random.Range(1, 7);
        DiceVal = Val;
        selected = false;
       
    }    

    public void SetDie(int set)
    {
       
        spriteRenderer.sprite = spriteList[set];
        Val = set;
        DiceVal = Val;
        selected = false;
        
    }

    public void SelectDie()
    {

        if (selected)
        {
            Val -= 7;
            spriteRenderer.sprite = spriteList[Val];
        }
        if (!selected)
        { 
            Val += 7;
            spriteRenderer.sprite = spriteList[Val];
        }
        selected = !selected;
    }
   
    public void unlockCheck()
    {
        enabledDice = GameObject.Find("GameController").GetComponent<gameController>().enabledDice;
        if (enabledDice[Val])
        {
            spriteRenderer.sprite = spriteList[Val];
        }
        else
        {
            Val = 0;
            spriteRenderer.sprite = spriteList[0];
            DiceVal = 0;
        }
    }


}
