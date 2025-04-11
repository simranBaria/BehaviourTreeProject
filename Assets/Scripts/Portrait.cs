using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Portrait : MonoBehaviour
{
    public CharacterClasses character;
    public GameObject select, disable;
    public bool selected = false, disabled = false;
    public TextMeshProUGUI classText;

    // Start is called before the first frame update
    void Start()
    {
        switch(character)
        {
            case CharacterClasses.Marksman:
                classText.text = "Marksman";
                break;

            case CharacterClasses.Tank:
                classText.text = "Tank";
                break;

            case CharacterClasses.Rogue:
                classText.text = "Rogue";
                break;

            case CharacterClasses.Mage:
                classText.text = "Mage";
                break;

            case CharacterClasses.Warrior:
                classText.text = "Warrior";
                break;

            case CharacterClasses.Support:
                classText.text = "Support";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPortrait()
    {
        if (!disabled)
        {
            if (!selected)
            {
                select.SetActive(true);
                Player.instance.SelectCharacter(character, gameObject);
                selected = true;
            }
            else
            {
                select.SetActive(false);
                Player.instance.DeselectCharacter(character, gameObject);
                selected = false;
            }
        }
    }

    public void DisablePortrait()
    {
        disable.SetActive(true);
        disabled = true;
    }
}
