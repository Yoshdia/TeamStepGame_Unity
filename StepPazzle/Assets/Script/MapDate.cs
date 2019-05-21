using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDate : MonoBehaviour
{
    public enum groundName
    {
        eDefaultPannel = 0,
        eChangedPannel = 1,
        eWall = 2,
        ePlayerDefaultPosition = 3
    }

    int[,] mapDate =
        {
        {0,2,0,0,0,2 },
        {0,0,0,2,0,2 },
        {0,0,0,0,0,2 },
        {0,2,2,3,0,2 },
        {0,0,0,0,0,0 }
    };

    public int GetMapDate(int x, int y)
    {
        return mapDate[y, x];
    }

    private void Start()
    {
        GetPlayerPos();
    }

    //二次元配列からプレイヤー座標を算出
    public Vector2 GetPlayerPos()
    {
        Vector2 playerPosition = new Vector2(-1,-1);

        bool checkEnd = false;

        for (playerPosition.y = 0; playerPosition.y < mapDate.GetLength(0); playerPosition.y++)
        {
            for (playerPosition.x= 0; playerPosition.x < mapDate.GetLength(1); playerPosition.x++)
            {
                if (mapDate[(int)playerPosition.y , (int)playerPosition.x] == 3)
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

    //二次元配列を返す
    public int[,] GetStageDate()
    {
        return mapDate;
    }


}
