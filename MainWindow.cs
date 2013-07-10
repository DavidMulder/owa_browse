using System;
using Gtk;
using GLib;
using WebKit;
using System.Timers;
using Notifications;
using System.Text.RegularExpressions;

public partial class MainWindow: Gtk.Window
{
	private Timer notifications = new Timer(2000);
	private ExtendedWebView webView;
	private bool recent_notification = false;

	public MainWindow (string url): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		webView = new ExtendedWebView ();
		ExtendedWebSettings settings = new ExtendedWebSettings ();
		settings.g_object_set ("user-agent", new GLib.Value ("Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:24.0) Gecko/20100101 Firefox/24.0"));
		settings.g_object_set ("enable-spell-checking", new GLib.Value (true));
		webView.TitleChanged += HandleTitleChanged;
		webView.Settings = settings;
		webView.NewWindowPolicyDecisionRequested += HandleNewWindowPolicyDecisionRequested;
		webView.CreateWebView += HandleCreateWebView;
		//notifications.Elapsed += HandleElapsed;
		//notifications.Start();
		webView.Open(url);
		VBox vbox1 = new VBox();
		vbox1.PackStart(webView);
		this.Add(vbox1);
		this.ShowAll();
	}

	void HandleCreateWebView (object o, CreateWebViewArgs args)
	{
		Window info = new Window("");
		info.DefaultWidth = 1000;
		info.DefaultHeight = 700;
		VBox vbox2 = new VBox();
		WebView child = new WebView();
		child.NavigationRequested += HandleNavigationRequested1;
		vbox2.PackStart(child);
		info.Add (vbox2);
		info.ShowAll();
		args.RetVal = child;
	}

	void HandleNavigationRequested1 (object o, NavigationRequestedArgs args)
	{
		// Destroy the window if it was already opened in the browser
		if (args.Request.Uri.Contains ("&URL=")) {
			WebView self = (WebView)o;
			VBox container = (VBox)self.Parent;
			Window parent = (Window)container.Parent;
			parent.Destroy();
			args.RetVal = 1;
		}
	}
	
	void HandleNewWindowPolicyDecisionRequested (object o, NewWindowPolicyDecisionRequestedArgs args)
	{
		string URL = System.Web.HttpUtility.UrlDecode(Regex.Split(args.Request.Uri, "&URL=")[1]);
		System.Diagnostics.Process.Start(URL);
	}

	void HandleElapsed (object sender, ElapsedEventArgs e)
	{
		if (recent_notification) {
			notifications.Interval = 5000;
			recent_notification = false;
		}
		if (!recent_notification && webView.SearchText ("Reminders", true, true, true)) {
			Notification notify = new Notification ("Outlook Notification", "");
			notify.Urgency = Urgency.Normal;
			notify.Show ();
			recent_notification = true;
			notifications.Interval = 30000;
		}
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
