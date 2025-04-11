using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellButton : MonoBehaviour
{
    public GameObject select;
    public bool selected;
    public SpellType type;
    public TextMeshProUGUI spellText;

    // Start is called before the first frame update
    void Start()
    {
        switch(type)
        {
            case SpellType.Blazing:
                spellText.text = "Blazing";
                break;

            case SpellType.Freezing:
                spellText.text = "Freezing";
                break;

            case SpellType.Healing:
                spellText.text = "Healing";
                break;

            case SpellType.Shielding:
                spellText.text = "Shielding";
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectSpell()
    {
        if(!selected)
        {
            selected = true;
            select.SetActive(true);
            SpellSelector.instance.SelectNewSpell(gameObject);
        }
    }

    public void DeselectSpell()
    {
        selected = false;
        select.SetActive(false);
    }
}
