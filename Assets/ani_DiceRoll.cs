using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ani_DiceRoll : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteList;
    public float elapsed;
    [SerializeField] private Animator myRoll = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartRoll()
    {
        // StartCoroutine(RunAnimation());
        //Debug.Log("Do the First thing");
        myRoll.Play("DiceRoll", 0, 0.0f);
        
        spriteRenderer.sprite = spriteList[0];
    }

  

    public IEnumerator RunAnimation()
    {
        Debug.Log("Doing the coroutine");
        
            for (int i = 0; i < spriteList.Length; i++)
            {
                spriteRenderer.sprite = spriteList[i];
            Debug.Log("Image =" + i);
            }
       
            yield return new WaitForSeconds(5f);
        
    }
}
