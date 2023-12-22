using System.Collections.Generic;
using UnityEngine;

public class GameData
{

    //PLAYER DATA


    //SESSION DATA
    private SessionState _state = SessionState.LOAD;
    public SessionState state
    {
        get { return _state; }
        set {
            _state = value;
            var data = new Dictionary<string, object>();
            data["state"] = _state;
            GameEvents.OnStateChanged(data);
        }
    }

    public Dictionary<string, List<JBlock>> jStacks = new Dictionary<string, List<JBlock>>();
    public List<GroundViewController> stackBases = new List<GroundViewController>();
    public List<GradeLabelViewController> gradeLabels = new List<GradeLabelViewController>();
    public int activeStack = -1;
    public int totalStacks;

    public bool goToNextStack = false;
    public bool goToPrevStack = false;


    public List<List<JBlockViewController>> blocksByStack = new List<List<JBlockViewController>>();
    public JBlockViewController currentlyHoveredBlock = null;
    public List<JBlockViewController> highlightedObjects = new List<JBlockViewController>();
    public List<JBlockViewController> allBlocks = new List<JBlockViewController>();

    //CONFIG DATA
    public float cameraHeight = 0.7f;
    public float groundSize = 6.0f;
    public float groundGap = 1.0f;
    public Vector3 blockSize = new Vector3(1.5f, 0.3f, 0.45f);
    public Vector3 blockGap = new Vector3(0.05f, 0.0f, 0.05f);
    public float sensitivity = 3.0f;
}
