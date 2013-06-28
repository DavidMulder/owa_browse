using System;
using Gtk;
using GLib;
using WebKit;

public partial class MainWindow: Gtk.Window
{	
	public MainWindow (string url): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		ScrolledWindow scrollWindow = new ScrolledWindow();
		ExposedWebView webView = new ExposedWebView();
		ExposedWebSettings settings = new ExposedWebSettings();
		settings.g_object_set("user-agent", new GLib.Value("Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:24.0) Gecko/20100101 Firefox/24.0"));
		settings.g_object_set("javascript-can-open-windows-automatically", new GLib.Value(true));
		settings.g_object_set("enable-spell-checking", new GLib.Value(true));
		webView.TitleChanged += HandleTitleChanged;
		webView.Settings = settings;
		webView.Create += HandleCreate;
		webView.Open(url);
		scrollWindow.Add(webView);
		VBox vbox1 = new VBox();
		vbox1.PackStart(scrollWindow);
		this.Add(vbox1);
		this.ShowAll();
	}

	void HandleCreate (object sender, EventArgs e)
	{
		Console.WriteLine("Created");
	}

	void HandleTitleChanged (object o, TitleChangedArgs args)
	{
		this.Title = args.Title;
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}

class ExposedWebSettings : WebKit.WebSettings {
	public void g_object_set(string name, GLib.Value value) {
		SetProperty(name, value);
	}

	public GLib.Value g_object_get(string name) {
		return GetProperty(name);
	}
}

class ExposedWebView : WebKit.WebView {
	public event EventHandler Create
	{
		add
		{
			Signal signal = Signal.Lookup (this, "create");
			signal.AddDelegate (value);
		}
		remove
		{
			Signal signal = Signal.Lookup (this, "create");
			signal.RemoveDelegate (value);
		}
	}
}
