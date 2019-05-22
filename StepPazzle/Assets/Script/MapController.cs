﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MapDateから受け取った情報をもとに、書き直したり返すクラス
public class MapController : MonoBehaviour {

    private int[,] mapDate;

    private void Awake()
    {
        mapDate = GetComponent<MapDate>().GetMapDate();
    }

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    //二次元配列からプレイヤー座標を算出し返す
    public Vector3 GetFirstPositionPlayer()
    {
        Vector3 playerPosition = new Vector3(-1, 1,-1);

        bool checkEnd = false;

        for (playerPosition.z = 0; playerPosition.z < mapDate.GetLength(0); playerPosition.z++)
        {
            for (playerPosition.x = 0; playerPosition.x < mapDate.GetLength(1); playerPosition.x++)
            {
                if (mapDate[(int)playerPosition.z, (int)playerPosition.x] ==(int) MapDate.eGroundName.ePlayerPosition)
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
    public void PlayerMovedChangeMapDate(Vector3 playerPos,Vector3 movePos)
    {
        mapDate[(int)playerPos.z, (int)playerPos.x] = (int)MapDate.eGroundName.eChangedPannel;

        mapDate[(int)(playerPos.z + movePos.z), (int)(playerPos.x + movePos.x)] = (int)MapDate.eGroundName.ePlayerPosition;

    }

    //二次元配列を返す
    public int[,] GetMapDate()
    {
        return mapDate;
    }
}
