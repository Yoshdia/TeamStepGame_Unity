using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using UnityEngine.UI;

public class InputScreenTouch : MonoBehaviour
{
    bool firstTouched = false;

    [SerializeField]
    GameObject insideObject = null;
    GameObject instanceInside = null;
    [SerializeField]
    GameObject frameObject = null;
    GameObject instanceframe = null;

    Vector3 firstMousePos;
    Vector3 framePos;

    int inputDelay = 0;
    [SerializeField]
    const int InputDelay = 5;

    public string inputKey()
    {
        if (inputDelay <= 0)
        {
            string input = "";

            Touch touch;

            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                if (touch.pressure >= 1.0f)
                {
                    //一番最初に触れられた座標を記憶
                    if (!firstTouched)
                    {
                        //firstMousePos = Input.mousePosition;
                        firstMousePos = touch.position;
                        firstTouched = true;
                        firstMousePos.z = 2.0f;
                        framePos = Camera.main.ScreenToWorldPoint(firstMousePos);
                        instanceframe = Instantiate(frameObject, framePos, new Quaternion());
                        instanceInside = Instantiate(insideObject, framePos, new Quaternion());
                    }

                    //継続的に触れられている座標
                    //Vector3 nowMousePos = Input.mousePosition;
                    Vector3 nowMousePos = touch.position;


                    //二つの座標の差
                    Vector3 firstSubNowPos = firstMousePos - nowMousePos;
                    Vector3 positionDirection = firstSubNowPos.normalized;

                    float x = firstSubNowPos.x;
                    float y = firstSubNowPos.y;

                    if (x > 0.5f || y > 0.5f)
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
                Destroy(instanceframe.gameObject);
                Destroy(instanceInside.gameObject);
            }

            ////長押しされている場合のみ処理を行う
            //if (Input.GetMouseButton(0))
            //{
            //    //一番最初に触れられた座標を記憶
            //    if (!firstTouched)
            //    {
            //        firstMousePos = Input.mousePosition;
            //        firstTouched = true;
            //        firstMousePos.z = 2.0f;
            //        framePos = Camera.main.ScreenToWorldPoint(firstMousePos);
            //        instanceframe = Instantiate(frameObject, framePos, new Quaternion());
            //        instanceInside = Instantiate(insideObject, framePos, new Quaternion());
            //    }



            //    //継続的に触れられている座標
            //    Vector3 nowMousePos = Input.mousePosition;
            //    //二つの座標の差
            //    Vector3 firstSubNowPos = firstMousePos - nowMousePos;

            //    Vector3 positionDirection = firstSubNowPos.normalized;

            //    float x = firstSubNowPos.x;
            //    float y = firstSubNowPos.y;

            //    if (x >=1.0f|| y >=1.0f)
            //    {
            //        //差の値で、xとyどちらが大きいか
            //        string biggerPos = (Math.Abs(x) > Math.Abs(y)) ? "x" : "y";

            //        if (biggerPos == "x")
            //        {
            //            input = x >= 0 ? "left" : "right";
            //        }
            //        else if (biggerPos == "y")
            //        {
            //            input = y >= 0 ? "down" : "up";
            //        }
            //        inputDelay = InputDelay;
            //        //Debug.Log("" + inputDelay);
            //    }
            //}
            //if (!Input.GetMouseButton(0))
            //{
            //    firstTouched = false;
            //    if (instanceframe != null)
            //    {
            //        Destroy(instanceframe.gameObject);
            //        Destroy(instanceInside.gameObject);
            //    }
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

