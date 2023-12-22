using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightController : IController
{
    private GameData gameData;

    public HighlightController(GameData gameData)
    {
        this.gameData = gameData;
    }

    public void Execute()
    {
        if(gameData.state  == SessionState.READY)
        {

            for(int i =0;i < gameData.allBlocks.Count; i++)
            {
                var block = gameData.allBlocks[i];
                bool shouldHighlight = gameData.highlightedObjects.Contains(block) || block.Equals(gameData.currentlyHoveredBlock);
                block.SetHighlight(shouldHighlight);
            }
        }
    }

    public void Destroy()
    {

    }
}
