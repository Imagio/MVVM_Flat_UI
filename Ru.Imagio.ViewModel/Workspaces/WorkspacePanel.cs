using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ru.Imagio.ViewModel.Workspaces
{
    public class WorkspacePanel : ViewModelBase
    {
        private readonly ICollection<WorkspacePanelItem> _items;
        private WorkspacePanelItem _selectedItem;

        public enum WorkspaceType
        {
            Notifications,
            Documents,
            Directories,
            Administration
        }

        public WorkspacePanel()
        {
            _items = new Collection<WorkspacePanelItem>
            {
                new WorkspacePanelItem("УВЕДОМЛЕНИЯ", this, WorkspaceType.Notifications),
                new WorkspacePanelItem("ДОКУМЕНТЫ", this, WorkspaceType.Documents),
                new WorkspacePanelItem("СПРАВОЧНИКИ", this, WorkspaceType.Documents),
                new WorkspacePanelItem("АДМИНИСТРИРОВАНИЕ", this, WorkspaceType.Administration)
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
