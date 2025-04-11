using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
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
        if(!occupied) SR.sprite = highlighted;
    }

    private void OnMouseExit()
    {
        if(!occupied) SR.sprite = normal;
    }

    private void OnMouseDown()
    {
        if(!occupied && Player.instance.selectedCharacter != CharacterClasses.None)
        {
            Debug.Log("here");
            foreach (GameObject character in characters)
            {
                if(character.GetComponent<HeroController>().characterClass == Player.instance.selectedCharacter)
                {
                    hero = Instantiate(character, transform.position, Quaternion.identity);
                    hero.GetComponent<HeroController>().Init(CharacterType.Ally, gameObject);
                    Player.instance.AddAlly(hero);
                    SetOccupied(true);
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
        }
        else
        {
            SR.sprite = normal;
            this.occupied = false;
        }
    }

}
