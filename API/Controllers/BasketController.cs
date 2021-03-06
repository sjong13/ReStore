using System;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class BasketController : BaseApiController
{
    private readonly StoreContext _context;

    public BasketController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Basket>> GetBasket()
    {
        var basket = await RetrieveBasket();

        return basket == null ? NotFound() : basket;
    }

    [HttpPost]
    public async Task<ActionResult> AddItemToBasket(int productId, int quantity)
    {

        var basket = await this.RetrieveBasket() ?? this.CreateBasket();
        
        var product = await _context.Products.FindAsync(productId);

        if (product == null)
        {
            return NotFound();
        }

        basket.AddItem(product, quantity);

        var result = await _context.SaveChangesAsync() > 0;

        if (result)
        {
            return StatusCode(201);
        }

        return BadRequest(new ProblemDetails {Title = "Problem saving item to basket"});
    }

    [HttpDelete]
    public async Task<ActionResult> RemoveItemFromBasket(int productId, int quantity)
    {
        // get basket
        // remove item or reduce quantity
        // save changes

        return Ok();
    }
    
    private async Task<Basket> RetrieveBasket()
    {
        var basket = await _context.Baskets
            .Include(i => i.Items)
            .ThenInclude(p => p.Product)
            .FirstOrDefaultAsync(x => x.BuyerId.Equals(Request.Cookies["buyerId"]));
        return basket;
    }
    
    private Basket CreateBasket()
    {
        var buyerId = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions {IsEssential = true, Expires = DateTime.Now.AddDays(30)};
        
        Response.Cookies.Append("buyerId", buyerId, cookieOptions);
        
        var basket = new Basket {BuyerId = buyerId};
        
        _context.Baskets.Add(basket);

        return basket;
    }
}