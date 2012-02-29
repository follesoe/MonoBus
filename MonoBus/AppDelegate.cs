using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Diagnostics;

namespace MonoBus
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		private UIWindow _window;
		private UINavigationController _navigationController;
		private BusStopTable _busStopTable;
			
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			_window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			_busStopTable = new BusStopTable();			
			_navigationController = new UINavigationController(_busStopTable);
			_window.RootViewController = _navigationController;		
			_window.MakeKeyAndVisible();
			return true;
		}
	}
}