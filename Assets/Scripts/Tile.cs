using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool enemyTile, gameStarted;
    public bool placeable = false;
    public Sprite normal, highlighted;
    public SpriteRenderer SR;
    public bool occupied = false;
    public List<GameObject> characters;
    public GameObject hero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        if (enemyTile || gameStarted) return;
        if (!occupied)
        {
            SR.sprite = highlighted;
            placeable = true;
        }
    }

    private void OnMouseExit()
    {
        if (enemyTile || gameStarted) return;
        if (!occupied)
        {
            SR.sprite = normal;
            placeable = false;
        }
    }

    private void OnMouseDown()
    {
        if(placeable && Player.instance.selectedCharacter != CharacterClasses.None)
        {
            foreach (GameObject character in characters)
            {
                if(character.GetComponent<HeroController>().characterClass == Player.instance.selectedCharacter)
                {
                    hero = Instantiate(character, transform.position, Quaternion.identity);
                    hero.GetComponent<HeroController>().Init(CharacterType.Ally, gameObject);
                    Player.instance.AddAlly(hero);
                    SetOccupied(true);

                    BattlePortraitManager.instance.AddPortrait(hero.GetComponent<HeroController>());
                    Player.instance.selectedCharacter = CharacterClasses.None;
                }
            }
        }
    }

    public void SpawnEnemy(GameObject hero)
    {
        if(!occupied)
        {
            this.hero = Instantiate(hero, transform.position, Quaternion.identity);
            this.hero.GetComponent<HeroController>().Init(CharacterType.Enemy, gameObject);
            SetOccupied(true);
        }
    }

    public void SetOccupied(bool occupied)
    {
        if (occupied)
        {
            SR.sprite = highlighted;
            this.occupied = true;
            placeable = false;
        }
        else
        {
            SR.sprite = normal;
            this.occupied = false;
        }
    }

    public void Disable()
    {
        SR.sprite = normal;
        gameStarted = true;
    }

}
