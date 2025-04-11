using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    public static SpellSelector instance;
    public SpellType selectedSpell;
    public GameObject selectedButton;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectNewSpell(GameObject newSpell)
    {
        selectedButton.GetComponent<SpellButton>().DeselectSpell();
        selectedButton = newSpell;
        selectedSpell = newSpell.GetComponent<SpellButton>().type;
    }
}
