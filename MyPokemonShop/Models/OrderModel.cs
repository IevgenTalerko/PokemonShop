using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPokemonShop.Models
{
    public class OrderModel
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [RegularExpression(@"^\+\d{2}\(\d{3}\)\d{3}-\d{2}-\d{2}$", ErrorMessage="Phone number should match this pattern +00(000)000-00-00")]
        [Required(ErrorMessage = "Enter your phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Enter your email")]
        [RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage="You've entered incorrect email")]
        public string Email { get; set; }


        /// <summary>
        /// сохранение данных о покупателе и его покупке
        /// </summary>
        public void Save()
        {
            using (var context = new PokemonShopDatabaseModelContainer())
            {
                Order order = new Order();
                order.Date = DateTime.Now;
                order.Id = Guid.NewGuid();

                var customer = context.CustomerSet.Where(x => x.Email == Email).FirstOrDefault();
                if (customer == null)
                {
                    customer = new Customer();
                    customer.Email = Email;
                    customer.Phone = Phone;
                    customer.Name = Name;

                    context.CustomerSet.Add(customer);
                }
                
                customer.Order.Add(order);

                context.SaveChanges();
            }
        }
    }
}