using System.Security.Cryptography.X509Certificates;

namespace CheckRegister.Models;

/**************************************************************************************************************************************************
* This class provides the data structures necessary to allow for the categorization of transactions which will eventually allow for the tracking 
* and analysis of spending habits.  The structure is not fully utilized do to the lack of a useable UI for the feature.  
*
*
*
****************************************************************************************************************************************************/
public class CategoryNode
{


    // Lists all of the categories that can be tracked. 

    public enum Category
    {
        Housing,
        Utilities,
        Food,
        Dining,
        MedicalInsurance,
        Insurance,
        InvestmentIncome,
        Wages,
        Reimbursement,
        OtherIncome,
        OtherExpense,
        Uncategorized,
        Income,
        Root,
        Rent,
        Mortgage,
        HouseRepair,
        VehicleRepair,
        Electricity,
        Sewer,
        GasUtility,
        Water,
        Groceries,
        Snacks,
        Candy,
        Desert,
        FoodStaples,
        FastFood,
        Restaurant,
        HouseInsurance,
        VehicleInsurance,
        Vehicle,
        LifeInsurance,
        Employment,
        Transportation,
        Benefits,
        OtherHousing,
        OtherUtilities,
        OtherFood,
        OtherInsurance,
        Fuel,
        OtherTransportation,
        Medical
    }

    //Creates a CategoryNode object to allow for a tree-like category structure. 

    public Category CatName { get; set; }
    public List<CategoryNode> Children { get; set; } = [];

    CategoryNode(Category catName)
    {
        CatName = catName;
    }

    //Creates the root node which is the final structure that will categorize transactions and allow for
    //data analysis.
    public static CategoryNode BuildNode()
    {

        CategoryNode Root = new CategoryNode(Category.Root);
        CategoryNode Housing = new CategoryNode(Category.Housing);
        CategoryNode Rent = new CategoryNode(Category.Rent);
        CategoryNode Mortgage = new CategoryNode(Category.Mortgage);
        CategoryNode HouseRepair = new CategoryNode(Category.HouseRepair);

        CategoryNode Utilities = new CategoryNode(Category.Utilities);
        CategoryNode Electricity = new CategoryNode(Category.Electricity);
        CategoryNode Sewer = new CategoryNode(Category.Sewer);
        CategoryNode Gas = new CategoryNode(Category.GasUtility);
        CategoryNode Water = new CategoryNode(Category.Water);

        CategoryNode Food = new CategoryNode(Category.Food);
        CategoryNode Groceries = new CategoryNode(Category.Groceries);
        CategoryNode Dining = new CategoryNode(Category.Dining);
        CategoryNode Snacks = new CategoryNode(Category.Snacks);
        CategoryNode Candy = new CategoryNode(Category.Candy);
        CategoryNode Desert = new CategoryNode(Category.Desert);
        CategoryNode Staples = new CategoryNode(Category.FoodStaples);
        CategoryNode FastFood = new CategoryNode(Category.FastFood);
        CategoryNode Restaurant = new CategoryNode(Category.Restaurant);

        CategoryNode Insurance = new CategoryNode(Category.Insurance);
        CategoryNode MedicalInsurance = new CategoryNode(Category.MedicalInsurance);
        CategoryNode HouseInsurance = new CategoryNode(Category.HouseInsurance);
        CategoryNode VehicleInsurance = new CategoryNode(Category.VehicleInsurance);
        CategoryNode LifeInsurance = new CategoryNode(Category.LifeInsurance);

        CategoryNode InvestmentIncome = new CategoryNode(Category.InvestmentIncome);
        CategoryNode Income = new CategoryNode(Category.Income);
        CategoryNode Employment = new CategoryNode(Category.Employment);
        CategoryNode Benefits = new CategoryNode(Category.Benefits);

        CategoryNode OtherHousing = new CategoryNode(Category.OtherHousing);
        CategoryNode OtherUtilities = new CategoryNode(Category.OtherUtilities);
        CategoryNode OtherFood = new CategoryNode(Category.OtherFood);
        CategoryNode OtherInsurance = new CategoryNode(Category.OtherInsurance);
        CategoryNode OtherIncome = new CategoryNode(Category.OtherIncome);

        CategoryNode Transportation = new CategoryNode(Category.Transportation);
        CategoryNode Vehicle = new CategoryNode(Category.Vehicle);
        CategoryNode Fuel = new CategoryNode(Category.Fuel);
        CategoryNode VehicleRepair = new CategoryNode(Category.VehicleRepair);
        CategoryNode OtherTransportation = new CategoryNode(Category.OtherTransportation);
        CategoryNode Medical = new CategoryNode(Category.Medical);
        CategoryNode Uncategorized = new CategoryNode(Category.Uncategorized);
        CategoryNode Wages = new CategoryNode(Category.Wages);
        CategoryNode OtherExpense = new CategoryNode(Category.OtherExpense);
        CategoryNode Reimbursement = new CategoryNode(Category.Reimbursement);

        Root.Children.AddRange(
                    Uncategorized,
            Medical,
            Housing,
            Utilities,
            Food,
            Insurance,
            Income,
            Transportation,
            OtherExpense
        );
        Housing.Children.AddRange(OtherHousing, Rent, Mortgage, HouseRepair);
        Utilities.Children.AddRange(OtherUtilities, Electricity, Sewer, Gas, Water);
        Food.Children.AddRange(OtherFood, Groceries, Dining);
        Groceries.Children.AddRange(Snacks, Candy, Desert, Staples);
        Dining.Children.AddRange(FastFood, Restaurant);
        Insurance.Children.AddRange(
            OtherInsurance,
            MedicalInsurance,
            HouseInsurance,
            VehicleInsurance,
            LifeInsurance
        );
        Income.Children.AddRange(OtherIncome, Employment, InvestmentIncome, Benefits, Wages, Reimbursement);
        Transportation.Children.AddRange(Vehicle, OtherTransportation);
        Vehicle.Children.AddRange(Fuel, VehicleRepair);

        return Root;

    }

    //Uses Recursion to find the category node in the tree to associate with a transaction. 
    //This allows for all transactions associated with a specific category to be programmatically
    //linked to the same CategoryNode which allows for faster retrieval and analysis. 
    
    public CategoryNode? FindCategoryNode(Category target)
    {
        if (this.CatName == target)
        {
            return this;
        }
        else
        {
            foreach (CategoryNode child in this.Children)
            {
                CategoryNode? result = this.FindCategoryNode(target);
                if (result != null) return result;
            }
            return null;
        }
    }

    public string ListCategories(List<string>? categoriesList = null)
    {
        categoriesList ??= new List<string>();

        foreach (var child in this.Children)
        {
            categoriesList.Add(child.CatName.ToString());
            child.ListCategories(categoriesList);
        }
        return string.Join(",", categoriesList);
    }
}
