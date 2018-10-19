using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;

namespace GestionnaireClient
{
    public class ObservableListSource<T> : ObservableCollection<T>, IListSource
             where T : class
    {
        private IBindingList _bindingList;

        bool IListSource.ContainsListCollection { get { return false; } }

        IList IListSource.GetList()
        {
            _bindingList = this.ToBindingList();
            return _bindingList;
        }
    }
}
