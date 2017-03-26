using System;
using tinyERP.Dal.Entities;

namespace tinyERP.BusinessLayer
{
    public class DummyDataGenerator
    {
        TinyErpBusinessComponent businessComponent = new TinyErpBusinessComponent();

        public void GenerateDummyData()
        {
            GenerateDummyBudgets(5);
            GenerateDummyTransactions(100);
        }

        private void GenerateDummyBudgets(int amount)
        {
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                Budget budget = new Budget()
                {
                    Year = 2000 + i,
                    Amount = random.Next(500, 50001) / 100 * 100
                };

                businessComponent.InsertBudget(budget);
            }
        }

        private void GenerateDummyTransactions(int amount)
        {
            Random random = new Random();

            for (int i = 0; i < amount; i++)
            {
                Budget budget = businessComponent.Budgets[random.Next(0, businessComponent.Budgets.Count)];

                Transaction transaction = new Transaction()
                {
                    Name = "Transaction " + i + 1,
                    Amount = random.Next(50, 5001) / 10 * 10,
                    PrivatePart = random.Next(0, 101) / 5 * 5,
                    Date = new DateTime(budget.Year, random.Next(1, 13), random.Next(1, 29)),
                    Budget = budget
                };

                businessComponent.InsertTransaction(transaction);
            }
        }
    }
}
