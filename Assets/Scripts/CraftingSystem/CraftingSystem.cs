using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public List<GameObject> objects;

    public List<CraftingRecipe> recipes;

    public GameObject CheckForRecipes(int objectId, int targetObjectId)
    {
        for (int i = 0; i < recipes.Count; i++)
        {
            if (recipes[i].objectID[0] == objectId && recipes[i].objectID[1] == targetObjectId)
            {
                return recipes[i].result;
            }
            else if (recipes[i].objectID[1] == objectId && recipes[i].objectID[0] == targetObjectId)
            {
                return recipes[i].result;
            }
        }
        return null;
    }
}