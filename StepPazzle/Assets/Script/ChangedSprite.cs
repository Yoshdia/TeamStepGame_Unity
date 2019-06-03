using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedSprite : MonoBehaviour {

    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Sprite changedSprite;

    // Update is called once per frame
    void Start()
    {
        //transform.GetComponent<Renderer>() = changedSprite;
        //Sprite ss=Instantiate(changedSprite, new Vector3(0,0,0), new Quaternion());

        //transform.GetComponent<SpriteRenderer>().sprite = changedSprite;
        transform.tag = "defaultPannel";
    }

    public void StepedMarterialChange()
    {
        transform.GetComponent<SpriteRenderer>().sprite = changedSprite;
        transform.tag = "changedPannel";
    }

    //まだ実装途中の為使用していない関数。
    public void ReturnMarterialChange()
    {
        transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        transform.tag = "defaultPannel";
    }
}
