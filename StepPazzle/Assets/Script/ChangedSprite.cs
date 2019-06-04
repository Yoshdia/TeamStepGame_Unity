using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedSprite : MonoBehaviour {

    [SerializeField]
    private Sprite defaultSprite=null;
    [SerializeField]
    private Sprite changedSprite = null;

    // Update is called once per frame
    void Start()
    {
        transform.tag = "defaultPannel";
    }

    public void StepedSpriteChange()
    {
        transform.GetComponent<SpriteRenderer>().sprite = changedSprite;
        transform.tag = "changedPannel";
    }

    //まだ実装途中の為使用していない関数。
    public void ReturnSpriteChange()
    {
        transform.GetComponent<SpriteRenderer>().sprite = defaultSprite;
        transform.tag = "defaultPannel";
    }
}
