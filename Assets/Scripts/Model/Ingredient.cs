using System;

namespace Model
{
    [Serializable]
    public class Ingredient
    {
        public string ingredient;
        public bool isCut;

        public Ingredient(string ingredient, bool isCut)
        {
            this.ingredient = ingredient;
            this.isCut = isCut;
        }
    }
}