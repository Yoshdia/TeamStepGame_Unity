using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour {

    [SerializeField]
    private GameObject pannel = null;

    private List<GameObject> pannelList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                Vector3 pannelPos = new Vector3(x * 1, 0, z * 1);
                Quaternion pannelQua = new Quaternion();
                GameObject mapPannel = Instantiate(pannel, pannelPos, pannelQua);
                pannelList.Add(mapPannel);

            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
