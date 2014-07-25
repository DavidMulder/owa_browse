using System;
using WebKit;
using Gtk;
using GLib;
using System.Timers;
using System.Text.RegularExpressions;

public class MainWindow: Window
{	
	private WebView webView;
	private delegate void HandlerSignalArgsDelegate(object o, SignalArgs args);
	private delegate void HandlerEventlArgsDelegate(object o, EventArgs args);
	private Timer check_mail = new Timer(5000);

	public MainWindow (string url): base (Gtk.WindowType.Toplevel)
	{
		Build ();
		webView = new WebView();

		WebSettings settings = new WebSettings ();
		settings.user_agent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:15.0) Gecko/20120427 Firefox/15.0a1";
		settings.enable_spell_checking = true;
		HandlerSignalArgsDelegate documentLoadFinishedDelegate = HandleDocumentLoadFinished;
		webView.DocumentLoadFinished = documentLoadFinishedDelegate;
		webView.settings = settings;
		HandlerSignalArgsDelegate createWebViewDelegate = HandleCreateWebView;
		webView.CreateWebView = createWebViewDelegate;
		HandlerSignalArgsDelegate newWindowPolicyDecisionRequestedHandler = HandleNewWindowPolicyDecisionRequested;
		webView.NewWindowPolicyDecisionRequested = newWindowPolicyDecisionRequestedHandler;
		HandlerSignalArgsDelegate navigationRequstedHandler = HandleNavigationRequested;
		webView.NavigationRequested = navigationRequstedHandler;
		webView.open(url);
		VBox vbox1 = new VBox();
		vbox1.PackStart(webView, true, true, 0);
		this.Add(vbox1);
		this.Title = "Microsoft Office Outlook";
		this.ShowAll();
		check_mail.Elapsed += HandleCheckMail;
		//check_mail.Start();

	}

	void HandleCheckMail (object sender, ElapsedEventArgs e)
	{
		checkmail();
	}

	void HandleNewWindowPolicyDecisionRequested (object o, SignalArgs args)
	{
		NetworkRequest request = new NetworkRequest (((GLib.Object)args.Args [1]).Handle);
		string request_uri = request.Uri;
		if (request_uri.Contains ("&URL=")) {
			string URL = System.Web.HttpUtility.UrlDecode (Regex.Split (request_uri, "&URL=") [1]);
			System.Diagnostics.Process.Start (URL);
		}
	}

	void HandleCreateWebView (object o, SignalArgs args)
	{
		Window info = new Window("");
		info.DefaultWidth = 1000;
		info.DefaultHeight = 700;
		VBox vbox2 = new VBox();
		WebView child = new WebView();
		HandlerSignalArgsDelegate navigationRequstedHandler = HandleNavigationRequested;
		child.NavigationRequested = navigationRequstedHandler;
		HandlerSignalArgsDelegate closeWebViewDelegate = HandleCloseWebView;
		child.CloseWebView = closeWebViewDelegate;
		vbox2.PackStart(child, true, true, 0);
		info.Add (vbox2);
		info.ShowAll();
		args.RetVal = child;
	}

	void HandleNavigationRequested (object o, SignalArgs args)
	{
		NetworkRequest request = new NetworkRequest(((GLib.Object)args.Args [1]).Handle);
		string request_uri = request.Uri;
		// Destroy the window if it was already opened in the browser
		if (request_uri.Contains ("&URL=")) {
			WebView self = (WebView)o;
			VBox container = (VBox)self.Parent;
			Window parent = (Window)container.Parent;
			parent.Destroy();
			args.RetVal = 1;
		}
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

	private void disable_header ()
	{
		System.Threading.Thread.Sleep(500);
		DOMDocument doc = webView.get_dom_document ();
		DOMElement divBrandLogo = doc.get_element_by_id ("divBrandLogo");
		divBrandLogo.set_attribute("style", "display:none");
		DOMElement divLogOff = doc.get_element_by_id ("divLogOff");
		divLogOff.set_attribute("style", "display:none");
	}

	private void checkmail ()
	{
		check_mail.Interval = 5000;
		DOMDocument doc = webView.get_dom_document ();
		DOMElement lnkNwMl = doc.get_element_by_id ("lnkNwMl");
		try {
			string style = lnkNwMl.get_attribute ("style");
			if (!style.Contains("display:none")) {
				new_mail(lnkNwMl);
				check_mail.Interval = 10000;
			}
		} catch {
			new_mail(lnkNwMl);
			check_mail.Interval = 10000;
		}
	}

	private void new_mail (DOMElement lnkNwMl)
	{
		Console.WriteLine("There is new mail"); //temporary
	}

	void HandleDocumentLoadFinished (object o, EventArgs args)
	{
		disable_header();
	}
	
	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}
}
