using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    PlayerMoveContloller player = null;
    [SerializeField]
    MapController stageCreater = null;
    [SerializeField]
    Image blackScreen = null;

    //[SerializeField]
    //GameObject playerObj = null;
    //[SerializeField]
    //GameObject stageObj = null;

    enum gameState
    {
        Start,
        Game,
        Clear,
        Result
    }

    gameState state;

    private void Start()
    {
        //state = gameState.Game;
        state = gameState.Start;
        //player.Reset();
        //blackScreen.color = new Color(0, 0, 0, 0.0f);
        blackScreen.color = new Color(0, 0, 0, 0.8f);

    }

    //// Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (gameState.Start):
                if(Input.GetKey(KeyCode.Return))
                {
                    StateChange();
                }
                break;
            case (gameState.Game):
                //stageControllerから、そのステージをクリアしたかどうかのフラグを受け取る[クリア=true]
                bool updateStop = false;
                updateStop = stageCreater.ClearCheck();
                //プレイヤーの更新が終了する条件。「ゲームクリア」かつ「移動の完了」
                bool playerUpdateEnd = (updateStop && player.CheckMovingEnd());
                playerUpdateEnd = false;
                if (playerUpdateEnd == false)
                {
                    player.UpdateInGame();
                }
                break;
            case (gameState.Clear):
                break;
            case (gameState.Result):
                break;
        }
    }

    private void StateChange()
    {
        switch (state)
        {
            case (gameState.Start):
                state = gameState.Game;
                blackScreen.color = new Color(0,0,0,0);

                stageCreater = Instantiate(stageCreater);
                stageCreater.InitProcces();
                stageCreater.MapReset();
                
                //playerの初期化に必要な処理数
                player = Instantiate(player);
                player.haveMapDateObject = stageCreater;
                player.Reset();

                break;
            case (gameState.Game):
                state = gameState.Clear;
                break;
            case (gameState.Clear):
                state = gameState.Result;
                break;
            case (gameState.Result):

                break;
        }
    }
}

