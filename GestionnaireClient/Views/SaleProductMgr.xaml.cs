using GestionnaireClient.DAL;
using GestionnaireClient.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GestionnaireClient.Views
{
    /// <summary>
    /// Logique d'interaction pour SaleProductMgr.xaml
    /// </summary>
    public partial class SaleProductMgr : UserControl
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
            DependencyProperty.Register("DbCtxtMgr", typeof(DBContextMgr), typeof(SaleProductMgr));
        #endregion

        #region PROPDP : SaleTitle
        public string SaleTitle
        {
            get { return (string)GetValue(SaleTitleProperty); }
            set { SetValue(SaleTitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DbCtxtMgr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SaleTitleProperty =
            DependencyProperty.Register("SaleTitle", typeof(string), typeof(SaleProductMgr));
        #endregion

        public SaleProductMgr(Sale selectedSale)
        {
            InitializeComponent();
            SaleTitle = "Vente du "+ selectedSale.DatePeriode.ToString();
            DataContext = this;
            _context.Products.Load();
            DbCtxtMgr = _context;
            AvailableProduct.ItemsSource = _context.Products.Local;
            SelectedSaleProduct.ItemsSource = _context.Sales.Local;
            
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
                if (item.DatePeriode == null || item.DatePeriode.Year < 2000)
                {
                    _context.Sales.Remove(item);
                }
            }


            _context.SaveChanges();
            // Refresh the grids so the database generated values show up.
            this.AvailableProduct.Items.Refresh();
        }

        private void SaleDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            // Load is an extension method on IQueryable,
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query,
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method
            // creates entity objects and adds them to the context.
            _context.Products.Load();
            _context.Sales.Load();
            // After the data is loaded call the DbSet<T>.Local property
            // to use the DbSet<T> as a binding source.
            //categoryViewSource.Source = _context.Categories.Local;
            AvailableProduct.ItemsSource = _context.Products.Local;
        }
    }
}
