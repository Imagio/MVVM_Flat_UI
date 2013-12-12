using System;
using Ru.Imagio.ViewModel.Workspaces;

namespace Ru.Imagio.ViewModel
{
    public class WorkspaceShell : ViewModelBase
    {
        public WorkspaceShell()
        {
            WorkspacePanel = new WorkspacePanel();
            WorkspacePanel.SelectionChanged += WorkspacePanelOnSelectionChanged;
        }

        private void WorkspacePanelOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            OnPropertyChanged("ActiveWorkspace");
        }

        public WorkspacePanel WorkspacePanel { get; private set; }

        public ViewModelBase ActiveWorkspace
        {
            get
            {
                return WorkspacePanel.SelectedItem.Workspace;
            }
        }
    }
}
