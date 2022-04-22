using System.Collections;
using System.Collections.Generic;
using GameSystem;
using UnityEngine;

public class Game
{
    private Block[,] blockMap;
    private PhysicsEngine physicsEngine;
    private List<IPhysicalObject> physicalObjects;

    public Game(int[,] blockIDMap)
    {
        this.physicsEngine = new PhysicsEngine();
        var length = blockIDMap.GetLength(0);
        var height = blockIDMap.GetLength(1);
        blockMap = new Block[length, height];

        for (int x = 0; x < blockIDMap.GetLength(0); x++)
        {
            for (int y = 0; y < blockIDMap.GetLength(1); y++)
            {
                var blockID = blockIDMap[x, y];
                if (blockID == 0)
                {
                    //..
                }
                else
                {
                    var block = new Block(blockID, x, y);
                    blockMap[x, y] = block;
                    physicalObjects.Add(block);
                }
            }
        }
    }

    public void Update(float deltaTime)
    {
        physicsEngine.Update(deltaTime, physicalObjects);
    }
}
