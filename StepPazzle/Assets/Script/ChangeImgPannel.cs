using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeImgPannel : MonoBehaviour {
    [SerializeField]
    private Material initMaterial = null;
    [SerializeField]
    private Material changeMaterial = null;

    BoxCollider pannelCollider;

    private bool changedTex = false;
    public bool GetChangeTexFlag() { return changedTex; }

    public void Start()
    {
        transform.GetComponent<Renderer>().material = initMaterial;
        pannelCollider = GetComponent<BoxCollider>();
        pannelCollider.size = new Vector3(0.5f,1.0f,0.5f);
        changedTex = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && changedTex == false)
        {
            transform.GetComponent<Renderer>().material = changeMaterial;
            transform.tag = "changedPannel";
            changedTex = true;
            pannelCollider.size = new Vector3(1, 2, 1);
        }
    }
}
