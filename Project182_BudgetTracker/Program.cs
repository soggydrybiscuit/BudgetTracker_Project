using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project182_BudgetTracker
{
    internal class Program
    {
        //initialised in class so list will exist as soon as program starts
        public static List<string> profile = new List<string>() { "Pubert", "Fred", "Mort", "Wendy" };
        static List<string> password = new List<string>() { "pass1", "pass2", "pass3", "pass4" };

        public static List<Expenses> expenses = new List<Expenses>();
        public static List<Income> income = new List<Income>();

        //int variable will help keep track of the logged in profile which will also help when adding income/expenses
        public static int currentUserIndex;

        enum MainMenuActions
        {
            Income = 1,
            Expenses,
            Transaction_Summary,
            Logout = 0
        }

        public static void MainMenu(int currentUserIndex)
        {
            Console.WriteLine($"*******Welcome {profile[currentUserIndex]}*******");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Main Menu:");

            foreach (MainMenuActions actions in Enum.GetValues(typeof(MainMenuActions)))
            {
                Console.WriteLine($"Press {(int)actions} to go to {actions.ToString()}");
            }

            Console.WriteLine("=====================================");
            Console.WriteLine("Enter action number");

            int action = int.Parse(Console.ReadLine());

            switch (action)
            {
                case 1:
                    Income.IncomeMenu(currentUserIndex);
                    break;
                case 2:
                    Expenses.ExpenseMenu(currentUserIndex);
                    break;
                case 3:
                    TransactionSummary();
                    break;
                case 0:
                    LogoutConfirmation(currentUserIndex);
                    break;
            }
        }

        public static void TransactionSummary()
        {
            //dictionary storing the categories for both income and expenses and totals for each profile --> therefore each profile would have their own dictionary
            //categories entered and double initiliased as zero so that categories will still print even if no amounts was stored in income or expense list

            foreach (string names in Program.profile)
            {
                Dictionary<string, double> totals = new Dictionary<string, double>
                {
                    { "Income" , 0},
                    { "Rent" , 0},
                    { "Utilities" , 0},
                    { "Groceries" , 0},
                    { "Entertainment" , 0},
                    { "Other" , 0}
                };

                //calculting the totals for each category for the same user from the expense and income lists
                foreach (Expenses var in Program.expenses)
                {
                    if (var.user == names)
                    {
                         totals[var.category] += var.amount;
                    }
                }

                foreach (Income var in Program.income)
                {
                    if (var.user == names)
                    {
                         totals[var.category] += var.amount;
                    }
                }

                //EXPECTED OUTPUT --- roomate1: income: Rent: utilities: groceries: entertainment: other:
                Console.WriteLine($"{names} :");
                Console.WriteLine("------------------------------------------------------");

                foreach (KeyValuePair<string, double> item in totals)
                {
                        Console.WriteLine($"{item.Key} : {item.Value}");
                }

                Console.WriteLine("=====================================");
            }

            Console.WriteLine("Press any key to return to main menu");
            Console.ReadKey();
            MainMenu(currentUserIndex);

        }

        public static void LogoutConfirmation(int currentUserIndex)
        {
            Console.WriteLine($"Are you sure you want to logout, {profile[currentUserIndex]} (Yes/No)?");
            string logout = Console.ReadLine();

            switch (logout)
            {
                case "Yes":
                    Environment.Exit(0);
                    break;
                case "No":
                    MainMenu(currentUserIndex);
                    break;
                default:
                    Console.WriteLine("Invalid input - Try again.");
                    LogoutConfirmation(currentUserIndex);
                    break;
            }
        }

        public static void Main(string[] args)
        {
            //user has three chances to input valid user and password combination.
            //If username/password is still incorrect - program ends
            Console.WriteLine("Welcome to the Budget Tracker for student accomodation");
            Console.WriteLine("------------------------------------------------------");

            bool isBreakLoop = false;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine("Enter username");
                string userProfileInput = Console.ReadLine();

                for (int m = 0; m < profile.Count; m++)
                {
                    bool isValidProfile = false;

                    if (profile[m] == userProfileInput)
                    {
                        isValidProfile = true;

                        Console.WriteLine($"Profile '{profile[m]}' found. Enter password");
                        string userPasswordInput = Console.ReadLine();

                        for (int n = 0; n < password.Count; n++)
                        {
                            if (password[n] == userPasswordInput && m == n)
                            {
                                Console.WriteLine("Successful login");
                                currentUserIndex = m;
                                MainMenu(currentUserIndex);
                                return;
                            }
                            else if (password[n] != userPasswordInput || m != n)
                            {
                                Console.WriteLine("Incorrect password - Try again");
                                isBreakLoop = true;
                                break;
                            }
                        }
                    }
                    if (isBreakLoop == true)
                    {
                        break;
                    }

                    if (isValidProfile == false)
                    {
                        Console.WriteLine("Profile does not exist - Try again");
                    }
                }
            }
            Console.WriteLine("Too many incorrect attempts - Program will terminate");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
