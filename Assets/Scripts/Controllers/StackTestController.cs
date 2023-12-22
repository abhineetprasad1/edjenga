using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTestController : IController
{

    private GameData gameData;

    public StackTestController(GameData gameData)
    {
        this.gameData = gameData;
        GameEvents.OnStackTest += OnStackTestTrigger;
        GameEvents.OnReset += ResetGame;
    }


    void OnStackTestTrigger(Dictionary<string, object> data)
    {
        var selectedStack = gameData.blocksByStack[gameData.activeStack];
        for(int i=0;i < selectedStack.Count; i++)
        {
            var block = selectedStack[i];
            if(block.jBlock.mastery == 0)
            {
                block.Reset();
            }
            else
            {
                block.EnablePhysics(true);
            }
        }
    }

    void ResetGame(Dictionary<string, object> data)
    {
        gameData.state = SessionState.END;
    }

    public void Execute()
    {
        if(gameData.state == SessionState.READY)
        {

        }
    }

    public void Destroy()
    {
        GameEvents.OnStackTest -= OnStackTestTrigger;
        GameEvents.OnReset -= ResetGame;
    }
}
