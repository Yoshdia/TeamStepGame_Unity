﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 移動速度
    [SerializeField]
    private float speed = 5;
    // 入力受付時、移動先の位置を算出して保存 
    private Vector3 targetPos;
    //移動方向ベクトル
    private Vector3 moveVector;

    //これから移動するpannelのサイズ
    [SerializeField]
    private GameObject pannel = null;
    //移動する距離。pannelにタッチされたオブジェクトのサイズをここに保存する
    private float movePannelSize;
    //ステージ情報、Playerの初期座標をここから入手する
    private MapInfo mapInfo;


    private List<int> mapList = new List<int>();

    //下４つの変数はまだ実装途中
    //private int stageHeight;
    private int stageWidth;

    int playerX;
    int playerZ;

    void Start()
    {
        //マップ情報に応じてPlayerの初期座標を変更する
        mapInfo = MapInfo.Instance;
        mapList = mapInfo.GetStageList();
        Vector3 playerPos = new Vector3(0, 0, 0);
        //MapInfoよりプレイヤーの位置となる2の位置を受け取りそこを初期地点にさせる
        playerX = mapInfo.GetplayerPositionX();
        playerZ = mapInfo.GetplayerPositionZ();
        //初期地点に移動させる
        playerPos = new Vector3(playerX, 1, playerZ);
        transform.position = playerPos;

        //この2文は気にしないこと
        //stageHeight = mapInfo.GetStageHeight();
        stageWidth = mapInfo.GetstageWidth();

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
                //この3文も気にしない
                playerX += (int)moveVector.x;
                playerZ += (int)moveVector.z;
                //Debug.Log("x: y:" + playerX + playerZ);


            }
        }
        Move();
    }

    //入力に応じて移動方向を代入
    void SetTargetPosition()
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
        //自分の移動先にレイを飛ばし障害物を検知する
        targetPositionNoWallFlag = Physics.Raycast(transform.position, moveVector, 1);

        int info = mapList[(((playerZ + (int)moveVector.z) * stageWidth) + (playerX + (int)moveVector.x))];

        if(info==1)
        {

        }
            Debug.Log(""+info);

        return targetPositionNoWallFlag;
    }

    //目的地へ移動する
    void Move()
    {
        //target座標へspeedの速さで進む
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

}
