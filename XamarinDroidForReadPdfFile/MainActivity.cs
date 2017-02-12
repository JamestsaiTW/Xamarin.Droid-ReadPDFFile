using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;

namespace XamarinDroidForReadPdfFile
{
    [Activity(Label = "XamarinDroidForReadPdfFile", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            CreatePdfFileFromLocalFile();

            var readPdfUsingDefaultAppButton = FindViewById(Resource.Id.ReadPDFUsingDefaultAppButton);
            readPdfUsingDefaultAppButton.Click += (sender, e) =>
            {
                StartActivity(typeof (ReadPdfUsingDefaultAppActivity));
            };
            var readPdfUsingWebViewButton = FindViewById(Resource.Id.ReadPDFUsingWebViewButton);
            readPdfUsingWebViewButton.Click += (sender, e) =>
            {
                StartActivity(typeof(ReadPdfUsingWebViewActivity));
            };

        }

        private async void CreatePdfFileFromLocalFile()
        {
            var fileName = "MyPDFDemoFile.pdf";
            var fullPath = Path.Combine(FilesDir.Path, fileName);
            if (!File.Exists(fullPath))
            {
                using (var stream = Assets.Open(fileName))
                {
                    using (var file = File.Create(fullPath))
                    {
                        await stream.CopyToAsync(file);
                    }
                }
            }
        }
    }
}

