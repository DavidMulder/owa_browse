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
	private ExposedWebView webView;
	private bool recent_notification = false;

	public MainWindow (string url): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		webView = new ExposedWebView();
		ExposedWebSettings settings = new ExposedWebSettings();
		settings.g_object_set("user-agent", new GLib.Value("Mozilla/5.0 (Macintosh; Intel Mac OS X 10.8; rv:24.0) Gecko/20100101 Firefox/24.0"));
		settings.g_object_set("enable-spell-checking", new GLib.Value(true));
		webView.TitleChanged += HandleTitleChanged;
		webView.Settings = settings;
		webView.NewWindowPolicyDecisionRequested += HandleNewWindowPolicyDecisionRequested;
		webView.CreateWebView += HandleCreateWebView;
		notifications.Elapsed += HandleElapsed;
		notifications.Start();
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

class ExposedWebSettings : WebKit.WebSettings {
	public void g_object_set(string name, GLib.Value value) {
		SetProperty(name, value);
	}

	public GLib.Value g_object_get(string name) {
		return GetProperty(name);
	}
}

class ExposedWebView : WebKit.WebView {
	public event CreateWebViewHandler CreateWebView
	{
		add
		{
			Signal signal = Signal.Lookup (this, "create-web-view", typeof(CreateWebViewArgs));
			signal.AddDelegate (value);
		}
		remove
		{
			Signal signal = Signal.Lookup (this, "create-web-view", typeof(CreateWebViewArgs));
			signal.RemoveDelegate (value);
		}
	}

	public event WebViewReadyHandler WebViewReady
	{
		add
		{
			Signal signal = Signal.Lookup (this, "web-view-ready", typeof(WebViewReadyArgs));
			signal.AddDelegate (value);
		}
		remove
		{
			Signal signal = Signal.Lookup (this, "web-view-ready", typeof(WebViewReadyArgs));
			signal.RemoveDelegate (value);
		}
	}

	public event NewWindowPolicyDecisionRequestedHandler NewWindowPolicyDecisionRequested
	{
		add
		{
			Signal signal = Signal.Lookup (this, "new-window-policy-decision-requested", typeof(NewWindowPolicyDecisionRequestedArgs));
			signal.AddDelegate (value);
		}
		remove
		{
			Signal signal = Signal.Lookup (this, "new-window-policy-decision-requested", typeof(NewWindowPolicyDecisionRequestedArgs));
			signal.RemoveDelegate (value);
		}
	}

	[DefaultSignalHandler (Type = typeof(WebView), ConnectionMethod = "OverrideCreateWebView")]
	protected virtual WebView OnCreateWebView (WebFrame frame)
	{
		ExposedWebView webView = new ExposedWebView();
		Value empty = Value.Empty;
		ValueArray valueArray = new ValueArray (2u);
		Value[] array = new Value[2];
		array [0] = new Value (this);
		valueArray.Append (array [0]);
		array [1] = new Value (frame);
		valueArray.Append (array [1]);
		GLib.Object.g_signal_chain_from_overridden (valueArray.ArrayPtr, ref empty);
		Value[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Value value = array2 [i];
			value.Dispose ();
		}
		return webView;
	}

	[DefaultSignalHandler (Type = typeof(WebView), ConnectionMethod = "OverrideNewWindowPolicyDecisionRequested")]
	protected virtual int OnNewWindowPolicyDecisionRequested (WebFrame frame, NetworkRequest request, WebNavigationAction navigation_action, WebPolicyDecision policy_decision)
	{
		Value val = new Value (GType.Int);
		ValueArray valueArray = new ValueArray (3u);
		Value[] array = new Value[5];
		array [0] = new Value (this);
		valueArray.Append (array [0]);
		array [1] = new Value (frame);
		valueArray.Append (array [1]);
		array [2] = new Value (request);
		valueArray.Append (array [2]);
		array [3] = new Value (navigation_action);
		valueArray.Append (array [3]);
		array [4] = new Value (policy_decision);
		valueArray.Append (array [4]);
		GLib.Object.g_signal_chain_from_overridden (valueArray.ArrayPtr, ref val);
		Value[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Value value = array2 [i];
			value.Dispose ();
		}
		int result = (int)val;
		val.Dispose ();
		return result;
	}

	[DefaultSignalHandler (Type = typeof(WebView), ConnectionMethod = "OverrideWebViewReady")]
	protected virtual bool OnWebViewReady (WebFrame frame)
	{
		Value empty = Value.Empty;
		ValueArray valueArray = new ValueArray (2u);
		Value[] array = new Value[2];
		array [0] = new Value (this);
		valueArray.Append (array [0]);
		array [1] = new Value (frame);
		valueArray.Append (array [1]);
		GLib.Object.g_signal_chain_from_overridden (valueArray.ArrayPtr, ref empty);
		Value[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			Value value = array2 [i];
			value.Dispose ();
		}
		return true;
	}
}

public delegate void WebViewReadyHandler (object o, WebViewReadyArgs args);

public class WebViewReadyArgs : SignalArgs
{
	//
	// Properties
	//
	
	public WebFrame Frame
	{
		get
		{
			return (WebFrame)base.Args [0];
		}
	}
}

public delegate void CreateWebViewHandler (object o, CreateWebViewArgs args);

public class CreateWebViewArgs : SignalArgs
{
	//
	// Properties
	//
	
	public WebFrame Frame
	{
		get
		{
			return (WebFrame)base.Args [0];
		}
	}
}

public delegate void NewWindowPolicyDecisionRequestedHandler (object o, NewWindowPolicyDecisionRequestedArgs args);

public class NewWindowPolicyDecisionRequestedArgs : SignalArgs
{
	//
	// Properties
	//
	
	public WebFrame Frame
	{
		get
		{
			return (WebFrame)base.Args [0];
		}
	}
	
	public NetworkRequest Request
	{
		get
		{
			return (NetworkRequest)base.Args [1];
		}
	}

	public WebNavigationAction NavigationAction
	{
		get
		{
			return (WebNavigationAction)base.Args [2];
		}
	}

	public WebPolicyDecision PolicyDecision
	{
		get
		{
			return (WebPolicyDecision)base.Args [3];
		}
	}
}

