using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public LayerMask allHeroes;
    public GameObject preBattleUI;
    public List<GameObject> allies, enemies;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        allies = new();
        enemies = new();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHero(GameObject hero, CharacterType type)
    {
        if (type == CharacterType.Ally) allies.Remove(hero);
        else enemies.Remove(hero);

        if (allies.Count == 0) EndRound(CharacterType.Enemy);
        else if (enemies.Count == 0) EndRound(CharacterType.Ally);
    }

    public void StartRound()
    {
        Collider[] foundAllies = Physics.OverlapSphere(Vector3.zero, 1000, LayerMask.GetMask("Allies"));
        foreach (Collider ally in foundAllies)
        {
            ally.GetComponent<HeroController>().SetRoundActive(true);
            allies.Add(ally.gameObject);
        }

        Collider[] foundEnemies = Physics.OverlapSphere(Vector3.zero, 1000, LayerMask.GetMask("Enemies"));
        foreach (Collider enemy in foundEnemies)
        {
            enemy.GetComponent<HeroController>().SetRoundActive(true);
            enemies.Add(enemy.gameObject);
        }


        preBattleUI.SetActive(false);
    }

    public void EndRound(CharacterType winningTeam)
    {
        if (winningTeam == CharacterType.Ally) foreach (GameObject hero in allies) hero.GetComponent<HeroController>().SetRoundActive(false);
        else foreach (GameObject hero in enemies) hero.GetComponent<HeroController>().SetRoundActive(false);
    }
}
