using System;
using Gtk;

namespace owa_browse3
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			MainWindow win = new MainWindow (args[0]);
			win.Show ();
			Application.Run ();
		}
	}
}
