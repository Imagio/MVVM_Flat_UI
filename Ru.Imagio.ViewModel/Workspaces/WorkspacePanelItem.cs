using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Ru.Imagio.ViewModel.Workspaces
{
    public class WorkspacePanelItem : ViewModelBase
    {
        private ICommand _selectCommand;

        public WorkspacePanelItem(string caption, WorkspacePanel workspacePanel, WorkspacePanel.WorkspaceType workspaceType)
        {
            Caption = caption;
            _workspacePanel = workspacePanel;
            _workspaceType = workspaceType;
            _workspacePanel.SelectionChanged += WorkspacePanelOnSelectionChanged;
        }

        private WorkspacePanel.WorkspaceType _workspaceType;

        private void WorkspacePanelOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("IsSelected");
        }

        private WorkspacePanel _workspacePanel;

        public bool IsSelected
        {
            get { return _workspacePanel.SelectedItem == this; }
        }

        public string Caption { get; private set; }

        public ICommand SelectCommand
        {
            get
            {
                return _selectCommand ?? (_selectCommand = new DelegateCommand(o => OnSelect()));
            }
        }

        public event EventHandler Select;

        protected virtual void OnSelect()
        {
            EventHandler handler = Select;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
