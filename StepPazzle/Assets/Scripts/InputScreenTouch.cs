﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputScreenTouch : MonoBehaviour
{
    bool firstTouched = false;

    Vector3 firstMousePos ;

    public string inputKey()
    {
        string input = "";

        //長押しされている場合のみ処理を行う
        if (Input.GetMouseButton(0))
        {
            //一番最初に触れられた座標を記憶
            if (!firstTouched)
            {
                firstMousePos = Input.mousePosition;
                firstTouched = true;
            }

            //継続的に触れられている座標
            Vector3 nowMousePos = Input.mousePosition;

            //二つの座標の差
            Vector3 firstSubNowPos = firstMousePos - nowMousePos;

            float x = firstSubNowPos.x;
            float y = firstSubNowPos.y;

            if(x!=0||y!=0)
            {


            //差の値で、xとyどちらが大きいか
            string biggerPos=(Math.Abs(x) > Math.Abs(y)) ? "x" : "y";

            if(biggerPos=="x")
            {
                input = x >= 0 ? "left" : "right";
            }
            else if(biggerPos=="y")
            {
                input = y >= 0 ? "down" : "up";
            }
            }


        }
        else
        {
            firstTouched = false;
        }

        return input;
    }
}

