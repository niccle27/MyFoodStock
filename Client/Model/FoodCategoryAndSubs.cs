using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    /// <summary>
    /// This is a poco in order to load link all the subcategory for one category into one object in order to facilitate de combobox binding
    /// </summary>
    public class FoodCategoryAndSubs
    {
        #region Private Fields

        Dictionary<string, int> subCategory = new Dictionary<string, int>();
        private int id;
        private string name;

        #endregion

        #region Properties

        public Dictionary<string, int> SubCategory
        {
            get => subCategory;
            set => subCategory = value;
        }

        public int Id
        {
            get => id;
            set => id = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        #endregion
    }
}
