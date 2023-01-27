namespace AllSpice.Repositories;

public class FavoritesRepository
{
    private readonly IDbConnection _db;

    public FavoritesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal RecipeMember Create(RecipeMember recipeMemberData)
    {
        string sql = @"
        INSERT INTO recipeMembers
        (recipeId, accountId)
        VALUES
        (@recipeId, @accountId);
        SELECT LAST_INSERT_Id();
        ";
        int id = _db.ExecuteScalar<int>(sql, recipeMemberData);
        recipeMemberData.Id = id;
        return recipeMemberData;
    }

    internal RecipeMember GetOne(int id)
    {
        string sql = @"
        SELECT
        *
        FROM recipeMembers
        WHERE id = @id;
        ";
        return _db.Query<RecipeMember>(sql, new { id }).FirstOrDefault();
    }

    internal void Remove(int id)
    {
        string sql = @"
        DELETE FROM recipeMembers
        WHERE id = @id;
        ";
        _db.Execute(sql, new { id });
    }

    internal List<MyRecipe> GetMyRecipes(string accountId)
    {
        string sql = @"
        SELECT
        re.*,
        rm.*,
        cr.*
        FROM recipeMembers rm
        JOIN recipes re ON re.id = rm.recipeId
        JOIN accounts cr ON re.creatorId = cr.id
        WHERE rm.accountId = @accountId;
        ";
        List<MyRecipe> myRecipes = _db.Query<MyRecipe, RecipeMember, Account, MyRecipe>(sql, (re, rm, cr) =>
        {
            re.FavoriteId = rm.Id;
            re.Creator = cr;
            return re;
        }, new { accountId }).ToList();
        return myRecipes;
    }
}
