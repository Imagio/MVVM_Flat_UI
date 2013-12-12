using System;
using System.Windows.Input;

namespace Ru.Imagio.ViewModel.Workspaces
{
    public class WorkspacePanelItem : ViewModelBase
    {
        private ICommand _selectCommand;

        public WorkspacePanelItem(string caption, WorkspacePanel workspacePanel, Func<ViewModelBase> factoryMethod)
        {
            Caption = caption;
            _workspacePanel = workspacePanel;
            _factoryMethod = factoryMethod;
            _workspacePanel.SelectionChanged += WorkspacePanelOnSelectionChanged;
        }

        private void WorkspacePanelOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("IsSelected");
        }

        private readonly WorkspacePanel _workspacePanel;

        private readonly Func<ViewModelBase> _factoryMethod;

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

        public ViewModelBase Workspace
        {
            get
            {
                return _factoryMethod();
            }
        }
    }
}
