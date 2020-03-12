using Caliburn.Micro;
using Dragablz;
using DragablzDemo.ViewModels;
using DragablzDemo.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DragablzDemo.Models
{
    public class InterTabClient : IInterTabClient
    {
        private readonly Func<TearedShellViewModel> _factory;

        public InterTabClient( Func<TearedShellViewModel> factory )
        {
            _factory = factory;
        }
        public INewTabHost<Window> GetNewHost( IInterTabClient interTabClient, object partition, TabablzControl source )
        {
            var vm = _factory();
            var v = new TearedShellView();
            ViewModelBinder.Bind( vm, v, null );
            v.Tabs.InterTabController = new InterTabController()
            {
                InterTabClient = this
            };
            return new NewTabHost<Window>( v, v.Tabs );
        }

        public TabEmptiedResponse TabEmptiedHandler( TabablzControl tabControl, Window window )
        {
            return TabEmptiedResponse.CloseWindowOrLayoutBranch;
        }
    }
}
