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
    private Vector3 moveVectorOnScene;
    //移動方向ベクトルをSpriteサイズで割った値。xyz全てに1と0しか入らない
    private Vector3 moveVectorOnMap;

    //[SerializeField]
    //private GameObject pannel = null;
    //移動する距離。pannelにのサイズをここに保存し移動距離に使用
    //private float movePannelSize;

    //MapDateがコンポーネントされているGameObject
    [SerializeField]
    private GameObject haveMapDateObject=null;
    //マップデータ上のPlayerの位置
    private Vector3 playerPosOnMapDate;

    private float moveSpriteSizeX;
    private float moveSpriteSizeZ;

    void Start()
    {
        moveSpriteSizeX = haveMapDateObject.GetComponent<MapPositioning>().spriteSizeX;
        moveSpriteSizeZ = haveMapDateObject.GetComponent<MapPositioning>().spriteSizeZ;

        //アタッチされたオブジェクト、地面のパネルのサイズを所得
        //movePannelSize = pannel.GetComponent<MeshRenderer>().bounds.size.x;

        //プレイヤーの初期座標を受け取り入れる
        playerPosOnMapDate = haveMapDateObject.GetComponent<MapController>().GetFirstPositionPlayer();
        transform.position = new Vector3(playerPosOnMapDate.x*moveSpriteSizeX, 1, playerPosOnMapDate.z*moveSpriteSizeZ);

        //移動方向ベクトルの初期化
        moveVectorOnScene = new Vector3(0, 0, 0);
        moveVectorOnMap = new Vector3(0, 0, 0);

        //目的座標をリセット
        targetPos = transform.position;
    }

    void Update()
    {
        bool moving = true;
        //移動中かどうかの判定。移動中でなければ入力を受付
        if (transform.position == targetPos)
        {
            moving = false;
            SetTargetPosition();
            //移動先に壁がない場合targetPosを更新させる
            if (TargetPositionHaveWall() == false)
            {
                targetPos = transform.position + moveVectorOnScene;
            }
        }


        if (transform.position != targetPos)
        {
            if(moving==false)
            {
                haveMapDateObject.GetComponent<MapController>().PlayerdMovedChangeMapDate(playerPosOnMapDate, moveVectorOnMap);
                playerPosOnMapDate += moveVectorOnMap;
            }
            Move();
        }
    }


    //入力に応じて移動方向を代入
    private void SetTargetPosition()
    {
        moveVectorOnScene = new Vector3(0, 0, 0);
        moveVectorOnMap = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVectorOnScene.x = +moveSpriteSizeX;
            moveVectorOnMap.x += 1;
            return;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVectorOnScene.x = -moveSpriteSizeX;
            moveVectorOnMap.x -= 1;
            return;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveVectorOnScene.z = +moveSpriteSizeZ;
            moveVectorOnMap.z += 1;
            return;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveVectorOnScene.z = -moveSpriteSizeZ;
            moveVectorOnMap.z -= 1;
            return;
        }
    }

    //移動先が移動可能か
    bool TargetPositionHaveWall()
    {
        bool targetPositionNoWallFlag = false;

        int targetPosZ = (int)(playerPosOnMapDate.z + (moveVectorOnScene.z/moveSpriteSizeZ));
        int targetPosX = (int)(playerPosOnMapDate.x + (moveVectorOnScene.x/moveSpriteSizeX));

        //Debug.Log("" +targetPosZ+":"+targetPosX);
        //壁だった場合移動できない
        if (haveMapDateObject.GetComponent<MapController>().GetNumberOnMap(targetPosZ, targetPosX) == (int)MapDate.eGroundName.eWall)
        {
            targetPositionNoWallFlag = true;
        }

        return targetPositionNoWallFlag;
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {

    }
}

