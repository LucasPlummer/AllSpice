namespace AllSpice.Repositories;

public class IngredientsRepository
{
    private readonly IDbConnection _db;

    public IngredientsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Ingredient Create(Ingredient ingredientData)
    {
        string sql = @"
    INSERT INTO ingredients
    (quantity, name, recipeId, creatorId)
    VALUES
    (@quantity, @name, @recipeId, @creatorId);
    SELECT LAST_INSERT_ID();
    ";
        int id = _db.ExecuteScalar<int>(sql, ingredientData);
        ingredientData.Id = id;
        return ingredientData;
    }


    internal Ingredient GetOne(int id)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients 
        WHERE id = @id;
        ";
        return _db.Query<Ingredient>(sql, new { id }).FirstOrDefault();
    }
    internal List<Ingredient> GetIngredientsByRecipe(int recipeId)
    {
        string sql = @"
        SELECT
        *
        FROM ingredients 
        WHERE recipeId = @recipeId;
        ";
        return _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    }

    internal void Remove(int id)
    {
        string sql = @"
        DELETE FROM ingredients
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }
}

