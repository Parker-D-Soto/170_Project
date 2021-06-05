using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PTBTChase : MonoBehaviour
{
    //Skelly Transform (Contains location of G.0.)
    //private Transform skelly_player = GameObject.FindGameObjectWithTag("Player").transform;
    //Speed of goblin
    public float speed;
    public GoblinAttack dmg;
    public Animator anim;

    //Actual vector locations of Skelly and the Goblin
    private Vector2 skelly_loc;
    private Vector2 goblin_loc;

    // Start is called before the first frame update
    void Awake()
    {

        goblin_loc = gameObject.transform.position;

        if(GameObject.FindGameObjectWithTag("Player") != null)
        {
            skelly_loc = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!dmg.Attacking)
        {
            anim.SetBool("Up", false);
            anim.SetBool("Left", false);
            anim.SetBool("Down", false);
            anim.SetBool("Right", false);
            float goblin_move = speed * Time.deltaTime;


            //moving goblin towards player
            transform.position = Vector2.MoveTowards(transform.position, skelly_loc, goblin_move);
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                skelly_loc = GameObject.FindGameObjectWithTag("Player").transform.position;
            }

            Vector2 directionHelp = new Vector2(skelly_loc.x - transform.position.x, skelly_loc.y - transform.position.y);

            if (Mathf.Abs(directionHelp.x) > Mathf.Abs(directionHelp.y))
            {
                anim.SetBool("Side", true);
                if (directionHelp.x < 0)
                {
                    anim.SetBool("Left", true);
                }
                else
                {
                    anim.SetBool("Right", true);
                }
            }
            else
            {
                if (directionHelp.y < 0)
                {
                    anim.SetBool("Down", true);
                }
                else
                {
                    anim.SetBool("Up", true);
                }
            }
        }
        
    }
}
