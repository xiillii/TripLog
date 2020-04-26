﻿using System;
using Foundation;
using TripLog.Controls;
using TripLog.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DatePickerEntryCell), typeof(DatePickerEntryCellRenderer))]
namespace TripLog.iOS.Renderers
{
    public class DatePickerEntryCellRenderer : EntryCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            var datepickerCell = (DatePickerEntryCell)item;
            UITextField textField = null;

            if (cell != null)
            {
                textField = (UITextField)cell.ContentView.Subviews[0];
            }

            // default datepicker display attributes
            var mode = UIDatePickerMode.Date;
            var displayFormat = "d";
            var date = NSDate.Now;
            var isLocalTime = false;

            // update datepicker based on cell's properties
            if (datepickerCell != null)
            {
                // kind must be universal or local to cast to NSDate
                if (datepickerCell.Date.Kind == DateTimeKind.Unspecified)
                {
                    var local = new DateTime(datepickerCell.Date.Ticks, DateTimeKind.Local);

                    date = (NSDate)local;
                }
                else
                {
                    date = (NSDate)datepickerCell.Date;
                }

                isLocalTime = datepickerCell.Date.Kind == DateTimeKind.Local
                    || datepickerCell.Date.Kind == DateTimeKind.Unspecified;
            }

            // create iOS datepicker
            var datepicker = new UIDatePicker
            {
                Mode = mode,
                BackgroundColor = cell.BackgroundColor,
                Date = date,
                TimeZone = isLocalTime ? NSTimeZone.LocalTimeZone : new NSTimeZone("UTC")
            };

            // create a toolbar with a done button that will close
            // the datepicker and set the selected value
            var done = new UIBarButtonItem("Done", UIBarButtonItemStyle.Done,
                (s, e) =>
                {
                    var pickedDate = (DateTime)datepicker.Date;

                    if (isLocalTime)
                    {
                        pickedDate = pickedDate.ToLocalTime();
                    }

                    // update the value of the UITextField within the Cell
                    if (textField != null)
                    {
                        textField.Text = pickedDate.ToString(displayFormat);
                        textField.ResignFirstResponder();
                    }

                    // update the Date property on the cell
                    if (datepickerCell != null)
                    {
                        datepickerCell.Date = pickedDate;
                        datepickerCell.SendCompleted();
                    }
                });

            var toolbar = new UIToolbar
            {
                BarStyle = UIBarStyle.Default,
                Translucent = false
            };

            toolbar.SizeToFit();
            toolbar.SetItems(new[] { done }, true);

            // set the input view, toolbar and initial value for the cell's
            // UITextField
            if (textField != null)
            {
                textField.InputView = datepicker;
                textField.InputAccessoryView = toolbar;

                if (datepickerCell != null)
                {
                    textField.Text = datepickerCell.Date.ToString(displayFormat);
                }
            }

            return cell;
        }
    }
}
