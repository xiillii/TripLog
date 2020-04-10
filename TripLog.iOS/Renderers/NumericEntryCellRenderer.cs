using System;
using TripLog.Controls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NumericEntryCell), typeof(TripLog.iOS.Renderers.NumericEntryCellRenderer))]
namespace TripLog.iOS.Renderers
{
    public class NumericEntryCellRenderer : EntryCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var numericCell = (NumericEntryCell)item;

            UITextField textField = null;
            if (cell != null)
            {
                textField = (UITextField)cell.ContentView.Subviews[0];
            }

            if (numericCell.Keyboard == Keyboard.Numeric)
                textField.KeyboardType = UIKeyboardType.NumbersAndPunctuation;

            return cell;
        }
    }
}
