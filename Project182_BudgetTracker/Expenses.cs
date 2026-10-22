using System;
using System.Collections.Generic;


namespace Project182_BudgetTracker
{
    internal class Expenses
    {
        public string user;
        public string category;
        public double amount;

        enum ExpenseMenuActions
        {
            View_expenses = 1,
            Add_expense,
            Go_to_Main_Menu
        }
        public static void ExpenseMenu(int currentUserIndex)
        {
            Console.WriteLine($"*******{Program.profile[currentUserIndex]}'s Expenses*******");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Expense Menu:");

            foreach (ExpenseMenuActions actions in Enum.GetValues(typeof(ExpenseMenuActions)))
            {
                Console.WriteLine($"Press {(int)actions} to {actions.ToString()}");
            }

            Console.WriteLine("=====================================");
            Console.WriteLine("Enter action number");

            int expenseAction = int.Parse(Console.ReadLine());

            switch (expenseAction)
            {
                case 1:
                    ViewExpenses(currentUserIndex); break;
                case 2:
                    AddExpense(currentUserIndex); break;
                case 3:
                    Program.MainMenu(currentUserIndex); break;
                default:
                    Console.WriteLine("Invalid input - Try again");
                    ExpenseMenu(currentUserIndex);
                    break;
            }

            Console.ReadKey();
        }
        public static string ExpenseCategories()
        {
            Console.WriteLine("1. Rent");
            Console.WriteLine("2. Utilities");
            Console.WriteLine("3. Groceries");
            Console.WriteLine("4. Entertainment");
            Console.WriteLine("5. Other");
            Console.WriteLine("6. Go back");

            int category = int.Parse(Console.ReadLine());

            switch (category)
            {
                case 1:
                    return "Rent";
                case 2:
                    return "Utilities";
                case 3:
                    return "Groceries";
                case 4:
                    return "Entertainment";
                case 5:
                    return "Other";
                default:
                    Console.WriteLine("Invalid Input - Try again");
                    return ExpenseCategories();
            }
        }

        public static void ViewExpenses(int currentUserIndex)
        {

            Console.WriteLine("Enter which category you want to view expenses for:");
            string viewCategory = ExpenseCategories();

            Console.WriteLine($"{Program.profile[currentUserIndex]}'s {viewCategory} Expenses");
            Console.WriteLine("------------------------------------------------------");

            foreach (Expenses var in Program.expenses)
            {
                if (var.user == Program.profile[currentUserIndex] && var.category == viewCategory)
                {
                    Console.WriteLine($"Expense Amount: {var.amount}");
                }
            }

            Console.WriteLine("=====================================");

            Console.WriteLine("Press any key to finish viewing");
            Console.ReadKey();
            ExpenseMenu(currentUserIndex);
        }

        public static void AddExpense(int currentUserIndex)
        {
            //exp represents one record of an expense (user, category, amount)
            Expenses expRecord = new Expenses();

            expRecord.user = Program.profile[currentUserIndex];

            Console.WriteLine("Enter the category of the expense:");
            string returnCategory = ExpenseCategories();
            expRecord.category = returnCategory;

            Console.WriteLine("Enter the expense amount");
            expRecord.amount = Convert.ToDouble(Console.ReadLine());

            //record is added to the list of expenses
            Program.expenses.Add(expRecord);

            Console.WriteLine($"Expense amount of {expRecord.amount} added to {expRecord.category}");

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            ExpenseMenu(currentUserIndex);
        }
    }
}

