using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrint : MonoBehaviour
{
    //設置される足跡のObject、数をInspectorで指定できる
    [SerializeField]
    private GameObject[] foot = null;

    //Object全てを管理する
    //ActiveでないObjectが格納されている配列
    private GameObject[] waitingFoot;
    //ActiveのObject。配列から足跡を受け取った時にアクティブにさせここに格納する
    private List<GameObject> activeFoot;
    //配列のサイズ
    private static int footNum = 50;

    //MapControllerからSpriteSizeを取得
    private float spriteSizeX = 0;
    private float spriteSizeZ = 0;

    //乱数を取得するため
    private System.Random random;

    private void Start()
    {
        //乱数所得
        random = new System.Random(foot.Length - 1);
        //配列にfootNum個のFootObjectを非Active状態で生成しておく
        waitingFoot = new GameObject[footNum];
        for (int num = 0; num < waitingFoot.Length; num++)
        {
            int range = random.Next(foot.Length);

            waitingFoot[num] = Instantiate(foot[range]);
            waitingFoot[num].transform.parent = transform;
            waitingFoot[num].SetActive(false);
        }

        activeFoot = new List<GameObject>();
    }

    //MapPositioning.Positioning()から呼ばれspriteSizeを受け取り保存する。
    public void SetSpriteSize(float x, float z)
    {
        spriteSizeX = x;
        spriteSizeZ = z;
    }

    public void SetFoot(Vector3 pos, Vector3 direction)
    {
        //そのままfootPosに足跡を設置すると進んでいない場所にはみ出してしまうため0.5ずらす
        Vector3 disPlaceVec = direction;
        disPlaceVec /= 2.0f;
        //元の座標にdisPlaceVecを足し0.5ずらさせる
        pos += disPlaceVec;
        Vector3 footPos = new Vector3(pos.x * spriteSizeX, 0.2f, pos.z * spriteSizeZ);
        //進んでる方向に足跡を回転させ更にy軸に90°回転させる
        direction.y = 90;
        Quaternion qua = Quaternion.LookRotation(direction);

        for (int num = 0; num < waitingFoot.Length; num++)
        {
            GameObject footObj = waitingFoot[num];
            if (footObj != null)
            {
                //アクティブにする
                footObj.SetActive(true);
                //座標、角度をセット
                footObj.transform.position = footPos;
                footObj.transform.rotation = qua;

                //配列からリストへ渡す
                activeFoot.Add(footObj);
                waitingFoot[num] = null;
                break;
            }
        }
    }

    public void DeleteOneFoot()
    {
        //画面から消したいObjectを非Activeにしリストから配列に渡す
        GameObject footObj = activeFoot[activeFoot.Count - 1];
        footObj.SetActive(false);

        for (int num = 0; num < waitingFoot.Length; num++)
        {
            if (!waitingFoot[num])
            {
                waitingFoot[num] = footObj;
                activeFoot.RemoveAt(activeFoot.Count - 1);
                break;
            }
        }
    }
}
