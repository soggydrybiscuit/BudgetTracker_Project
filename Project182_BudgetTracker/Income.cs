using System;
using System.Collections.Generic;

namespace Project182_BudgetTracker
{
    internal class Income
    {
        public string user;
        public string category;
        public double amount;

        enum IncomeMenuActions
        {
            View_Income = 1,
            Add_Income,
            Go_to_Main_Menu
        }
        public static void IncomeMenu(int currentUserIndex)
        {
            Console.WriteLine($"*******{Program.profile[currentUserIndex]} 's Income*******");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine("Income Menu:");

            foreach (IncomeMenuActions actions in Enum.GetValues(typeof(IncomeMenuActions)))
            {
                Console.WriteLine($"Press {(int)actions} to {actions.ToString()}");
            }

            Console.WriteLine("=====================================");
            Console.WriteLine("Enter action");

            int incomeAction = int.Parse(Console.ReadLine());

            switch (incomeAction)
            {
                case 1:
                    ViewIncome(currentUserIndex); break;
                case 2:
                    AddIncome(currentUserIndex); break;
                case 3:
                    Program.MainMenu(currentUserIndex); break;
                default:
                    Console.WriteLine("Invalid input - Try again");
                    IncomeMenu(currentUserIndex);
                    break;
            }

            Console.ReadKey();
        }

        static void ViewIncome(int currentUserIndex)
        {
            Console.WriteLine($"{Program.profile[currentUserIndex]}'s Income");
            Console.WriteLine("------------------------------------------------------");

            foreach (Income var in Program.income)
            {
                if (var.user == Program.profile[currentUserIndex])
                {
                    Console.WriteLine($"Income Amount: {var.amount}");
                }
            }

            Console.WriteLine("=====================================");

            Console.WriteLine("Press any key to finish viewing");
            Console.ReadKey();
            IncomeMenu(currentUserIndex);
        }

        static void AddIncome(int currentUserIndex)
        {
            //represents one record of income (current user, category, amount)
            Income incRecord = new Income();

            //both user and category are already set - category added to simplify the process of adding to dictionary
            incRecord.user = Program.profile[currentUserIndex];
            incRecord.category = "Income";

            Console.WriteLine("Enter amount of income");
            incRecord.amount = Convert.ToDouble(Console.ReadLine());

            //record gets stored in actual income list
            Program.income.Add(incRecord);

            Console.WriteLine($"Income amount of {incRecord.amount} was recorded");

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            IncomeMenu(currentUserIndex);
        }
    }
}
