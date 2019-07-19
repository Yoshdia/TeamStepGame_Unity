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
        eSecondStage,
        eDifficultStage
    }

    int[,] mapDateFirst =
        {
    {2,0,0,0,2},
    {0,0,0,0,2},
    {0,2,0,0,2},
    {0,0,0,0,0},
    {0,0,0,0,0},

    {2,0,0,0,0},
    {4,0,0,0,2},
    { 0,0,0,0,2}
    };

    int[,] mapDateSecond =
        {
        {2,0,0,0,0 },
        {2,0,2,2,0 },
        {2,0,4,2,0 },
        {2,2,2,2,0 },
        {0,0,0,0,0 },
    };

    int[,] mapDateThird =
    {
        { 0,0,0,0,0,0},
        { 0,0,0,0,0,0},
        { 0,0,0,0,2,0},
        { 0,0,0,0,2,0},
        { 0,0,0,0,4,0},
        { 0,0,0,0,0,0},
        { 2,0,0,0,0,0},
        { 0,0,2,0,0,0},
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
    public int[,] GetMapDate(eStageName stage, ref Vector3 pos, ref string fileName, ref Vector3 cameraPos)
    {
        switch (stage)
        {
            case (eStageName.eFirstStage):
                pos = new Vector3(1.4f, 1.5f, 0);
                fileName = "SmokeWoman_BH";
                cameraPos = new Vector3(2.8f, 5.2f, -11f);
                ResetMap(mapDateFirst);
                return mapDateFirst;
                ;
            case (eStageName.eSecondStage):
                pos = new Vector3(0.8f, 0, 0.6f);
                fileName = "flower";
                cameraPos = new Vector3(1.5f, 5.5f, 1.25f);
                ResetMap(mapDateSecond);
                return mapDateSecond;
                ;
            case (eStageName.eDifficultStage):
                pos = new Vector3(1.06f, 0.80f, 0);
                fileName = "deff";
                cameraPos = new Vector3(2.50f, 2.77f, -7.00f);
                ResetMap(mapDateThird);
                return mapDateThird;
        }

        return mapDateFirst;
    }

    private void ResetMap(int[,] array)
    {
        for (int num = 0; num < array.GetLength(0); num++)
        {
            for (int x = 0; x < array.GetLength(1); x++)
            {
                if (array[num, x] == (int)eGroundName.eChangedPannel)
                {
                    array[num, x] = (int)eGroundName.eDefaultPannel;
                }

            }
        }
    }

    public GameObject[,] GetNullObjectDate()
    {
        return mapObjectDate;
    }

}

