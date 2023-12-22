using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionStateController : IController
{
    private GameData gameData;

    public SessionStateController(GameData gameData)
    {
        this.gameData = gameData;
        GameEvents.OnStateChanged += OnSessionStateChanged;
    }

    void OnSessionStateChanged(Dictionary<string, object> data)
    {
        SessionState state = (SessionState)data["state"];

        switch (state)
        {
            case SessionState.LOAD:
                break;
            case SessionState.POPULATE:
                break;
            case SessionState.READY:
                break;
            case SessionState.END:
                End();
                break;
        }

    }

    void End()
    {
        Main.Instance.EndGame();

        ServiceLocator.GetService<MonoService>().DelayedCallByFrame(3, () =>
        {
            Main.Instance.Play();
        });
    }

    public void Execute()
    {

    }

    public void Destroy()
    {
        GameEvents.OnStateChanged -= OnSessionStateChanged;
    }
}
