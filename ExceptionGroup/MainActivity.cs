using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;


namespace ExceptionGroup
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        int i = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            AppCenter.Start("82cee015-ad78-469a-b368-e337f2caa84c",
                   typeof(Analytics), typeof(Crashes));

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            Button button = FindViewById<Button>(Resource.Id.button1);
            button.Click += Button_Click;

            Button button2 = FindViewById<Button>(Resource.Id.button2);
            button2.Click += Button2_Click;

            Button button3 = FindViewById<Button>(Resource.Id.button3);
            button3.Click += Button3_Click;

        }

        private void Button3_Click(object sender, EventArgs e)
        {
           

            try
            {
                C c = null;
                Console.WriteLine(c.pet);
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);

                //var messageWithStacktraceId = exception.Message + $" Stack ID: [{exception.StackTrace.GetHashCode()}]";
                //var wrappedException = new WrappedException(messageWithStacktraceId, exception.StackTrace);

                //Crashes.TrackError(wrappedException, null);


            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                B b = null;
                Console.WriteLine(b.age);
            }
            catch (Exception exception)
            {
                if (i % 2 == 1)
                {
                    TimeZoneNotFoundException e3 = new TimeZoneNotFoundException(exception.Message + "Error3");
                    Crashes.TrackError(e3,null, new ErrorAttachmentLog[]{ErrorAttachmentLog.AttachmentWithText("1!", "hello.txt")}); 
                }
                else
                {
                    TimeZoneNotFoundException e2 = new TimeZoneNotFoundException(exception.Message + "Error4");
                    Crashes.TrackError(e2, null, new ErrorAttachmentLog[] { ErrorAttachmentLog.AttachmentWithText("2!", "hello.txt") });
                }
                i++;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {

            try
            {
                A a = null;
                Console.WriteLine(a.name);


            }
            catch (Exception exception)
            {
                TimeZoneNotFoundException e2 = new TimeZoneNotFoundException(exception.Message+"Button1");
                Crashes.TrackError(e2);

                //var messageWithStacktraceId = exception.Message + $" Stack ID: [{exception.StackTrace.GetHashCode()}]";
                //var wrappedException = new WrappedException(messageWithStacktraceId, exception.StackTrace);

                //Crashes.TrackError(wrappedException, null);


            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            try
            {
                int divByZero = 42 / int.Parse("0");
            }
            catch (Exception exception)
            {
                //Crashes.TrackError(exception);
          
                //var messageWithStacktraceId = exception.Message + $" Stack ID: [{exception.StackTrace.GetHashCode()}]";
                //var wrappedException = new WrappedException(messageWithStacktraceId, exception.StackTrace);

                //Crashes.TrackError(wrappedException, null);


            }

            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
    public class A
    {
        public string name { get; set; }
    }
    public class B
    {
        public string age { get; set; }
    }
    public class C
    {
        public string pet { get; set; }
    }
}
