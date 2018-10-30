using GestionnaireClient.DAL;
using GestionnaireClient.Views;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GestionnaireClient
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBContextMgr _context = new DBContextMgr();


        #region PROPDP : DisplayedControl
        public Control DisplayedControl
        {
            get { return (Control)GetValue(DisplayedControlProperty); }
            set { SetValue(DisplayedControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayedControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayedControlProperty =
            DependencyProperty.Register("DisplayedControl", typeof(Control), typeof(MainWindow)); 
        #endregion





        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Gestionnaire_Closed(object sender, System.EventArgs e)
        {    
            _context.Dispose();
        }

        private void MenuCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            
            switch (MenuCombo.SelectedIndex)
            {
                case 0 : DisplayedControl = new CustomerMgr(); break;
                case 1 : DisplayedControl = new SaleMgr(); break;
                case 2: DisplayedControl = new ProductMgr(); break;
                default: break;
            }
        }
    }
}
