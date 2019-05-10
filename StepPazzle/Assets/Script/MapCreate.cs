using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour {

    [SerializeField]
    private GameObject pannel = null;

    IEnumerator Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                Vector3 pannelPos = new Vector3(z * 1, 0, x * 1);
                Quaternion pannelQua = new Quaternion();
                GameObject mapPannel = Instantiate(pannel, pannelPos, pannelQua);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    void Update()
    {

    }
}
