using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace API.Entities;

public class Basket
{
    public int Id { get; set; }

    public string BuyerId { get; set; }

    public List<BasketItem> Items { get; set; } = new();

    public void AddItem(Product product, int quantity)
    {
        if (Items.All(item => item.ProductId != product.Id))
        {
            Items.Add(new BasketItem{Product = product, Quantity = quantity});
        }

        var existingItem = Items.FirstOrDefault(item => item.ProductId.Equals(product.Id));

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
    }

    public void RemoveItem(int productId, int quantity)
    {
        var item = Items.FirstOrDefault(item => item.Id.Equals(productId));

        if (item == null)
        {
            return;
        }

        item.Quantity -= quantity;

        if (item.Quantity.Equals(0))
        {
            Items.Remove(item);
        }
    }
}

