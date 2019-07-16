using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class InputScreenTouch : MonoBehaviour
{
    bool firstTouched = false;

    Vector3 firstMousePos;

    public string inputKey()
    {
        string input = "";

        //Touch touch= Input.GetTouch(0);
        //長押しされている場合のみ処理を行う
        //if (Input.GetMouseButton(0))
        foreach (Touch touch in Input.touches)
        {
            if (Input.touchCount > 0)
            {
                //一番最初に触れられた座標を記憶
                if (!firstTouched)
                {
                    //firstMousePos = Input.mousePosition;
                    firstMousePos = touch.position;
                    firstTouched = true;
                }

                //継続的に触れられている座標
                //Vector3 nowMousePos = Input.mousePosition;
                Vector3 nowMousePos = touch.position;

                //二つの座標の差
                //Vector3 firstSubNowPos = firstMousePos - nowMousePos;
                Vector3 firstSubNowPos = firstMousePos - nowMousePos;

                float x = firstSubNowPos.x;
                float y = firstSubNowPos.y;

                if (x != 0 || y != 0)
                {


                    //差の値で、xとyどちらが大きいか
                    string biggerPos = (Math.Abs(x) > Math.Abs(y)) ? "x" : "y";

                    if (biggerPos == "x")
                    {
                        input = x >= 0 ? "left" : "right";
                    }
                    else if (biggerPos == "y")
                    {
                        input = y >= 0 ? "down" : "up";
                    }
                }


            }
            else
            {
                firstTouched = false;
            }

        }
        return input;
    }
}

