
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float step = 2f;
    // 入力受付時、移動先の位置を算出して保存 
    Vector3 target;
    // 何らかの理由で移動できなかった場合、元の位置に戻すため移動前の位置を保存
    Vector3 prevPos;

    //これから移動するpannelのサイズ
    [SerializeField]
    private GameObject pannel = null;
    //移動する距離。pannelにタッチされたオブジェクトのサイズをここに保存する
    private float movePannelSize;

    void Start()
    {
        //目的座標をリセット
        target = transform.position;
        //アタッチされたオブジェクトのサイズを所得
        movePannelSize = pannel.GetComponent<MeshRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        // ① 移動中かどうかの判定。移動中でなければ入力を受付
        if (transform.position == target)
        {
            SetTargetPosition();
        }
        Move();
    }

    // ② 入力に応じて移動後の位置を算出
    void SetTargetPosition()
    {
        //移動先に壁があるか
        bool targetPositionNoWallFlag = false;

        prevPos = target;

        Vector3 moveVector = new Vector3(0, 0, 0);
        //斜め移動を防ぐためのフラグ
        bool oneBottonInputFlag = false;

        if (Input.GetKey(KeyCode.RightArrow) && oneBottonInputFlag == false)
        {
            moveVector.x = +movePannelSize;
            oneBottonInputFlag = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && oneBottonInputFlag == false)
        {
            moveVector.x = -movePannelSize;
            oneBottonInputFlag = true;
        }
        if (Input.GetKey(KeyCode.UpArrow) && oneBottonInputFlag == false)
        {
            moveVector.z = +movePannelSize;
            oneBottonInputFlag = true;
        }
        if (Input.GetKey(KeyCode.DownArrow) && oneBottonInputFlag == false)
        {
            moveVector.z = -movePannelSize;
            oneBottonInputFlag = true;
        }

        targetPositionNoWallFlag = Physics.Raycast(transform.position, moveVector, 1);

        if (targetPositionNoWallFlag == false)
        {
            target = transform.position + moveVector;
        }

    }

    // ③ 目的地へ移動する
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, step * Time.deltaTime);
    }

}
