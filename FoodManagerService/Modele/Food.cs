using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FoodManagerService.Modele
{
    [DataContract]
    public class Food 
    {
        private string _name;
        private int _quantity;
        private DateTime _expirationDate;
        private int _price;
        private string _unit;
        private int _id;
        private int _idCategory;
        private int _idSubCategory;



        public Food()
        {
            
        }
        [DataMember]
        public int Id { get => _id; set => _id = value; }
        [DataMember]
        public int IdCategory { get => _idCategory; set => _idCategory = value; }
        [DataMember]
        public int IdSubCategory { get => _idSubCategory; set => _idSubCategory = value; }
        [DataMember]
        public String Name { get => _name; set => _name = value; }
        [DataMember]
        public int Quantity { get => _quantity; set => _quantity = value; }
        [DataMember]
        public DateTime ExpirationDate { get => _expirationDate; set => _expirationDate = value; }
        [DataMember]
        public int Price { get => _price; set => _price = value; }
        [DataMember]
        public String Unit { get => _unit; set => _unit = value; }
    }
}