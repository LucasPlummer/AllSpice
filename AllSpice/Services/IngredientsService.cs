namespace AllSpice.Services;

public class IngredientsService
{
    private readonly IngredientsRepository _repo;

    private readonly RecipesService _recipeService;

    public IngredientsService(IngredientsRepository repo, RecipesService recipeService)
    {
        _repo = repo;
        _recipeService = recipeService;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        Ingredient ing = _repo.Create(ingredientData);
        return ing;
    }

    internal List<Ingredient> GetIngredientsByRecipe(int recipeId, string userId)
    {
        Recipe recipe = _recipeService.GetOne(recipeId, userId);
        List<Ingredient> ingredients = _repo.GetIngredientsByRecipe(recipeId);
        return ingredients;
    }

    internal string Remove(int id, string userId)
    {
        Ingredient original = _repo.GetOne(id);
        if (original == null)
        {
            throw new Exception("No ingredient to delete that id!");
        }
        if (original.CreatorId != userId)
        {
            throw new Exception("Not your ingredients!");
        }
        _repo.Remove(id);
        return $"Ingredient at {id} was removed";
    }
}
