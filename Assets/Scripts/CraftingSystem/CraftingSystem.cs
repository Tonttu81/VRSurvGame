using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<GameObject> objects; // Lista kaikista pelin objekteista/tavaroista

    public List<CraftingRecipe> recipes; // Lista kaikista crafting recipeistä

    // Recipe systeemi toimii siten, että jokainen recipe koostuu kahdesta objektista.

    public GameObject CheckForRecipes(int objectId, int targetObjectId) // Tämä kutsutaan attachableobject scriptissä attachobject metodissa eli kun kaksi objektia yhdistetään
    {
        for (int i = 0; i < recipes.Count; i++) // Käy kaikki recipet läpi
        {
            if (recipes[i].objectID[0] == objectId && recipes[i].objectID[1] == targetObjectId) // Jos pelaajan yhdistämät objektit kuuluvat crafting recipeen, palauttaa oikean recipen
            {
                return recipes[i].result;
            }
            // Tarkistaa myös objektit toisinpäin, jos pelaajan yhdistämät objektit kuuluvat recipeen mutta ovat väärinpäin
            else if (recipes[i].objectID[1] == objectId && recipes[i].objectID[0] == targetObjectId) 
            {
                return recipes[i].result;
            }
        }
        return null; // Jos objekteille ei löydy recipeä, ei palauta mitään
    }
}