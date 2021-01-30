using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase_Behavior : MonoBehaviour
{
    //Skelly Transform (Contains location of G.0.)
    public Transform skelly_player;
    //Speed of goblin
    public float speed;

    //Actual vector locations of Skelly and the Goblin
    private Vector2 skelly_loc;
    private Vector2 goblin_loc;

    // Start is called before the first frame update
    void Awake()
    {
        goblin_loc = gameObject.transform.position;
        skelly_loc = skelly_player.position;
    }

    // Update is called once per frame
    void Update()
    {
        float goblin_move = speed * Time.deltaTime;

        //moving goblin towards player
        transform.position = Vector2.MoveTowards(transform.position, skelly_loc, goblin_move);

        skelly_loc = skelly_player.position;
    }
}
