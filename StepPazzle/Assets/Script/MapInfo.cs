using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    //ステージ情報を格納したシングルトン。MapCreaterとPlayerから呼ばれる。
    private int[,] stage = {
        {0,0,0,0,0 },
        {0,1,1,1,0 },
        {0,0,2,0,0 },
        {0,0,1,0,0 },
        {0,0,0,0,0 }
    };

    private static MapInfo mInstance;
    public static MapInfo Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject obj = new GameObject("MapInfo");
                mInstance = obj.AddComponent<MapInfo>();
            }
            return mInstance;
        }
    }

    public int GetStageInfo(int x,int z)
    {
        return stage[x,z];
    }

    public int GetStageSizeWidth()
    {
        return stage.GetLength(1);
    }

    public int GetStageSizeHeight()
    {
        return stage.GetLength(0);
    }
}
