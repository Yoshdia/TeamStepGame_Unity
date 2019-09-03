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
        eThirdStage,
        //eFourthStage,
        //eFifthStage,
        //eSixthStage,
        //eSeventhStage,
        //eEighthStage,
        //eNinthStage,
        //eTenthStage,
    }

    //int[,] mapDateFirst =
    //    {
    //{2,0,0,0,2},
    //{0,0,0,0,2},
    //{0,2,0,0,2},
    //{0,0,0,0,0},
    //{0,0,0,0,0},

    //{2,0,0,0,0},
    //{4,0,0,0,2},
    //{ 0,0,0,0,2}
    //};
    int[,] mapDateSecond =
        {
        {0,2,0,0,0},
        {0,0,0,2,0},
        {4,0,0,0,0},
        {0,0,2,0,0},
        {0,0,2,2,0},

        {0,0,0,0,0},
    };
    //int[,] mapDateThird =
    //{
    //   { 0,1,1,1,},
    //   { 0,0,0,1,},
    //   { 0,0,0,0,},
    //   { 1,0,0,0,},
    //   { 4,0,0,0,},
    //};
    //int[,] mapDateFourth =
    //{
    //    { 0,0,0,0,0 ,0 },
    //    { 0,1,0,0,0 ,0} ,
    //    { 1,4,0,1,0 ,1} ,
    //    { 0,0,0,0,0 ,0} ,
    //    { 0,0,0,0,0 ,0} ,

    //    { 0,0,0,0,0 ,0} ,
    //    { 0,0,1,0,0 ,0} ,
    //};
    //int[,] mapDateFifth =
    //{
    //    { 1,1,0,0,0},
    //    { 1,0,0,4,0},
    //    { 0,0,0,0,1},
    //    { 0,0,0,0,0},
    //    { 1,0,0,0,0},

    //    { 0,0,0,0,1},
    //    { 0,0,0,1,1 } ,
    //};
    int[,] mapDateFirst =
    {
        { 2,0,0,2},
        { 0,0,0,0},
        { 2,0,0,0},
        { 0,0,0,4},
        { 0,0,0,2 },
    };
    //int[,] mapDateSeventh =
    //{
    //    { 1,1,0,0,0 ,1},
    //    { 1,1,0,0,0 ,1},
    //    { 0,0,0,0,1 ,1},
    //    { 0,0,0,0,0 ,1},
    //    { 1,1,0,0,0 ,1},

    //    { 1,4,0,0,0 ,0},
    //    { 1,0,0,0,1 ,0},
    //};
    //int[,] mapDateEighth =
    //{
    //    { 4,0,0,0,0 ,0},
    //    { 0,0,0,0,0 ,0},
    //    { 0,1,0,0,0 ,0},
    //    { 1,0,0,0,0 ,1},
    //    { 0,0,0,0,0 ,0},

    //    { 0,0,1,1,0 ,0},
    //    { 0,0,0,0,0 ,0},
    //    { 0,0,0,0,0 ,0 },
    //};
    //int[,] mapDateNinth =
    //{
    //    { 0,0,0,0,0 ,0},
    //    { 1,0,0,0,0 ,0},
    //    { 0,0,0,0,0 ,0},
    //    { 0,0,0,0,0 ,1},
    //    { 0,0,1,0,0 ,0},

    //    { 0,0,0,0,0 ,0},
    //    { 0,1,0,0,0 ,0},
    //    { 0,0,1,0,0 ,0},
    //};
    int[,] mapDateThird =
    {
        { 0,0,0,0,0 ,0},
        { 0,0,0,0,0 ,0},
        { 0,2,0,0,0 ,0},
        { 2,0,0,0,0 ,2},
        { 0,0,0,0,0 ,2},

        { 0,0,0,0,0 ,0},
        { 0,0,4,0,0 ,0},
        { 0,0,0,2,0 ,0 },
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
    public int[,] GetMapDate(eStageName stage, ref Vector3 pos, ref string fileName, ref string monoChromeFileName, ref Vector3 cameraPos, ref Vector2 mapSize, ref float speed)
    {
        switch (stage)
        {
            case (eStageName.eFirstStage):
                pos = new Vector3(2.00f, 2.56f, 0);
                fileName = "Stage1silhouette";
                cameraPos = new Vector3(2.8f, 5.2f, -11f);
                ResetMap(mapDateFirst);
                mapSize = new Vector2(4.0f, 5.0f);
                speed = 10.0f;
                return mapDateFirst;
                ;
            case (eStageName.eSecondStage):
                pos = new Vector3(1.40f,2.00f,0);
                fileName = "Stage2silhouette";
                cameraPos = new Vector3(2.8f, 5.2f, -11f);
                ResetMap(mapDateSecond);
                mapSize = new Vector2(5,6);
                speed = 7.0f;
                return mapDateSecond;
            case (eStageName.eThirdStage):
                pos = new Vector3(1.16f,1.50f,0);
                fileName = "Stage3silhouette";
                cameraPos = new Vector3(2.8f, 5.2f, -11f);
                ResetMap(mapDateThird);
                mapSize = new Vector2(6,8);
                speed = 6.5f;
                return mapDateThird;
                //case (eStageName.):
                //    pos = new Vector3();
                //    fileName = "";
                //    cameraPos = new Vector3();
                //    ResetMap();
                //    mapSize = new Vector2();
                //    speed = ;
                //    return ;


                //case (eStageName.eSecondStage):
                //    pos = new Vector3(0.8f, 0, 0.6f);
                //    fileName = "flower";
                //    cameraPos = new Vector3(1.5f, 5.5f, 1.25f);
                //    ResetMap(mapDateSecond);
                //    mapSize = new Vector2(5.0f, 5.0f);
                //    speed = 3.0f;
                //    return mapDateSecond;
                //    ;
                //case (eStageName.eDifficultStage):
                //    pos = new Vector3(1.06f, 0.80f, 0);
                //    fileName = "deff";
                //    cameraPos = new Vector3(2.50f, 2.77f, -7.00f);
                //    ResetMap(mapDateThird);
                //    mapSize = new Vector2(6.0f, 8.0f);
                //    speed = 2.0f;
                //    return mapDateThird;
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

