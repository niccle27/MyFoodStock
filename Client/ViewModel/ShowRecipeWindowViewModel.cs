using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Client.ViewModel
{
    public class ShowRecipeWindowViewModel
    {
        /// <summary>
        /// hydrate everything from xml 
        /// </summary>
        /// <param name="textXml"></param>
        public ShowRecipeWindowViewModel(string textXml)
        {
            XElement recipe;
            try
            {
                recipe = XElement.Parse(textXml);
                _description = recipe.Descendants("Description").First().Value;
                _duration = recipe.Descendants("Duration").First().Value;
                _listIngredients = (from ingredient in recipe.Descendants("Ingredient") select ingredient.Value).ToList();
                _listSteps = (from step in recipe.Descendants("Step") select step.Value).Select((value, index) => new KeyValuePair<int, string>(index, value)).ToDictionary(t => t.Key, t => t.Value);
            }
            catch (System.Xml.XmlException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private string _description;
        private string _duration;
        List<string> _listIngredients;
        Dictionary<int, string> _listSteps;

        public string Description
        {
            get => _description;
            set => _description = value;
        }

        public string Duration
        {
            get => _duration;
            set => _duration = value;
        }

        public List<string> ListIngredients
        {
            get => _listIngredients;
            set => _listIngredients = value;
        }

        public Dictionary<int, string> ListSteps
        {
            get => _listSteps;
            set => _listSteps = value;
        }
    }
}
