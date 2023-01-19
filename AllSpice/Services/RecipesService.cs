namespace AllSpice.Services;


public class RecipesService
{
    private readonly RecipesRepository _repo;

    public RecipesService(RecipesRepository repo)
    {
        _repo = repo;
    }


    internal Recipe Create(Recipe recipeData)
    {
        Recipe recipe = _repo.Create(recipeData);
        return recipe;
    }

    internal List<Recipe> Get()
    {
        List<Recipe> recipes = _repo.Get();
        return recipes;
    }

    internal Recipe GetOne(int id, string userId)
    {
        Recipe recipe = _repo.GetOne(id);
        if (recipe == null)
        {
            throw new Exception("No recipe at that id");
        }
        if (recipe.CreatorId != userId)
        {
            throw new Exception("You don't own that!");
        }
        return recipe;
    }

    internal string Remove(int id, string userId)
    {
        Recipe original = GetOne(id, userId);
        if (original.CreatorId != userId)
        {
            throw new Exception("not your recipe!");
        }

        _repo.Remove(id);
        return $"{original.Title} has been EXTERMINATED!";
    }

    internal Recipe Update(Recipe recipeUpdate, int id, string userId)
    {
        Recipe original = GetOne(id, userId);
        original.Title = recipeUpdate.Title ?? original.Title;
        original.Instructions = recipeUpdate.Instructions ?? original.Instructions;
        original.Img = recipeUpdate.Img ?? original.Img;
        original.Category = recipeUpdate.Category ?? original.Category;

        bool edited = _repo.Update(original);
        if (edited == false)
        {
            throw new Exception("recipe was not edited");
        }
        return original;
    }
}
