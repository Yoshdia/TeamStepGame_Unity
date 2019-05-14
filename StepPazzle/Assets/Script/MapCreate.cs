using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreate : MonoBehaviour
{
    [SerializeField]
    private GameObject pannel = null;
    //パネルすべてを管理するList
    private List<GameObject> pannelList = new List<GameObject>();
    //クリアしたかどうかのフラグ。シーン遷移が実装されたら削除する予定
    private bool clearFlag = false;

    void Start()
    {
        clearFlag = false;
        //ステージの生成、子要素に追加してpannelListに加えていく
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                Vector3 pannelPos = new Vector3(z * 1, 0, x * 1);
                Quaternion pannelQua = new Quaternion();
                GameObject mapPannel = Instantiate(pannel, pannelPos, pannelQua);
                mapPannel.transform.parent = transform;
                pannelList.Add(mapPannel);
                //yield return new WaitForSeconds(0.01f);
            }
        }
        int cnt = 0;
        foreach (GameObject child in pannelList)
        {
            cnt++;
        }
        Debug.Log("List cnt " + cnt);
    }

    void Update()
    {
        //クリアしていない状態なら　全てのpannelが踏まれてないかをチェックする
        if (clearFlag == false)
        {
            bool allPannelChangedFlag = true;
            foreach (GameObject child in pannelList)
            {
                if (child.tag == "defaultPannel")
                {
                    allPannelChangedFlag = false;
                }
            }
            if (allPannelChangedFlag == true)
            {
                clearFlag = true;
                Debug.Log("Clear");
            }
        }
    }
}
