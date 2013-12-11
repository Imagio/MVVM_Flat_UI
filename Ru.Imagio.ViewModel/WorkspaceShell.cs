using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ActiveWorkspace = null;
            OnPropertyChanged("ActiveWorkspace");
        }

        public WorkspacePanel WorkspacePanel { get; private set; }

        public ViewModelBase ActiveWorkspace { get; private set; }
    }
}
