using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragablzDemo.ViewModels
{
	public class TabContentViewModel : Screen
	{
		public TabContentViewModel(int index)
		{
			DisplayName = $"Tab {index}";
			ContentText = $"Content of tab {index}";
		}

		public string ContentText { get; }
	}
}
