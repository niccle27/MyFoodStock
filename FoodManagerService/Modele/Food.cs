using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodManagerService.Modele
{
    public class Food:IUpdatable,IDeletable
    {
        private string _name;
        private int _quantity;
        private DateTime _expirationDate;
        private int _price;
        private string _unit;

        public Food(string name, int quantity, DateTime expirationDate, int price, string unit)
        {
            Name = name;
            Quantity = quantity;
            ExpirationDate = expirationDate;
            Price = price;
            Unit = unit;
        }

        enum Category
        {
            Pasta,
            Vegetable,
            Fruit,
            DairyProduct,
            Meat,
            Fish,
            Sauce,
            Drink
        }

        public String Name { get => _name; set => _name = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public DateTime ExpirationDate { get => _expirationDate; set => _expirationDate = value; }
        public int Price { get => _price; set => _price = value; }
        public String Unit { get => _unit; set => _unit = value; }
        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}