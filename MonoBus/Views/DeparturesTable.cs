using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace MonoBus
{
	public class DeparturesTable : UITableViewController
	{
		public BusStop SelectedBusStop { get; set; }
		public DeparturesResponse Response { get; set; }		
		private DeparturesDataSource _dataSource;
		
		public DeparturesTable() 
		{
			_dataSource = new DeparturesDataSource(this);	
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();						
			TableView.DataSource = _dataSource;
		}
		
		public void LoadDepartures()
		{			
			Title = SelectedBusStop.NameWithAbbreviations;
			_dataSource.LoadDepartures();
		}
		
		private class DeparturesDataSource : UITableViewDataSource
		{
			private BusBuddy _busBuddy;
			private DeparturesTable _tvc;			
			private NSString _cellID = new NSString("BusStopCell");
			
			public DeparturesDataSource(DeparturesTable tvc)
			{				
				_tvc = tvc;
				_busBuddy = new BusBuddy();
			}
			
			public void LoadDepartures()
			{			
				if(_tvc.Response != null) {
					_tvc.Response.Departures.Clear();
					_tvc.TableView.ReloadData();
				}
				
				ShowSpinner(true);
				
				_busBuddy.GetDepartures(_tvc.SelectedBusStop.LocationId, response => {
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
				return _tvc.Response == null ? 0 : _tvc.Response.Departures.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = _tvc.TableView.DequeueReusableCell(_cellID);
				if(cell == null) {
					cell = new UITableViewCell(UITableViewCellStyle.Subtitle, _cellID);					
					cell.TextLabel.Font = UIFont.FromName("Georgia", 16f);
					cell.DetailTextLabel.Font = UIFont.FromName("Georgia", 12f);
				}
				
				var departure = _tvc.Response.Departures[indexPath.Row];				
				cell.TextLabel.Text = departure.Line + " - " + departure.Destination;
				cell.DetailTextLabel.Text = departure.DisplayTime;
				return cell;
			}
		}
	}
}