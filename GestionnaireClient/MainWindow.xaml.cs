using GestionnaireClient.DAL;
using System.Data.Entity;
using System.Linq;
using System.Windows;


namespace GestionnaireClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomerContext _context = new CustomerContext();

        public MainWindow()
        {
            InitializeComponent();
           
            //CustomerDataGrid.DataContext = new CustomerBrowser();
            
        }

        private void Gestionnaire_Closed(object sender, System.EventArgs e)
        {    
            _context.Dispose();
        }

        private void Gestionnaire_Loaded(object sender, RoutedEventArgs e)
        {

            // Load is an extension method on IQueryable,
            // defined in the System.Data.Entity namespace.
            // This method enumerates the results of the query,
            // similar to ToList but without creating a list.
            // When used with Linq to Entities this method
            // creates entity objects and adds them to the context.
            _context.Customers.Load();

            // After the data is loaded call the DbSet<T>.Local property
            // to use the DbSet<T> as a binding source.
            //categoryViewSource.Source = _context.Categories.Local;
            CustomerDataGrid.ItemsSource = _context.Customers.Local;
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
            foreach (var item in _context.Customers.Local.ToList())
            {
                if (item.FirstName == string.Empty && item.LastName == string.Empty && item.Email == string.Empty)
                {
                    _context.Customers.Remove(item);
                }
            }

            _context.SaveChanges();
            // Refresh the grids so the database generated values show up.
            this.CustomerDataGrid.Items.Refresh();
        }
    }
}
