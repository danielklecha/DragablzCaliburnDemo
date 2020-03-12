using Caliburn.Micro;
using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragablzDemo.ViewModels
{
    public class TearedShellViewModel : Conductor<IScreen>.Collection.OneActive
    {
        public TearedShellViewModel(
            IInterTabClient interTabClient,
            IInterLayoutClient interLayoutClient )
        {
            InterTabClient = interTabClient;
            InterLayoutClient = interLayoutClient;
        }
        public IInterTabClient InterTabClient { get; }
        public IInterLayoutClient InterLayoutClient { get; }
    }
}
