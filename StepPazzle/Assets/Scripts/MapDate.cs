using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDate : MonoBehaviour
{

    public enum eGroundName
    {
        eDefaultPannel = 0,
        eChangedPannel = 1,
        eWall = 2,
        eWhite = 3,
        ePlayerPosition = 4
    }

    public enum eStageName
    {
        eFirstStage,
        eSecondStage
    }


    int[,] mapDateFirst =
        {
    {2,0,0,0,0},
    {0,0,0,0,2},
    {0,2,0,0,2},
    {0,0,0,0,0},
    {0,0,0,0,0},

    {2,0,0,0,0},
    {4,0,0,0,2}

    };

    int[,] mapDateSecond =
        {
        {2,0,0,0,0 },
        {2,0,2,2,0 },
        {2,0,4,2,0 },
        {2,2,2,2,0 },
        {0,0,0,0,0 },
    };



    GameObject[,] mapObjectDate =
    {
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},

        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null},
        {null, null,null,null,null, null,null}
    };

    //二次元配列を返す
    public int[,] GetMapDate(eStageName stage)
    {
        switch (stage)
        {
            case (eStageName.eFirstStage):
                return mapDateFirst;
                ;
            case (eStageName.eSecondStage):
                return mapDateSecond;
                ;
        }

        return mapDateFirst;
    }

    public Vector3 GetSpriteSize(eStageName stage)
    {
        Vector3 sprite=new Vector3();

        switch (stage)
        {
            case (eStageName.eFirstStage):
                sprite = new Vector3(0.68f,0, 0.69f);
                break;
            case (eStageName.eSecondStage):
                sprite = new Vector3(0.8f,0,0.6f);
                break;
        }
        return sprite;

    }

    public GameObject[,] GetNullObjectDate()
    {
        return mapObjectDate;
    }

}

