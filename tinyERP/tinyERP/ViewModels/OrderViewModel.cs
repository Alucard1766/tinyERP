using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using tinyERP.Dal.Entities;
using tinyERP.UI.Factories;
using tinyERP.UI.Views;

namespace tinyERP.UI.ViewModels
{
    internal class OrderViewModel : ViewModelBase
    {
        private Order _selectedOrder;
        private ObservableCollection<Order> _orderList;

        public OrderViewModel(IUnitOfWorkFactory factory) : base(factory)
        {
        }

        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set { SetProperty(ref _selectedOrder, value, nameof(SelectedOrder)); }
        }

        public ObservableCollection<Order> OrderList
        {
            get { return _orderList; }
            set { SetProperty(ref _orderList, value, nameof(OrderList)); }
        }

        public override void Load()
        {
            var orders = UnitOfWork.Orders.GetOrdersWithCustomers();
            OrderList = new ObservableCollection<Order>(orders);
            CollectionViewSource.GetDefaultView(OrderList).SortDescriptions.Add(new SortDescription("StateModificationDate", ListSortDirection.Descending));
        }

        #region New-Order-Command

        private RelayCommand _newOrderCommand;

        public ICommand NewOrderCommand
        {
            get { return _newOrderCommand ?? (_newOrderCommand = new RelayCommand(param => NewOrder())); }
        }

        private void NewOrder()
        {
            var order = new Order();
            var vm = new EditOrderViewModel(new UnitOfWorkFactory(), order);
            vm.Init();
            var window = new EditOrderView(vm);

            if (window.ShowDialog() ?? false)
            {
                OrderList.Add(order);
            }
        }

        #endregion

        #region Edit-Order-Command

        private RelayCommand _editOrderCommand;

        public ICommand EditOrderCommand
        {
            get { return _editOrderCommand ?? (_editOrderCommand = new RelayCommand(param => EditOrder(), CanEditOrder)); }
        }

        private void EditOrder()
        {
            var vm = new EditOrderViewModel(new UnitOfWorkFactory(), SelectedOrder);
            vm.Init();
            var window = new EditOrderView(vm);

            if (window.ShowDialog() ?? false)
            {
                CollectionViewSource.GetDefaultView(OrderList).Refresh();
            }
        }

        private bool CanEditOrder(object selectedItems)
        {
            return (selectedItems as ICollection)?.Count == 1;
        }

        #endregion

        #region Delete-Orders-Command

        private RelayCommand _deleteOrdersCommand;

        public ICommand DeleteOrdersCommand
        {
            get { return _deleteOrdersCommand ?? (_deleteOrdersCommand = new RelayCommand(DeleteOrders, CanDeleteOrders)); }
        }

        [SuppressMessage("ReSharper", "PossibleNullReferenceException")] //null-reference tested in CanDeleteOrders-method
        private void DeleteOrders(object selectedItems)
        {
            var selectedOrders = (selectedItems as IEnumerable)?.Cast<Order>().ToList();
            if (MessageBox.Show($"Wollen Sie die ausgewählten Aufträge ({selectedOrders.Count}) wirklich löschen?",
                "Aufträge löschen?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                UnitOfWork.Orders.RemoveRange(selectedOrders);
                UnitOfWork.Complete();

                foreach (var o in selectedOrders)
                    OrderList.Remove(o);
            }
        }

        private bool CanDeleteOrders(object selectedItems)
        {
            return (selectedItems as ICollection)?.Count > 0;
        }

        #endregion
    }
}
