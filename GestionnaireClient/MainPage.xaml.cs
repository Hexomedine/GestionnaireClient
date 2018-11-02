using GestionnaireClient.DAL;
using GestionnaireClient.Views;
using System;
using System.Collections.Generic;
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

namespace GestionnaireClient
{
    /// <summary>
    /// Logique d'interaction pour MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
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
            DependencyProperty.Register("DisplayedControl", typeof(Control), typeof(MainPage));
        #endregion

        public MainPage()
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
                case 0: DisplayedControl = new CustomerMgr(); break;
                case 1: DisplayedControl = new SaleMgr(); break;
                case 2: DisplayedControl = new ProductMgr(); break;
                default: break;
            }
        }
    }
}
