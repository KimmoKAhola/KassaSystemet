using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KassaSystemet.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            DateTime timeOfPurchase = DateTime.Now;
            Purchases = new();
        }
        public List<Purchase> Purchases { get; set; }

        public decimal Pay() => 0m;
        private void AddPurchase(List<Purchase> shoppingCart)
        {
            (int id, decimal amount) = UserInputHandler.ProductInput();
            if (amount > 100)
                Console.WriteLine($"You can not purchase more than {100} of a product!", ConsoleColor.Red);
            else if (ProductCatalogue.Instance.Products.ContainsKey(id))
                shoppingCart.Add(new Purchase(id, amount));
            else
                Console.WriteLine($"No product with id {id} exist in the system.", ConsoleColor.Red);
        }
    }
}
