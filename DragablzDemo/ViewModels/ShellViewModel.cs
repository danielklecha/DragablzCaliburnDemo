using Caliburn.Micro;
using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragablzDemo.ViewModels
{
	public class ShellViewModel : Conductor<IScreen>.Collection.OneActive
	{
		public ShellViewModel(
			IInterTabClient interTabClient,
			IInterLayoutClient interLayoutClient)
		{
			InterTabClient = interTabClient;
			InterLayoutClient = interLayoutClient;
			Items.Add( new TabContentViewModel(1) );
			Items.Add( new TabContentViewModel(2) );
			Items.Add( new TabContentViewModel(3) );
		}

		public IInterTabClient InterTabClient { get; }
		public IInterLayoutClient InterLayoutClient { get; }
	}
}
