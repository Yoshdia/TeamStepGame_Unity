using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialPannel : MonoBehaviour {

    [SerializeField]
    private Material defaultMaterial = null;
    [SerializeField]
    private Material changeMaterial = null;

    void Start () {
        //現在のMarterialをdefault状態にする
        transform.GetComponent<Renderer>().material = defaultMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StepedMarterialChange();
        }
    }

    public void StepedMarterialChange()
    {
        transform.GetComponent<Renderer>().material = changeMaterial;
        transform.tag = "changedPannel";
    }

    //まだ実装途中の為使用していない関数。
    public void ReturnMarterialChange()
    {
        transform.GetComponent<Renderer>().material = defaultMaterial;
        transform.tag = "defaultPannel";
    }
}
