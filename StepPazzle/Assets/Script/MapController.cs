using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MapDateから受け取った情報をもとに、書き直したり返すクラス
public class MapController : MonoBehaviour
{

    public struct sMapDate
    {
        public int[,] mapNumberDate;
        public GameObject[,] mapObjectDate;
    }

    private sMapDate mapDate;

    private void Awake()
    {
        mapDate.mapNumberDate = GetComponent<MapDate>().GetMapDate();
        mapDate.mapObjectDate = GetComponent<MapDate>().GetNullObjectDate();
        GetComponent<MapPositioning>().Positioning();
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    //二次元配列からプレイヤー座標を算出し返す
    public Vector3 GetFirstPositionPlayer()
    {
        Vector3 playerPosition = new Vector3(-1, 1, -1);

        bool checkEnd = false;

        for (playerPosition.z = 0; playerPosition.z < mapDate.mapNumberDate.GetLength(0); playerPosition.z++)
        {
            for (playerPosition.x = 0; playerPosition.x < mapDate.mapNumberDate.GetLength(1); playerPosition.x++)
            {
                if (mapDate.mapNumberDate[(int)playerPosition.z, (int)playerPosition.x] == (int)MapDate.eGroundName.ePlayerPosition)
                {
                    checkEnd = true;
                    break;
                }
            }
            if (checkEnd == true)
            {
                break;
            }
        }

        return playerPosition;
    }

    //プレイヤーが移動したときに呼ばれる関数。マップデータを書き直す
    public void PlayerMovedChangeMapDate(Vector3 playerPos, Vector3 movePos)
    {
        if (mapDate.mapNumberDate[(int)playerPos.z, (int)playerPos.x] == (int)MapDate.eGroundName.eDefaultPannel)
        {
            //プレイヤーがいたマップ座標をchangedPannelに変え、ChangedMaterialPannelのマテリアルを変えさせる関数を呼ぶ
            mapDate.mapNumberDate[(int)playerPos.z, (int)playerPos.x] = (int)MapDate.eGroundName.eChangedPannel;
            mapDate.mapObjectDate[(int)playerPos.z, (int)playerPos.x].GetComponent<ChangeMaterialPannel>().StepedMarterialChange();
        }
        else if(mapDate.mapNumberDate[(int)playerPos.z, (int)playerPos.x]==(int)MapDate.eGroundName.eChangedPannel)
        {
            mapDate.mapNumberDate[(int)playerPos.z, (int)playerPos.x] = (int)MapDate.eGroundName.eDefaultPannel;
            mapDate.mapObjectDate[(int)playerPos.z, (int)playerPos.x].GetComponent<ChangeMaterialPannel>().ReturnMarterialChange();
        }
        //移動先の座標をPlayerがいる番号に書き換える
        mapDate.mapNumberDate[(int)(playerPos.z + movePos.z), (int)(playerPos.x + movePos.x)] = (int)MapDate.eGroundName.ePlayerPosition;

    }

    //二次元配列を返す
    public int[,] GetMapDate()
    {
        return mapDate.mapNumberDate;
    }
    public GameObject[,] GetMapObjectDate()
    {
        return mapDate.mapObjectDate;
    }
}
