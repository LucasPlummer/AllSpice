
namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
    private readonly FavoritesService _favoriteService;
    private readonly Auth0Provider _auth0provider;

    public FavoritesController(FavoritesService favoriteService, Auth0Provider auth0provider)
    {
        _favoriteService = favoriteService;
        _auth0provider = auth0provider;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Favorite>> Create([FromBody] RecipeMember recipeMemberData)
    {
        try
        {
            Favorite userInfo = await _auth0provider.GetUserInfoAsync<Favorite>(HttpContext);
            recipeMemberData.AccountId = userInfo.Id;
            RecipeMember fav = _favoriteService.Create(recipeMemberData);
            return Ok(fav);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
        try
        {
            Favorite userInfo = await _auth0provider.GetUserInfoAsync<Favorite>(HttpContext);
            string message = _favoriteService.Remove(id, userInfo.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


}
