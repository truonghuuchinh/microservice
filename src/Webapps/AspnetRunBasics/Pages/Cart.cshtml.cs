using System;
using System.Linq;
using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        private readonly IBasketService _basket;

        public CartModel(IBasketService basket)
        {
            _basket = basket;
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basket.GetBasket("thChinh");

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "thChinh";
            var basket = await _basket.GetBasket(userName);
            var itemRemove = basket.Items.FirstOrDefault(x => x.ProductId == productId);
            basket.Items.Remove(itemRemove);

            await _basket.UpdateBasket(basket);
            return RedirectToPage();
        }
    }
}