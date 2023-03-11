using System.Threading.Tasks;
using AspnetRunBasics.Models;
using AspnetRunBasics.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class CheckOutModel : PageModel
    {
        private readonly IBasketService _basketService;

        public CheckOutModel(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [BindProperty]
        public BasketCheckoutModel  Order { get; set; }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            Cart = await _basketService.GetBasket("thChinh");
            return Page();
        }

        public async Task<IActionResult> OnPostCheckOutAsync()
        {
            Cart = await _basketService.GetBasket("thChinh");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            Order.UserName = "thChinh";
            Order.TotalPrice = Cart.TotalPrice;

            await _basketService.CheckoutBasket(Order);
            
            return RedirectToPage("Confirmation", "OrderSubmitted");
        }       
    }
}