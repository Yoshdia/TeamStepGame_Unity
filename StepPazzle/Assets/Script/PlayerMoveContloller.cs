using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveContloller : MonoBehaviour
{

    // 移動速度
    [SerializeField]
    private float speed = 5;
    // 入力受付時、移動先の位置を算出して保存 
    private Vector3 targetPos;
    //移動方向ベクトル
    private Vector3 moveVector;

    //地面のオブジェクトをここに保存
    [SerializeField]
    private GameObject pannel = null;
    //移動する距離。pannelにタッチされたオブジェクトのサイズをここに保存する
    private float movePannelSize;
    //マップデータ、MapDateからマップ情報を受け取る
    private int[,] mapDate ;

    void Start()
    {
        mapDate = GetComponent<MapController>().GetMapDate();
        //プレイヤーの初期座標を受け取り入れる
        Vector2 playerPos = GetComponent<MapController>().GetFirstPositionPlayer();
        playerPos.y = movePannelSize;
        transform.position = playerPos;

        //移動方向ベクトルの初期化
        moveVector = new Vector3(0, 0, 0);
        //目的座標をリセット
        targetPos = transform.position;
        //アタッチされたオブジェクト、地面のパネルのサイズを所得
        movePannelSize = pannel.GetComponent<MeshRenderer>().bounds.size.x;
    }

    void Update()
    {
        //移動中かどうかの判定。移動中でなければ入力を受付
        if (transform.position == targetPos)
        {
            SetTargetPosition();
            //移動先に壁がない場合targetPosを更新させる
            if (TargetPositionHaveWall() == false)
            {
                targetPos = transform.position + moveVector;
            }
        }
        Move();
    }


    //入力に応じて移動方向を代入
    private void SetTargetPosition()
    {
        moveVector = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVector.x = +movePannelSize;
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVector.x = -movePannelSize;
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVector.z = +movePannelSize;
            return;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVector.z = -movePannelSize;
            return;
        }
    }

    //移動先に壁があるか(ある場合trueを返す
    bool TargetPositionHaveWall()
    {
        bool targetPositionNoWallFlag = false;


        ////自分の移動先にレイを飛ばし障害物を検知する
        //targetPositionNoWallFlag = Physics.Raycast(transform.position, moveVector, 1);
        //int info = mapList[(((playerZ + (int)moveVector.z) * stageWidth) + (playerX + (int)moveVector.x))];

        return targetPositionNoWallFlag;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

}

