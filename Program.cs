using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1DV402.S3.L03
{
    class Program
    {
        //Här körs programmet
        static void Main(string[] args)
        {
           Console.Title = "Receptsamling";
           bool exit = false;
           List<Recipe> recipes = null;
           //Loopa menyn tills menyval 0 anvvänds
           do
           {
               switch (GetMenuChoice())
               {
                   case 0:
                       exit = true;
                       continue;
                   case 1:
                       recipes = LoadRecipes();
                       break;
                   case 2:
                       SaveRecipes(recipes);
                       break;
                   case 3:
                       DeleteRecipe(recipes);
                       break;
                   case 4:
                       ViewRecipe(recipes, false);
                       break;
                   case 5:
                       ViewRecipe(recipes, true);
                       break;
               }
               Console.Clear();
           } while (!exit);
        }
        private static void ContinueOnKeyPressed()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n  Tryck på tangent för att fortsätta  \n");
            Console.ResetColor();
            Console.CursorVisible = false;
            Console.ReadKey(true);
            Console.Clear();
            Console.CursorVisible = true;
        }
        //Används för att ta bort recept
        private static void DeleteRecipe(List<Recipe> recipes)
        {
            if (recipes.Count == 0 || recipes == null)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║     Det finns inga recept att ta bort     ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.ResetColor();
                ContinueOnKeyPressed();
            }
            else if (recipes != null)
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║         Välj recept att ta bort           ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
                Console.WriteLine("0. Avbryt.");
                Console.WriteLine();
                Console.WriteLine("=============================================");
                Console.WriteLine();
                //Sätt recipe till GetRecipe och loopa igenom listan för att presentera tillgängliga recept med index som användaren har möjlighet att ta bort
                Recipe recipe = GetRecipe("header", recipes);
                for (int i = 0; i < recipes.Count; i++)
                {
                    if (recipe == recipes[i])
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("\n Är du säker på att du vill ta bort '{0}'? [J/N]\n", recipe.Name);
                        Console.ResetColor();
                        Console.CursorVisible = false;
                        ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                        Console.CursorVisible = true;
                        //Ta bort valt recept om användaren väljer j
                        if (keyPressed.KeyChar == 'j')
                        {
                            recipes.RemoveAt(i);
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine();
                            Console.WriteLine("╔═══════════════════════════════════════════╗");
                            Console.WriteLine("║         Receptet har tagits bort          ║");
                            Console.WriteLine("╚═══════════════════════════════════════════╝");
                            Console.WriteLine();
                            Console.BackgroundColor = ConsoleColor.Black;
                            ContinueOnKeyPressed();
                        }
                    } 
                }
            }
        }
        //Här hämtas menyn
        private static int GetMenuChoice()
        {
            int menuChoice;

            do
            {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║         Receptsamling med fil             ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine("");
                Console.WriteLine("- Arkiv -------------------------------------");
                Console.WriteLine();
                Console.WriteLine("0. Avsluta.");
                Console.WriteLine("1. Öppna textfil med recept.");
                Console.WriteLine("2. Spara recept på textfil.");
                Console.WriteLine();
                Console.WriteLine("- Redigera ----------------------------------");
                Console.WriteLine();
                Console.WriteLine("3. Ta bort recept.");
                Console.WriteLine();
                Console.WriteLine("- Visa --------------------------------------");
                Console.WriteLine();
                Console.WriteLine("4. Visa recept.");
                Console.WriteLine("5. Visa alla recept.");
                Console.WriteLine();
                Console.WriteLine("═════════════════════════════════════════════");
                Console.WriteLine();
                Console.Write("Ange menyval [0-5]: ");
                //Säkerställer att menyvalet är inom det angivna intervallet
                if (int.TryParse(Console.ReadLine(), out menuChoice) && menuChoice >= 0 && menuChoice <= 5)
                {
                    return menuChoice;
                }
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n FEL! Ange en siffra mellan 0 och 5.\n");
                ContinueOnKeyPressed();
            } while (true);
        }
        //Presenterar de recept som finns tillgängliga
        private static Recipe GetRecipe(string header, List<Recipe> recipes)
        {
            bool exit = false;
            Recipe recipe = null;
            int recipeChoice = 0;
            do
            {
                int temp = 0;
                foreach (Recipe recipeAvailable in recipes)
                {
                    Console.WriteLine("{0}. {1}.", ++temp, recipeAvailable.Name);
                }
                Console.WriteLine();
                Console.WriteLine("═════════════════════════════════════════════");
                Console.WriteLine();
                Console.Write("Välj recept [1-{0}]: ", recipes.Count);
                //Säkerställer att receptvalet är inom det angivna intervallet
                if (!int.TryParse(Console.ReadLine(), out recipeChoice) || recipeChoice < 0 || recipeChoice > recipes.Count)
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\n FEL! Ange en siffra mellan 0 och {0}.\n", recipes.Count);
                    ContinueOnKeyPressed();
                    ViewRecipe(recipes, false);
                }
                exit = true;
            } while (!exit);
            switch(recipeChoice)
            {
                case 0:
                    return null;
                case 1:
                    recipe = recipes[0];
                    break;
                case 2:
                    recipe = recipes[1];
                    break;
                case 3:
                    recipe = recipes[2];
                    break;
            }
            return recipe;
        }
        //Laddar recept från filen recipes.text.txt
        private static List<Recipe> LoadRecipes()
        {
            List<Recipe> recipes;
            RecipeRepository recipeRepository = new RecipeRepository("recipes.text.txt");
            recipes = recipeRepository.Load();
            
            if(recipes != null)
	        {
                Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║          Recepten har lästs in.           ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.BackgroundColor = ConsoleColor.Black;
                ContinueOnKeyPressed();
                return recipes;
	        }
	        else
	        {
		        Console.Clear();
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║    Fel! Ett fel inträffade då recepten    ║");
                Console.WriteLine("║                lästes in.                 ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.BackgroundColor = ConsoleColor.Black;
		        return null;
	        }
        }
        //Sparar tillgängliga recept till recipes.txt
        private static void SaveRecipes(List<Recipe> recipes)
        {
            RecipeRepository recipeRepository = new RecipeRepository("recipes.txt");
            Console.Clear();
            if(recipes == null || recipes.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║      Det finns inga recept att spara      ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                recipeRepository.Save(recipes);
                if (recipeRepository.Path == "recipes.txt")
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("╔═══════════════════════════════════════════╗");
                    Console.WriteLine("║           Recepten har sparats            ║");
                    Console.WriteLine("╚═══════════════════════════════════════════╝");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("╔═══════════════════════════════════════════╗");
                    Console.WriteLine("║    Fel! Ett fel inträffade då recepten    ║");
                    Console.WriteLine("║              skulle sparas.               ║");
                    Console.WriteLine("╚═══════════════════════════════════════════╝");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
            }
            ContinueOnKeyPressed();
        }
        //Här hämtas presentation av tillgängliga recept
        private static void ViewRecipe(List<Recipe> recipes, bool viewAll = false)
        {
            RecipeView recipeView = new RecipeView();
            Console.Clear();
            if (recipes == null || recipes.Count == 0)
            {
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("╔═══════════════════════════════════════════╗");
                Console.WriteLine("║      Det finns inga recept att visa       ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                ContinueOnKeyPressed();
            }
            else if(viewAll && (recipes != null))
            {
                recipeView.Render(recipes);
                ContinueOnKeyPressed();
            }
            else if(!viewAll && (recipes != null))
            {
                bool exit = false;
                //Presenterar tillgängliga recept tills användaren väljer 0 eller listan är tom
                do
                {
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("╔═══════════════════════════════════════════╗");
                    Console.WriteLine("║         Välj recept att visa              ║");
                    Console.WriteLine("╚═══════════════════════════════════════════╝");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine();
                    Console.WriteLine("0. Avbryt.");
                    Console.WriteLine();
                    Console.WriteLine("=============================================");
                    Console.WriteLine();
                    Recipe recipe = GetRecipe("header", recipes);
                    if (recipe != null)
                    {
                        Console.Clear();
                        recipeView.Render(recipe);
                        ContinueOnKeyPressed();
                    } 
                    else if(recipe == null)
                    {
                        exit = true;
                    }
                } while (!exit);
            }
        }
    }
}
