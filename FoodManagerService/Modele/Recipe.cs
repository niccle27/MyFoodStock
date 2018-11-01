using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FoodManagerService.Modele
{
    [DataContract]
    public class Recipe
    {
        private int id;
        private string title;
        private string author;
        private string textXml;
        private string imagePath;//TODO pas certain que ce soit la meilleure méthode => à vérifier


        [DataMember]
        public int Id
        {
            get => id;
            set => id = value;
        }
        [DataMember]
        public string Title
        {
            get => title;
            set => title = value;
        }

        [DataMember]
        public string Author
        {
            get => author;
            set => author = value;
        }
        [DataMember]
        public string TextXml
        {
            get => textXml;
            set => textXml = value;
        }
        [DataMember]
        public string ImagePath
        {
            get => imagePath;
            set => imagePath = value;
        }

    }
}