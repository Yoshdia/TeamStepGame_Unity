using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MapDateから受け取った情報をもとに、書き直したり返すクラス
public class MapController : MonoBehaviour
{
    //int型とGameObject型を持った構造体 どちらにもMapDateから値を取得する
    public struct sMapDate
    {
        public int[,] mapNumberDate;
        public GameObject[,] mapObjectDate;
    }
    private sMapDate mapDate;
    //mapControllerのマップ情報の配列サイズを取得
    private Vector3 mapLengthMin;
    private Vector3 mapLengthMax;

    private void Awake()
    {
        //int型とGameobject型のマップ情報をmapControllerから取得
        mapDate.mapNumberDate = GetComponent<MapDate>().GetMapDate();
        mapDate.mapObjectDate = GetComponent<MapDate>().GetNullObjectDate();
        //MapPositioningの、マップ生成関数
        GetComponent<MapPositioning>().Positioning();
        
        mapLengthMin = new Vector3(0, 0, 0);
        mapLengthMax = new Vector3(mapDate.mapNumberDate.GetLength(1), 0, mapDate.mapNumberDate.GetLength(0));
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject型のマップ情報の中で、一つでも変化前パネルがあった場合クリアフラグを倒しゲームを続行させる
        bool clearFlag = true;
        for (int z = 0; z < mapLengthMax.z; z++)
        {
            for (int x = 0; x < mapLengthMax.x; x++)
            {
                if (mapDate.mapNumberDate[z, x] == (int)MapDate.eGroundName.eDefaultPannel)
                {
                    clearFlag = false;
                }
            }
        }
        if (clearFlag == true)
        {
            Debug.Log("Clear!");
        }
    }

    //二次元配列からプレイヤー座標を算出し返す
    public Vector3 GetFirstPositionPlayer()
    {
        Vector3 playerPosition = new Vector3(-1, 1, -1);

        bool checkEnd = false;

        for (playerPosition.z = mapLengthMin.z; playerPosition.z < mapLengthMax.z; playerPosition.z++)
        {
            for (playerPosition.x = mapLengthMin.x; playerPosition.x < mapLengthMax.x; playerPosition.x++)
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
    public void PlayerdMovedChangeMapDate(Vector3 playerPos, Vector3 movePos)
    {
        int beforePositionZOnMap = (int)(playerPos.z);
        int beforePositionXOnMap = (int)(playerPos.x);

        int nextPositionZOnMap = (int)((playerPos.z) + (movePos.z));
        int nextPositionXOnMap = (int)((playerPos.x) + (movePos.x));

        //Debug.Log("" + playerPos + mapDate.mapNumberDate[(int)playerPos.z, (int)playerPos.x]);
        if (mapDate.mapNumberDate[nextPositionZOnMap, nextPositionXOnMap] == (int)MapDate.eGroundName.eDefaultPannel)
        {
            //プレイヤーがいたマップ座標をchangedPannelに変え、ChangedMaterialPannelのマテリアルを変えさせる関数を呼ぶ
            mapDate.mapNumberDate[nextPositionZOnMap, nextPositionXOnMap] = (int)MapDate.eGroundName.eChangedPannel;
            mapDate.mapObjectDate[nextPositionZOnMap, nextPositionXOnMap].GetComponent<ChangedSprite>().StepedSpriteChange();
        }
        else if (mapDate.mapNumberDate[nextPositionZOnMap, nextPositionXOnMap] == (int)MapDate.eGroundName.eChangedPannel ||
            mapDate.mapNumberDate[nextPositionZOnMap, nextPositionXOnMap] == (int)MapDate.eGroundName.eWhite ||
            mapDate.mapNumberDate[nextPositionZOnMap, nextPositionXOnMap] == (int)MapDate.eGroundName.ePlayerPosition)
        {
            mapDate.mapNumberDate[beforePositionZOnMap, beforePositionXOnMap] = (int)MapDate.eGroundName.eDefaultPannel;
            mapDate.mapObjectDate[beforePositionZOnMap, beforePositionXOnMap].GetComponent<ChangedSprite>().ReturnSpriteChange();
        }
        //移動先の座標をPlayerがいる番号に書き換える
        //mapDate.mapNumberDate[(int)(playerPos.z + movePos.z), (int)(playerPos.x + movePos.x)] = (int)MapDate.eGroundName.ePlayerPosition;
    }

    public int GetNumberOnMap(int z, int x)
    {
        if (mapLengthMin.x > x ||
    mapLengthMax.x == x ||
    mapLengthMin.z > z ||
    mapLengthMax.z == z)
        {
            Debug.Log("Error!");
            return 0;
        }
        return mapDate.mapNumberDate[z, x];
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
