using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoBus
{
	public class BusStopTable : UITableViewController
	{		
		public BusStopResponse Response { get; set; }
		
		public BusStopTable() {}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();			
			Title = "Busstop";				
			TableView.DataSource = new BusStopDataSource(this);
			TableView.Delegate = new BusStopDelegate(this);
		}
		
		private class BusStopDelegate : UITableViewDelegate
		{
			private BusStopTable _tvc;
			private DeparturesTable _departures;
			
			public BusStopDelegate(BusStopTable tvc)
			{
				_tvc = tvc;
			}
			
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				if(_departures == null)
					_departures = new DeparturesTable();
										
				var busStop = _tvc.Response.BusStops[indexPath.Row];
				_departures.SelectedBusStop = busStop;		
				_departures.LoadDepartures();
				_tvc.NavigationController.PushViewController(_departures, true);
			}
		}
		
		private class BusStopDataSource : UITableViewDataSource 
		{
			private BusBuddy _busBuddy;
			private BusStopTable _tvc;
			private NSString _cellID = new NSString("BusStopCell");
			
			public BusStopDataSource(BusStopTable tvc)
			{
				_tvc = tvc;
				
				ShowSpinner (true);
				
				_busBuddy = new BusBuddy();
				_busBuddy.GetBusStops(response => {
					_tvc.Response = response;
					InvokeOnMainThread(() => { 
						_tvc.TableView.ReloadData();
						ShowSpinner(false);
					});
				});
			}
			
			private void ShowSpinner(bool show)
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = show;
			}
			
			public override int RowsInSection (UITableView tableView, int section)
			{
				return _tvc.Response == null ? 0 : _tvc.Response.BusStops.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath)
			{
				var cell = _tvc.TableView.DequeueReusableCell(_cellID);
				if(cell == null) {
					cell = new UITableViewCell(UITableViewCellStyle.Default, _cellID);					
					cell.TextLabel.Font = UIFont.FromName("Georgia", 16f);
					cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
				}
				
				cell.TextLabel.Text = _tvc.Response.BusStops[indexPath.Row].Name;
				return cell;
			}						
		}				
	}
}