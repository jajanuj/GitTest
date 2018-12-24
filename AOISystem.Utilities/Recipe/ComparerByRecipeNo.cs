using System.Collections;

namespace AOISystem.Utilities.Recipe
{
    public class ComparerByRecipeNo : IComparer
    {
        public int Compare(object recipe1, object recipe2)
        {
            return ((RecipeInfo)recipe1).RecipeNo.CompareTo(((RecipeInfo)recipe2).RecipeNo);
        }
    }
}
