using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImgPannel : MonoBehaviour {
    [SerializeField]
    private Material initMaterial = null;
    [SerializeField]
    private Material changeMaterial = null;

    private bool changedTex = false;
    public bool GetChangeTexFlag() { return changedTex; }

    public void Start()
    {
        this.GetComponent<Renderer>().material = initMaterial;
        changedTex = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && changedTex == false)
        {
            this.GetComponent<Renderer>().material = changeMaterial;
            this.tag = "changedPannel";
            changedTex = true;
        }
    }
}
