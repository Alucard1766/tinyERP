using System;
using System.Collections.ObjectModel;

namespace tinyERP.UI.ViewModels
{
    class BudgetViewModel : ViewModelBase
    {
        public ObservableCollection<BudgetView> BudgetViews { get; set; }

        public BudgetViewModel()
        {
        }
        public override void Load()
        {
            BudgetViews = new ObservableCollection<BudgetView>
            {
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Drucker", new DateTime(2017, 03, 10), "Büromaterial", 50),
                new BudgetView(100.00, "Schreibmaterial", new DateTime(2017, 03, 12), "Büromaterial", 0)
            };
        }
    }
    public class BudgetView
    {
        public BudgetView(double amount, string categorie, DateTime date, string description, int privatpart)
        {
            Amount = amount;
            Categorie = categorie;
            Date = date;
            Description = description;
            Privatpart = privatpart;
        }

        public double Amount { get; set; }
        public string Categorie { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int Privatpart { get; set; }
    }
}
