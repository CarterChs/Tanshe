using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        game = new Game(new int[1, 1]);
    }

    // Update is called once per frame
    void Update()
    {
        game.Update(Time.deltaTime);
    }
}
