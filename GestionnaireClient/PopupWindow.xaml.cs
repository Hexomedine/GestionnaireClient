using GestionnaireClient.Model;
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
    /// Logique d'interaction pour PopupWindow.xaml
    /// </summary>
    public partial class PopupWindow : Page
    {

        #region PROPDP : DisplayedControl
        public Control DisplayedPopupControl
        {
            get { return (Control)GetValue(DisplayedPopupControlProperty); }
            set { SetValue(DisplayedPopupControlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayedControl.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayedPopupControlProperty =
            DependencyProperty.Register("DisplayedPopupControl", typeof(Control), typeof(MainWindow));
        #endregion

        public PopupWindow(Sale selectedSale)
        {
            InitializeComponent();
            DisplayedPopupControl = new SaleProductMgr(selectedSale);
        }
    }
}
