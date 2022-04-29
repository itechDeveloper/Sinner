using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject item;
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void SpawnDroppedItem()
    {
        Vector2 playerpos = new Vector2(player.position.x, player.position.y + 3);
        Instantiate(item, playerpos, Quaternion.identity);
    }
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
