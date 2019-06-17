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
    public int[,] GetMapDate()
    {
        return mapDateFirst;
    }

    public GameObject[,] GetNullObjectDate()
    {
        return mapObjectDate;
    }

}

