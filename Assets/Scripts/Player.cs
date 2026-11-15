using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance = null;
    public LayerMask allHeroes;
    public GameObject preBattleUI, battleUI, postBattleUI, playerTiles, enemyTiles;
    public TextMeshProUGUI endResultsText, timerText;
    public bool gameTied;
    public int gameTimer;
    public float remainingTime;
    public List<GameObject> allies, enemies;
    public CharacterClasses selectedCharacter = CharacterClasses.None;
    public List<GameObject> portraits, selectedPortraits;
    public StartButton startButton;
    public bool roundStarted = false;
    public List<GameObject> spells;
    public bool portraitsDisabled = false;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        allies = new();
        enemies = new();
        remainingTime = gameTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if(!roundStarted)
        {
            if (allies.Count == 5 && !startButton.canStart) startButton.Enable(true);
            else if (allies.Count != 5 && startButton.canStart) startButton.Enable(false);
        }
        else
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0) EndRound(2);
            else timerText.text = Mathf.RoundToInt(remainingTime).ToString();
        }
    }

    public void RemoveHero(GameObject hero, CharacterType type)
    {
        if (type == CharacterType.Ally) allies.Remove(hero);
        else enemies.Remove(hero);

        if (allies.Count == 0) EndRound(1);
        else if (enemies.Count == 0) EndRound(0);
    }

    public void StartRound()
    {
        preBattleUI.SetActive(false);
        battleUI.SetActive(true);

        Collider[] foundEnemies = Physics.OverlapSphere(Vector3.zero, 1000, LayerMask.GetMask("Enemies"));
        foreach (Collider enemy in foundEnemies)
        {
            enemy.GetComponent<HeroController>().SetRoundActive(true);
            enemies.Add(enemy.gameObject);
        }

        foreach (GameObject ally in allies)
        {
            ally.GetComponent<HeroController>().SetRoundActive(true);
        }

        foreach(GameObject spell in spells)
        {
            if(spell.GetComponent<Spell>().spell == SpellSelector.instance.selectedSpell)
            {
                spell.SetActive(true);
                break;
            }
        }
        
        playerTiles.SetActive(false);
        enemyTiles.SetActive(false);
        roundStarted = true;
    }

    public void EndRound(int result)
    {
        if (result == 0)
        {
            foreach (GameObject hero in allies) hero.GetComponent<HeroController>().SetRoundActive(false);
            endResultsText.text = "You win!";
        }
        else if (result == 1)
        {
            foreach (GameObject hero in enemies) hero.GetComponent<HeroController>().SetRoundActive(false);
            endResultsText.text = "You lost!";
        }
        else if (result == 2)
        {
            foreach (GameObject hero in allies) hero.GetComponent<HeroController>().SetRoundActive(false);
            foreach (GameObject hero in enemies) hero.GetComponent<HeroController>().SetRoundActive(false);
            endResultsText.text = "It's a tie!";
        }
        
        battleUI.SetActive(false);
        postBattleUI.SetActive(true);
    }

    public void SelectCharacter(CharacterClasses character, GameObject portrait)
    {
        selectedCharacter = character;
        selectedPortraits.Add(portrait);

        if (selectedPortraits.Count == 5) DisableRemainingPortraits();
    }

    public void DeselectCharacter(CharacterClasses character, GameObject portrait)
    {
        selectedPortraits.Remove(portrait);
        if (selectedCharacter == character) selectedCharacter = CharacterClasses.None;

        for (int i = allies.Count - 1; i >= 0; i--)
        {
            GameObject ally = allies[i];
            HeroController hero = ally.GetComponent<HeroController>();

            if (hero.characterClass == character)
            {
                allies.RemoveAt(i);
                hero.initialTile.GetComponent<Tile>().SetOccupied(false);
                Destroy(ally);
                break;
            }
        }

        if (portraitsDisabled && selectedPortraits.Count < 5) EnableRemaingPortraits();
    }

    public void DisableRemainingPortraits()
    {
        foreach(GameObject portrait in portraits)
        {
            if (!selectedPortraits.Contains(portrait)) portrait.GetComponent<Portrait>().DisablePortrait();
        }
        startButton.Enable(true);
        portraitsDisabled = true;
    }

    public void EnableRemaingPortraits()
    {
        foreach (GameObject portrait in portraits)
        {
            if (!selectedPortraits.Contains(portrait)) portrait.GetComponent<Portrait>().EnablePortrait();
        }
        startButton.Enable(false);
        portraitsDisabled = false;
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
