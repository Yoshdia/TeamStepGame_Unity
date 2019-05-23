using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPositioning : MonoBehaviour
{

    private int[,] mapDate = { };
    private GameObject[,] mapObjectDate = { };

    [SerializeField]
    private GameObject wallObject = null;
    [SerializeField]
    private GameObject pannelObject = null;
    [SerializeField]
    private GameObject whiteObject = null;
    
    // Use this for initialization
    void Start()
    {

    }

    public void Positioning()
    {
        mapDate = GetComponent<MapController>().GetMapDate();
        mapObjectDate = GetComponent<MapController>().GetMapObjectDate();
        //mapDateをもとにステージを配置していく
        //パネルのサイズを取得
        float movePannelSize = pannelObject.GetComponent<MeshRenderer>().bounds.size.x;
        for (int z = 0; z < mapDate.GetLength(0); z++)
        {
            for (int x = 0; x < mapDate.GetLength(1); x++)
            {
                int pannelInfo = mapDate[z, x];
                Vector3 objectPos = new Vector3(x * movePannelSize, 0, z * movePannelSize);
                GameObject setObject = whiteObject;
                if (pannelInfo == (int)MapDate.eGroundName.eWall)
                {
                    setObject = wallObject;
                }
                if (pannelInfo == (int)MapDate.eGroundName.eDefaultPannel)
                {
                    setObject = pannelObject;
                }
                if (pannelInfo == (int)MapDate.eGroundName.ePlayerPosition ||
                    pannelInfo == (int)MapDate.eGroundName.eWhite)
                {
                    setObject = whiteObject;
                }
                if (setObject != null)
                {
                    Vector3 objectPosOnMap = new Vector3(z, 0, x);
                    setObject.GetComponent<GroundCommon>().SetGroundPositionOnMap(objectPosOnMap);
                }
                setObject = Instantiate(setObject, objectPos, new Quaternion());
                mapObjectDate[z, x] = setObject;
                setObject.transform.parent = transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
