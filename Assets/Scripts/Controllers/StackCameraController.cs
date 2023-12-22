using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class StackCameraController : IController
{
    private GameData gameData;

    CinemachineVirtualCameraBase orbitalCamera;
   

    public StackCameraController(GameData gameData) {
        this.gameData = gameData;
        orbitalCamera = Main.Instance.orbitalCamera;
       
        ((CinemachineFreeLook)orbitalCamera).m_YAxis.Value = gameData.cameraHeight;

    }

    
    public void Execute()
    {
      if(gameData.state == SessionState.READY)
        {
            bool shouldUpdateCamera = false;
            if (gameData.stackBases.Count > 0 && gameData.activeStack == -1)
            {
                gameData.activeStack = 0;
                shouldUpdateCamera = true;            
            }

            if (gameData.goToNextStack)
            {
                gameData.activeStack = (gameData.activeStack + 1) % gameData.totalStacks;
                shouldUpdateCamera = true;
                gameData.goToNextStack = false;
            }

            if (gameData.goToPrevStack)
            {
                gameData.activeStack = (gameData.activeStack - 1) % gameData.totalStacks;
                if(gameData.activeStack < 0)
                {
                    gameData.activeStack = gameData.totalStacks + gameData.activeStack;
                }
                shouldUpdateCamera = true;
                gameData.goToPrevStack = false;
            }

            if (shouldUpdateCamera)
            {
                orbitalCamera.Follow = gameData.stackBases[gameData.activeStack].transform;
                orbitalCamera.LookAt = gameData.stackBases[gameData.activeStack].transform;
                ((CinemachineFreeLook)orbitalCamera).m_YAxis.Value = gameData.cameraHeight;
            }
        }
 
    }

    public void Destroy()
    {

    }
}
