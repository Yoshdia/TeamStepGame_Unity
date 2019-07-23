using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    MapDate.eStageName stageName =0;

    [SerializeField]
    PlayerMoveContloller player = null;
    [SerializeField]
    MapController stageCreater = null;
    [SerializeField]
    FootPrint footPrinter = null;
    [SerializeField]
    GameObject mainCamera = null;
    [SerializeField]
    EffectSpawner effectSpawner = null;
    [SerializeField]
    EffectSpawner effectSpawner2 = null;

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
        state = gameState.Start;

        stageCreater = Instantiate(stageCreater);
        stageCreater.transform.parent = transform;

        player = Instantiate(player);
        player.transform.parent = transform;

        footPrinter = Instantiate(footPrinter);
        footPrinter.transform.parent = transform;

        effectSpawner = Instantiate(effectSpawner);
        effectSpawner.transform.parent = transform;

        effectSpawner2 = Instantiate(effectSpawner2);
        effectSpawner2.transform.parent = transform;

        stageCreater.footPrinter = footPrinter;
        stageCreater.effectSpawner = effectSpawner;
        stageCreater.effectSpawner2 = effectSpawner2;

        player.haveMapDateObject = stageCreater;
    }

    //// Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case (gameState.Start):
                if (Input.GetKey(KeyCode.Return))
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
                //playerUpdateEnd = false;
                if (playerUpdateEnd == false)
                {
                    player.UpdateInGame();
                }
                else
                {
                    StateChange();
                }
                break;
            case (gameState.Clear):
                if(Input.GetKey(KeyCode.RightShift))
                {
                    StateChange();
                }
                break;
            case (gameState.Result):
                if(Input.GetKey(KeyCode.LeftShift))
                {

                StateChange();
                }
                break;
        }
    }

    //stateを変える関数
    private void StateChange()
    {
        switch (state)
        {
            //StartからGameへ
            case (gameState.Start):
                state = gameState.Game;

                GameStart();

                break;
            //GameからClearへ
            case (gameState.Game):
                state = gameState.Clear;

                GameEnd();
                
                break;
            case (gameState.Clear):
                state = gameState.Result;
                break;
            case (gameState.Result):
                state = gameState.Start;
                stageName = MapDate.eStageName.eDifficultStage;
                break;
        }
    }

    private void GameStart()
    {
        footPrinter.FirstProccess();
        effectSpawner.FirstProcces();
        effectSpawner2.FirstProcces();
        stageCreater.InitProcces();

        Vector3 cameraPos = new Vector3();

        stageCreater.MapReset(stageName, ref cameraPos);

        mainCamera.transform.position = cameraPos;
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        player.Reset();
    }

    private void GameEnd()
    {
        stageCreater.ResetMap();
        player.GoOutScreen();
    }

    public void Retry()
    {
        GameEnd();
        GameStart();
    }
}

