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
    private Vector3 mapLengthMax;

    //マップデータ上でPlayerが移動した位置を記憶
    private List<Vector3> movedPlayerPosList;

    //ステージデータを受け取る関数を呼ぶための変数
    MapDate haveMapData = null;

    MapPositioning MapCreater = null;
    [HideInInspector]
    public FootPrint footPrinter = null;
    public EffectSpawner effectSpawner = null;
    public EffectSpawner effectSpawner2 = null;

    Vector3 spriteSize = new Vector3();
    [SerializeField]
    GameObject white = null;
    GameObject instanceWhite = null;

    public void InitProcces()
    {
        //stageName = MapDate.eStageName.eFirstStage;

        GetComponent<MapPositioning>().FirstProccess();
        haveMapData = GetComponent<MapDate>();

        MapCreater = GetComponent<MapPositioning>();
    }

    public void MapReset(MapDate.eStageName selectedStageName, ref Vector3 cameraPos,ref float speed)
    {
        //int型とGameobject型のマップ情報をmapControllerから取得
        string fileName = null;
        string monoChromeFileName = null;
        Vector2 mapSize = new Vector2(0, 0);

        //マップデータを受け取ると共に、spriteSizeとfileName、mainCameraの位置と向きを参照渡しして受け取る
        mapDate.mapNumberDate = haveMapData.GetMapDate(selectedStageName, ref spriteSize, ref fileName, ref monoChromeFileName, ref cameraPos, ref mapSize,ref speed);
        mapDate.mapObjectDate = haveMapData.GetNullObjectDate();

        white.transform.localScale = new Vector3(spriteSize.x*mapSize.x, spriteSize.y*mapSize.y, 0.1f);
        Vector3 whiteP = new Vector3(0, 0,0);
        whiteP.x =( (spriteSize.x * mapSize.x) / 2)-spriteSize.x/2;
        whiteP.y =( (spriteSize.y * mapSize.y) / 2)-spriteSize.y/2;
        whiteP.z = 0.2f;
        white.transform.position = whiteP;

        if (instanceWhite == null)
        {
            instanceWhite = Instantiate(white);
        }
        else
        {
            Destroy(instanceWhite.gameObject);
            instanceWhite = Instantiate(white);
        }


        Debug.Log("" + spriteSize);
        footPrinter.SetSpriteSize(spriteSize);
        effectSpawner.SetSpriteSize(spriteSize);
        effectSpawner2.SetSpriteSize(spriteSize);

        //マップ生成関数
        MapCreater.Positioning(fileName, monoChromeFileName, spriteSize);

        mapLengthMax = new Vector3(mapDate.mapNumberDate.GetLength(1), 0, mapDate.mapNumberDate.GetLength(0));

        if (movedPlayerPosList != null)
        {
            for (int i = movedPlayerPosList.Count - 1; i >= 0; i--)
            {
                movedPlayerPosList.RemoveAt(i);
            }
        }
        else
        {
            //マップ生成時にListを初期化するためここで要素を入れておく
            movedPlayerPosList = new List<Vector3>();
            movedPlayerPosList.Add(new Vector3(0, 0, 0));
        }

    }

    // Update is called once per frame
    public bool ClearCheck()
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
            return true;
        }
        return false;
    }

    //二次元配列からプレイヤー座標を算出し返す
    public Vector3 GetFirstPositionPlayer()
    {
        Vector3 playerPosition = new Vector3(-1, -1, -1);

        bool checkEnd = false;

        for (playerPosition.y = 0; playerPosition.y < mapLengthMax.z; playerPosition.y++)
        {
            for (playerPosition.x = 0; playerPosition.x < mapLengthMax.x; playerPosition.x++)
            {
                int groundName = (int)mapDate.mapNumberDate[(int)playerPosition.y, (int)playerPosition.x];
                if (groundName == (int)MapDate.eGroundName.ePlayerPosition)
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
        movedPlayerPosList.Add(playerPosition);

        return playerPosition;
    }

    public void MovedPlayer(Vector3 currentPlayerPosOnMap, Vector3 moveVec)
    {
        //二次配列上のプレイヤーが移動する予定地
        Vector3 nextPositionOnMap = currentPlayerPosOnMap + moveVec;

        bool changedSprite = false;
        //二次配列上で、次に書き換える予定の名前
        MapDate.eGroundName nextGroundName;

        Vector3 beforePlayerPos = movedPlayerPosList[movedPlayerPosList.Count - 1];

        //移動予定地が前回移動した地点だった場合
        if (nextPositionOnMap == beforePlayerPos)
        {
            movedPlayerPosList.RemoveAt(movedPlayerPosList.Count - 1);
            //前回移動した地点だったが、白い床だった場合余計な関数を呼ばないようにする
            if (mapDate.mapNumberDate[(int)currentPlayerPosOnMap.y, (int)currentPlayerPosOnMap.x] == (int)MapDate.eGroundName.eWhite)
            {
                return;
            }
            changedSprite = true;
            nextGroundName = MapDate.eGroundName.eDefaultPannel;
            mapDate.mapNumberDate[(int)currentPlayerPosOnMap.y, (int)currentPlayerPosOnMap.x] = (int)nextGroundName;
            mapDate.mapObjectDate[(int)currentPlayerPosOnMap.y, (int)currentPlayerPosOnMap.x].GetComponent<ChangedSprite>().ChangeSprite(changedSprite);
            //spriteがもとに戻るので、そこにあった足跡も消す
            footPrinter.DeleteOneFoot();
            Vector3 ppp = nextPositionOnMap;
            effectSpawner2.SetEffect(ppp);
        }
        //移動予定地が変化前だった場合
        else if (mapDate.mapNumberDate[(int)nextPositionOnMap.y, (int)nextPositionOnMap.x] == (int)MapDate.eGroundName.eDefaultPannel)
        {
            changedSprite = false;
            nextGroundName = MapDate.eGroundName.eChangedPannel;
            mapDate.mapNumberDate[(int)nextPositionOnMap.y, (int)nextPositionOnMap.x] = (int)nextGroundName;
            mapDate.mapObjectDate[(int)nextPositionOnMap.y, (int)nextPositionOnMap.x].GetComponent<ChangedSprite>().ChangeSprite(changedSprite);
            movedPlayerPosList.Add(currentPlayerPosOnMap);
            //spriteを変化させたので、そこに足跡を生成させる
            Vector3 footPos = nextPositionOnMap;
            Vector3 footDirection = currentPlayerPosOnMap - nextPositionOnMap;
            //footQua = Quaternion.Euler(nextPositionOnMap-currentPlayerPosOnMap);
            footPrinter.SetFoot(footPos, footDirection);
            effectSpawner.SetEffect(footPos);
        }
        else if (mapDate.mapNumberDate[(int)nextPositionOnMap.y, (int)nextPositionOnMap.x] == (int)MapDate.eGroundName.eWhite)
        {
            movedPlayerPosList.Add(currentPlayerPosOnMap);
        }
        else
        {
            return;
        }
    }

    //playerから呼ばれる。受け取ったマップ上の位置がplayerにとって移動可能かどうかを調べ可能ならtrueを返す
    public bool canMove(Vector3 pos)
    {
        if (pos.x < 0 ||
            pos.x >= mapLengthMax.x ||
            pos.y < 0 ||
            pos.y >= mapLengthMax.z)
        {
            Debug.Log("OutOfRange");
            return false;
        }

        int groundName = mapDate.mapNumberDate[(int)pos.y, (int)pos.x];
        if (groundName == (int)MapDate.eGroundName.eDefaultPannel ||
            groundName == (int)MapDate.eGroundName.eWhite ||
            pos == movedPlayerPosList[movedPlayerPosList.Count - 1])
        {
            return true;
        }
        return false;
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

    public Vector3 GetSpriteSize()
    {
        return spriteSize;
    }

    //Gameシーン終了後に配置してあるMapをリセットする
    public void ResetMap()
    {
        MapCreater.Reset();
        footPrinter.Reset();
        Destroy(instanceWhite.gameObject);
        instanceWhite = null;
    }
}
