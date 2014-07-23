using System;
using WebKit;
using Gtk;
using GLib;
using System.Text.RegularExpressions;

public class MainWindow: Window
{	
	private WebView webView;
	private delegate void HandlerDelegate(object o, SignalArgs args);

	public MainWindow (string url): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		webView = new WebView();

		WebSettings settings = new WebSettings ();
		settings.user_agent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20120427 Firefox/15.0a1";
		settings.enable_spell_checking = true;
		HandlerDelegate HandleTitleChangedDelegate = HandleTitleChanged;
		webView.TitleChanged = HandleTitleChangedDelegate;
		webView.settings = settings;
		HandlerDelegate createWebViewDelegate = HandleCreateWebView;
		webView.CreateWebView = createWebViewDelegate;
		webView.open(url);
		VBox vbox1 = new VBox();
		vbox1.PackStart(webView, true, true, 0);
		this.Add(vbox1);
		this.Title = "Microsoft Office Outlook";
		this.ShowAll();
	}

	void HandleCreateWebView (object o, SignalArgs args)
	{
		Window info = new Window("");
		info.DefaultWidth = 1000;
		info.DefaultHeight = 700;
		VBox vbox2 = new VBox();
		WebView child = new WebView();
		HandlerDelegate closeWebViewDelegate = HandleCloseWebView;
		child.CloseWebView = closeWebViewDelegate;
		vbox2.PackStart(child, true, true, 0);
		info.Add (vbox2);
		info.ShowAll();
		args.RetVal = child;
	}

	void HandleCloseWebView (object o, EventArgs args)
	{
		((WebView)o).Parent.Parent.Destroy();
	}

	private void Build ()
	{
		// Widget MainWindow
		this.Name = "MainWindow";
		this.Title = "";
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		this.DefaultWidth = 1600;
		this.DefaultHeight = 850;
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
	}

	void HandleTitleChanged (object o, EventArgs args) {}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
