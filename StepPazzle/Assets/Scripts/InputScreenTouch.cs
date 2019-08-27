﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class InputScreenTouch : MonoBehaviour
{
    bool firstTouched = false;

    //[SerializeField]
    //GameObject insideObject = null;
    //GameObject instanceInside = null;
    //[SerializeField]
    //GameObject frameObject = null;
    //GameObject instanceframe = null;

    Vector3 firstMousePos;

    int inputDelay = 0;
    [SerializeField]
    const int InputDelay = 5;

    public string inputKey()
    {
        if (inputDelay <= 0)
        {
            string input = "";

            //長押しされている場合のみ処理を行う
            //if (Input.GetMouseButton(0))
            //foreach (Touch touch in Input.touches)
            Touch touch;

            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector2 insidePosition;
                touch = Input.GetTouch(i);
                if (touch.pressure >= 1.0f)
                {
                    //一番最初に触れられた座標を記憶
                    if (!firstTouched)
                    {
                        //firstMousePos = Input.mousePosition;
                        firstMousePos = touch.position;
                        firstTouched = true;
                        insidePosition = firstMousePos;
                        //instanceframe = Instantiate(frameObject, insidePosition, new Quaternion());
                        //instanceInside = Instantiate(insideObject, insidePosition, new Quaternion());
                    }

                    //継続的に触れられている座標
                    //Vector3 nowMousePos = Input.mousePosition;
                    Vector3 nowMousePos = touch.position;


                    //二つの座標の差
                    Vector3 firstSubNowPos = firstMousePos - nowMousePos;

                    Vector3 positionDirection = firstSubNowPos.normalized;

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
                        inputDelay = InputDelay;
                        Debug.Log("" + inputDelay);
                    }


                }
            }
            if (Input.touchCount < 1)
            {
                firstTouched = false;
            }


            //else
            //{
            //    firstTouched = false;
            //}
            return input;
        }
        else
        {
            inputDelay--;
        }
        return "";
    }
}

