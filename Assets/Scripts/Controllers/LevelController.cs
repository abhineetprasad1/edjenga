using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelController : IController
{

    private GameData gameData;

    public LevelController(GameData gameData)
    {
        this.gameData = gameData;
        PopulateLevelData();
    }

    async Task<List<JBlock>> FetchData()
    {
        var remoteDataService = ServiceLocator.GetService<RemoteDataService>();
        List<JBlock> blockContainer = await remoteDataService.GetRemoteData<JBlock>(Constants.API_URL);
        Debug.Log("dataList " + blockContainer.Count);
        return blockContainer;
    }

    async void PopulateLevelData()
    {
        //fetch all block data
        var blockContainer = await FetchData();

        //sort data into stacks
        Dictionary<string, List<JBlock>> jStacks = gameData.jStacks;
        
        for(int i =0;i < blockContainer.Count; i++)
        {
            var jblock = blockContainer[i];
            var grade = jblock.grade;

            if(!jStacks.TryGetValue(grade, out List<JBlock> blocks))
            {
                blocks = new List<JBlock>();
                jStacks[grade] = blocks;
                gameData.totalStacks++;
            }

            blocks.Add(jblock);
        }

        //sort the list
        foreach(var kvp in gameData.jStacks)
        {
            var stack = kvp.Value;

            stack.Sort(new JBlockComparer());
        }


        gameData.state = SessionState.POPULATE;
    }

    void PopulateLevelObjects()
    {
        var origin = Vector3.zero;
        foreach (var kvp in gameData.jStacks)
        {
            var grade = kvp.Key;
            var stack = kvp.Value;


            //create ground
            var ground = ObjectUtils.CreateGround(origin);
            gameData.stackBases.Add(ground.GetComponent<GroundViewController>());

            var blocksList = new List<JBlockViewController>();
            for(int i =0;i < stack.Count; i++)
            {
                //create block
                int rowIndex = i / 3;
                bool isOddRow = (rowIndex % 2) == 1;

                int colIndex = i % 3;
                var pos = new Vector3(origin.x, origin.y + rowIndex * gameData.blockSize.y , origin.z + (colIndex-1) * (gameData.blockSize.z + gameData.blockGap.z));

                if (isOddRow)
                {
                    pos = new Vector3(origin.x + (colIndex - 1) * (gameData.blockSize.z + gameData.blockGap.z), origin.y + rowIndex * gameData.blockSize.y, origin.z);
                }

                var go = ObjectUtils.CreateJBlock(pos, isOddRow?new Vector3(0,90,0): Vector3.zero,stack[i], ground.transform);
                var blockViewController = go.GetComponent<JBlockViewController>();
                gameData.allBlocks.Add(blockViewController);
                blocksList.Add(blockViewController);

            }
            gameData.blocksByStack.Add(blocksList);

            var gradeLabel = ObjectUtils.CreateGradeLabel(origin + new Vector3(2.5f, 0, 0f), new Vector3(0,-90,0),grade, ground.transform);
            gameData.gradeLabels.Add(gradeLabel);


            origin = origin + new Vector3(0, 0, gameData.groundSize + gameData.groundGap);

        }
    }

    public void Execute()
    {
        if(gameData.state == SessionState.POPULATE)
        {
            PopulateLevelObjects();
            gameData.state = SessionState.READY;
        }

        

    }

    public void Destroy()
    {

    }
}
