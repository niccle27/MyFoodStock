using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodManagerService.Modele
{
    interface IUpdatable
    {
        void Update();
    }

    interface IDeletable
    {
        void Delete();
    }
}
