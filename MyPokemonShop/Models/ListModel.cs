using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPokemonShop.Models
{
    public class ListModel
    {
        public List<ListItem> List {get; set;}
        public ListModel()
        {
            List = new List<ListItem>();

            

        }

        /// <summary>
        /// Выборка данных о покупателях и их покупках
        /// </summary>
        public void FillModel()
        {
            //в зависимости от трактовки задания:
            //если нужно отображать только последнее событие для каждого покупателя и общее количество предыдущих его покупок - 1 вариант
            //если нужно отображать в ленте все события каждого покупателя с указанием их номера для покупателя - 2 вариант
            
            using (var context = new PokemonShopDatabaseModelContainer())
            {
                //1 вариант
                //List = context.CustomerSet.Join(context.OrderSet, x => x.Email, y => y.Customer.Email, (x, y) => new { Name = x.Name, Email = x.Email, Date = y.Date }).GroupBy(x => x.Email).Select(x => new ListItem() { Name = x.FirstOrDefault().Name, Count = x.Count(), Date = x.OrderByDescending(y => y.Date).FirstOrDefault().Date }).OrderByDescending(x => x.Date).ToList();

                //2 вариант
                List = context.CustomerSet.Join(context.OrderSet, x => x.Email, y => y.Customer.Email, (x, y) => new { Name = x.Name, Email = x.Email, Date = y.Date, Id = y.Id })
                    .Select(x => new ListItem() { Name = x.Name, Date = x.Date, Count = context.OrderSet.Where(z => z.Customer.Email == x.Email && z.Date <= x.Date).Count() }).OrderByDescending(x => x.Date).ToList();
            }
        }
    }

    public class ListItem
    {
        public string Name {get; set;}
        public int Count {get;set;}
        public DateTime Date { get; set; }
    }
}