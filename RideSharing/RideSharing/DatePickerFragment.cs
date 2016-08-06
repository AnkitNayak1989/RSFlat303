using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace RideSharing.Resources
{
    [Activity(Label = "DatePickerFragment")]
    public class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        //protected override void OnCreate(Bundle savedInstanceState)
        //{
        //    base.OnCreate(savedInstanceState);

        //    // Create your application here
        //}
        public static readonly string TAG = "X:" + typeof(DatePickerFragment).Name.ToUpper();
        Action<DateTime> dateSelectedHandler = delegate { };
        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
         { 
             // Note: monthOfYear is a value between 0 and 11, not 1 and 12! 
             DateTime selectedDate = new DateTime(year, monthOfYear + 1, dayOfMonth); 
             //Log.Debug(TAG, selectedDate.ToLongDateString()); 
             dateSelectedHandler(selectedDate); 
         }
        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
         { 
             DatePickerFragment frag = new DatePickerFragment(); 
             frag.dateSelectedHandler = onDateSelected; 
             return frag; 
         }
        public override Dialog OnCreateDialog(Bundle savedInstanceState)
         { 
             DateTime currently = DateTime.Now; 
             DatePickerDialog dialog = new DatePickerDialog(Activity, this, currently.Year, currently.Month,
                                                            currently.Day); 
             return dialog; 
         }



}
}