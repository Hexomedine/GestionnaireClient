﻿using GestionnaireClient.DAL;
using GestionnaireClient.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace GestionnaireClient.Views
{
    /// <summary>
    /// Logique d'interaction pour SaleMgr.xaml
    /// </summary>
    public partial class SaleMgr : UserControl
    {
        DBContextMgr _context = new DBContextMgr();


        #region PROPDP : DbCtxtMgr
        public DBContextMgr DbCtxtMgr
        {
            get { return (DBContextMgr)GetValue(DbCtxtMgrProperty); }
            set { SetValue(DbCtxtMgrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DbCtxtMgr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DbCtxtMgrProperty =
            DependencyProperty.Register("DbCtxtMgr", typeof(DBContextMgr), typeof(SaleMgr));
        #endregion

        public SaleMgr()
        {
            InitializeComponent();
            DataContext = this;
            _context.Sales.Load();
            DbCtxtMgr = _context;
            SaleDataGrid.ItemsSource = _context.Sales.Local;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // When you delete an object from the related entities collection
            // (in this case Products), the Entity Framework doesn’t mark
            // these child entities as deleted.
            // Instead, it removes the relationship between the parent and the child
            // by setting the parent reference to null.
            // So we manually have to delete the products
            // that have a Category reference set to null.

            // The following code uses LINQ to Objects
            // against the Local collection of Products.
            // The ToList call is required because otherwise the collection will be modified
            // by the Remove call while it is being enumerated.
            // In most other situations you can use LINQ to Objects directly
            // against the Local property without using ToList first.
            foreach (var item in _context.Sales.Local.ToList())
            {
                if (item.DatePeriode == null || item.DatePeriode.Year <2000)
                {
                    _context.Sales.Remove(item);
                }
            }


            _context.SaveChanges();
            // Refresh the grids so the database generated values show up.
            this.SaleDataGrid.Items.Refresh();
        }

        private void SaleDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Load is an extension method on IQueryable,
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query,
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method
            // creates entity objects and adds them to the context.
            _context.Sales.Load();
            // After the data is loaded call the DbSet<T>.Local property
            // to use the DbSet<T> as a binding source.
            //categoryViewSource.Source = _context.Categories.Local;
            SaleDataGrid.ItemsSource = _context.Sales.Local;
        }

        private void OpenSaleButton_Click(object sender, RoutedEventArgs e)
        {
            SimpleWindow simpleWindow = new SimpleWindow();
            Sale vente = (Sale)SaleDataGrid.SelectedItem;
            simpleWindow.Content = new PopupWindow(vente);
            simpleWindow.Show();
            //Page pg = GetDependencyObjectFromVisualTree(this, typeof(Page)) as Page;
            //Sale vente = (Sale)SaleDataGrid.SelectedItem;
            //pg.NavigationService.Navigate(new PopupWindow(vente));
        }

        /// <summary>

        /// Walk visual tree to find the first DependencyObject  of the specific type.

        /// </summary>

        private DependencyObject

        GetDependencyObjectFromVisualTree(DependencyObject startObject, Type type)

        {

            //Walk the visual tree to get the parent(ItemsControl)

            //of this control

            DependencyObject parent = startObject;

            while (parent != null)

            {

                if (type.IsInstanceOfType(parent))

                    break;

                else

                    parent = VisualTreeHelper.GetParent(parent);

            }

            return parent;

        }
    }
}
