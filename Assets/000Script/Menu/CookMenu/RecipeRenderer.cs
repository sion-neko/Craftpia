using UnityEngine;

public class RecipeRenderer : MonoBehaviour
{
    private CookItem[] _recipeArray;

    private void Start()
    {
        _recipeArray = GameData.instance.getCookItemDataArray();
    }
    public void PasteRecipe(GameObject cookPanel, Player player)
    {
        CookExeButton.instance.PasteRecipe(cookPanel, player, _recipeArray);

    }

}
