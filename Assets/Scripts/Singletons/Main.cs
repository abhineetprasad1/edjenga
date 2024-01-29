using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{

    private static Main instance;
    public static Main Instance {
        get { return instance; }
    }

    private GameData gameData;
    private SessionController sessionController;

    public CinemachineVirtualCameraBase orbitalCamera;

    private void Awake()
    {
        if(Main.Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            GameServices.Initialize();
           
        }
    }


    public GameData GetGameData()
    {
        return gameData;
    }


    // Start is called before the first frame update
    void Start()
    {
        Play();
    }

    public void Play()
    {
        gameData = ServiceLocator.GetService<GameDataService>().LoadGame();
        sessionController = new SessionController(gameData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        sessionController.Destroy();
        sessionController = null;
    }

}
