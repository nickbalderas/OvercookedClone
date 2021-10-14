using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Recipe
    {
        public string id;
        public string name;
        public List<Ingredient> ingredients;
    }
}
