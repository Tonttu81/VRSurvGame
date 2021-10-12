using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<GameObject> objects;

    public List<CraftingRecipe> recipes;

    public bool CheckForRecipes(int objectId)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].objectID[0] == objectId || recipes[i].objectID[1] == objectId)
            {
                print("ojjopjopjop");
                return true;
            }
        }
        return false;
    }
}