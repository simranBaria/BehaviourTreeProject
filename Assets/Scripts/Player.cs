using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public LayerMask allHeroes;
    public GameObject preBattleUI, battleUI;
    public List<GameObject> allies, enemies;
    public CharacterClasses selectedCharacter = CharacterClasses.None;
    public List<GameObject> portraits, selectedPortraits;
    public StartButton startButton;
    public bool roundStarted = false;
    public List<GameObject> spells;

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
        if(!roundStarted)
        {
            if (allies.Count == 5 && !startButton.canStart) startButton.Enable(true);
            else if (allies.Count != 5 && startButton.canStart) startButton.Enable(false);
        }
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
        Collider[] foundEnemies = Physics.OverlapSphere(Vector3.zero, 1000, LayerMask.GetMask("Enemies"));
        foreach (Collider enemy in foundEnemies)
        {
            enemy.GetComponent<HeroController>().SetRoundActive(true);
            enemies.Add(enemy.gameObject);
        }

        foreach (GameObject ally in allies) ally.GetComponent<HeroController>().SetRoundActive(true);

        foreach(GameObject spell in spells)
        {
            if(spell.GetComponent<Spell>().spell == SpellSelector.instance.selectedSpell)
            {
                spell.SetActive(true);
                break;
            }
        }

        preBattleUI.SetActive(false);
        battleUI.SetActive(true);
    }

    public void EndRound(CharacterType winningTeam)
    {
        if (winningTeam == CharacterType.Ally) foreach (GameObject hero in allies) hero.GetComponent<HeroController>().SetRoundActive(false);
        else foreach (GameObject hero in enemies) hero.GetComponent<HeroController>().SetRoundActive(false);
    }

    public void SelectCharacter(CharacterClasses character, GameObject portrait)
    {
        selectedCharacter = character;
        selectedPortraits.Add(portrait);

        if (selectedPortraits.Count == 5) DisableRemainingPortraits();
    }

    public void DeselectCharacter(CharacterClasses character, GameObject portrait)
    {
        foreach(GameObject ally in allies)
        {
            if(ally.GetComponent<HeroController>().characterClass == character)
            {
                allies.Remove(ally);
                ally.GetComponent<HeroController>().initialTile.GetComponent<Tile>().SetOccupied(false);
                Destroy(ally);
                selectedPortraits.Remove(portrait);
                selectedCharacter = CharacterClasses.None;
                break;
            }
        }
    }

    public void DisableRemainingPortraits()
    {
        foreach(GameObject portrait in portraits)
        {
            if (!selectedPortraits.Contains(portrait)) portrait.GetComponent<Portrait>().DisablePortrait();
        }
        startButton.Enable(true);
    }

    public void AddAlly(GameObject ally)
    {
        allies.Add(ally);
    }
}

public enum CharacterClasses
{
    None,
    Marksman,
    Tank,
    Rogue,
    Warrior,
    Mage,
    Support
}
