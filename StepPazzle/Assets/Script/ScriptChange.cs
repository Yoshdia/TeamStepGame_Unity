using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.UI;

public class ScriptChange : MonoBehaviour
{
    //[SerializeField]
    //private Sprite defaultSprite;
    [SerializeField]
    private Sprite changedSprite;

    // Update is called once per frame
    void Start()
    {
        //transform.GetComponent<Renderer>() = changedSprite;
        //Sprite ss=Instantiate(changedSprite, new Vector3(0,0,0), new Quaternion());
        transform.GetComponent<SpriteRenderer>().sprite = changedSprite;
    }
}
