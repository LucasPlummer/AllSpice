
namespace AllSpice.Services;

public class FavoritesService
{
    private readonly FavoritesRepository _repo;
    private readonly RecipesService _recipesService;

    public FavoritesService(FavoritesRepository repo, RecipesService recipesService)
    {
        _repo = repo;
        _recipesService = recipesService;
    }

    internal RecipeMember Create(RecipeMember recipeMemberData)
    {
        Recipe recipe = _recipesService.GetOne(recipeMemberData.RecipeId, recipeMemberData.AccountId);

        RecipeMember fav = _repo.Create(recipeMemberData);

        return fav;
    }



    internal List<MyRecipe> GetMyRecipes(string accountId)
    {
        List<MyRecipe> myRecipes = _repo.GetMyRecipes(accountId);
        return myRecipes;
    }

    internal string Remove(int id, string userId)
    {
        RecipeMember original = _repo.GetOne(id);
        if (original == null)
        {
            throw new Exception("not favorite to Delete at that id");
        }
        if (original.AccountId != userId)
        {
            throw new Exception("Nacho favirote");
        }
        _repo.Remove(id);
        return $"favirote at {id} was removed";
    }
}
