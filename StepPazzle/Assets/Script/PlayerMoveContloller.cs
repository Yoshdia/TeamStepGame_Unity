using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveContloller : MonoBehaviour {

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

    private int[,] mapDate;

    void Start () {
        mapDate=GetComponent<MapDate>().GetStageDate();
	}
	
	void Update () {
		
	}
}
