using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Ru.Imagio.ViewModel.Workspaces
{
    public class WorkspacePanel : ViewModelBase
    {
        private readonly ICollection<WorkspacePanelItem> _items;
        private WorkspacePanelItem _selectedItem;

        public WorkspacePanel()
        {
            _items = new Collection<WorkspacePanelItem>
            {
                new WorkspacePanelItem("УВЕДОМЛЕНИЯ", this, FactoryMethod),
                new WorkspacePanelItem("ДОКУМЕНТЫ", this, FactoryMethod),
                new WorkspacePanelItem("СПРАВОЧНИКИ", this, FactoryMethod),
                new WorkspacePanelItem("АДМИНИСТРИРОВАНИЕ", this, FactoryMethod)
            };

            foreach (var workspacePanelItem in Items)
            {
                workspacePanelItem.Select += (sender, args) =>
                {
                    SelectedItem = sender as WorkspacePanelItem;
                };
            }

            SelectedItem = _items.FirstOrDefault();
        }

#if DEBUG
        private ViewModelBase FactoryMethod()
        {
            Thread.Sleep(1000);
            return null;
        }
#endif

        public WorkspacePanelItem SelectedItem
        {
            get { return _selectedItem; }
            private set
            {
                if (value == _selectedItem) return;
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
                OnSelectionChanged();
            }
        }

        public ICollection<WorkspacePanelItem> Items
        {
            get
            {
                return _items;
            }
        }

        public event EventHandler SelectionChanged;

        protected virtual void OnSelectionChanged()
        {
            EventHandler handler = SelectionChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
