using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPositioning : MonoBehaviour
{
    //白い床や壁、スタート地点に設置される、すでに描かれた状態のSpriteのObject
    [SerializeField]
    private GameObject otherPannelObject = null;
    //踏むと変化する役割を持ったObject
    [SerializeField]
    private GameObject pannelObject = null;

    //壁の上に設置される、そこに進むことはできないことを見てわかるようにするためのObject
    [SerializeField]
    private GameObject jammerObject = null;
    //白い床やスタート地点に設置される、何度踏んでも大丈夫なことを見てわかるようにするためのObject
    [SerializeField]
    private GameObject whiteWallObject = null;

    //足跡を生成する
    FootPrint printer=null;
    //MapController、PannelCommonの関数を呼び出すための変数
    MapController haveMapData = null;
    //PannelCommon otherObject = null;

    [HideInInspector]
    public float spriteSizeX = 0;
    [HideInInspector]
    public float spriteSizeZ = 0;

    public void FirstProccess()
    {
        printer = GetComponent<FootPrint>();
        haveMapData = GetComponent<MapController>();
        //otherObject = GetComponent<PannelCommon>();
    }

    public void Positioning()
    {
        //スプライトサイズの設定。GameManagerからステージごとの値を受け取る
        spriteSizeX = 0.68f; spriteSizeZ = 0.69f;
        //足跡を生成するFootPrintにSpriteサイズを渡し、設置位置の基準にさせる
        printer.SetSpriteSize(spriteSizeX, spriteSizeZ);

        //どのSpriteをStageにするか、そのSpriteを配列に保存する
        Sprite[] mapSprite = Resources.LoadAll<Sprite>("Img/FirstImage");
        //配列の引数
        int spriteCnt = 0;

        //MapControllerからマップ情報をint型で取得
        int[,] mapDate = { };
        mapDate = haveMapData.GetMapDate();
        //MapControllerからマップ情報をGameObject型で取得 初期はすべてnull(int型マップ情報の配列と同じサイズの配列)
        GameObject[,] mapObjectDate = { };
        mapObjectDate = haveMapData.GetMapObjectDate();
        //それぞれのObjectサイズをSpriteサイズと等しくさせる
        whiteWallObject.transform.localScale = new Vector3(spriteSizeX, 0.2f, spriteSizeZ);
        jammerObject.transform.localScale = new Vector3(spriteSizeX, 0.2f, spriteSizeZ);


        //MapControllerから配列の最大値やステージ情報を取得し配置する
        for (int z = mapDate.GetLength(0) - 1; z >= 0; z--)
        {
            for (int x = 0; x < mapDate.GetLength(1); x++)
            {
                //パネル情報。MapDateのeGroundNameで命名済み
                int pannelInfo = mapDate[z, x];
                //設置していくオブジェクトの座標や向き
                Vector3 objectPos = new Vector3(x * spriteSizeX, 0, z * spriteSizeZ);
                Quaternion objectQua = new Quaternion(0, 0, 0, 0);
                //マップ情報によって書き換えられるGameObjectを初期化。初期値としてイベントの無い白いブロックを入れる
                GameObject setObject = otherPannelObject;
                GameObject wallObject = null;

                //壁
                if (pannelInfo == (int)MapDate.eGroundName.eWall)
                {
                    setObject = otherPannelObject;
                    objectQua = Quaternion.Euler(0, 0, 0);
                    wallObject = Instantiate(jammerObject, new Vector3(objectPos.x, 0.1f, objectPos.z), objectQua);
                    wallObject.transform.parent = transform;
                }
                //変化前のパネル
                if (pannelInfo == (int)MapDate.eGroundName.eDefaultPannel)
                {
                    setObject = pannelObject;
                }
                //プレイヤーの初期座標またはイベントの無い白いブロック
                if (pannelInfo == (int)MapDate.eGroundName.ePlayerPosition ||
                    pannelInfo == (int)MapDate.eGroundName.eWhite)
                {
                    setObject = otherPannelObject;
                    objectQua = Quaternion.Euler(0, 0, 0);
                    wallObject = Instantiate(whiteWallObject, new Vector3(objectPos.x, 0.1f, objectPos.z), objectQua);
                    wallObject.transform.parent = transform;
                }
                setObject.GetComponent<PannelCommon>().SetSprite(mapSprite[spriteCnt]);

                if (mapSprite.Length != spriteCnt)
                {
                    spriteCnt++;
                }

                //ChangeSpriteを持ったオブジェクトは90度回転させ上を向かせるようにする
                objectQua = Quaternion.Euler(90, 0, 0);

                //オブジェクトを生成
                setObject = Instantiate(setObject, objectPos, objectQua);
                //すべてnullで登録されていたGameObject型のマップ情報の全てにsetObjectを登録していく
                mapObjectDate[z, x] = setObject;
                //このScriptがタッチされているオブジェクトの子にする
                setObject.transform.parent = transform;
                
            }
        }
    }
}
