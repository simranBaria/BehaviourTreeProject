using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyTiles;
    public List<GameObject> tiles;
    public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform tile in enemyTiles.transform)
        {
            tiles.Add(tile.gameObject);
        }


        for(int i = 1; i <= 5; i++)
        {
            GameObject randomTile = tiles[Random.Range(0, tiles.Count - 1)];
            tiles.Remove(randomTile);

            GameObject randomEnemy = enemies[Random.Range(0, enemies.Count)];
            randomTile.GetComponent<Tile>().SpawnEnemy(randomEnemy);
            enemies.Remove(randomEnemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
