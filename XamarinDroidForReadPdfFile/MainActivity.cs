using Android.App;
using Android.Widget;
using Android.OS;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Graphics;
using Path = System.IO.Path;

namespace XamarinDroidForReadPdfFile
{
    [Activity(Label = "XamarinDroidForReadPdfFile", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var createPdfFileFromLocalButton = FindViewById<Button>(Resource.Id.CreatePDFFileFromLocalButton);
            createPdfFileFromLocalButton.Click += (sender, e) =>
            {
                CreatePdfFileFromLocalFile();
            };

            var createPdfFileFromWebUrlButton = FindViewById<Button>(Resource.Id.CreatePDFFileFromWebUrlButton);
            createPdfFileFromWebUrlButton.Click += (sender, e) =>
            {
                CreatePdfFileFromWebUrlAsync("http://xamarinclassdemo.azurewebsites.net/files/MyPDFDemoFile.pdf");
            };

            var deletePdfFileButton = FindViewById<Button>(Resource.Id.DeletePDFFileButton);
            deletePdfFileButton.Click += (sender, e) =>
            {
                var fileName = "MyPDFDemoFile.pdf";
                var fullPath = Path.Combine(FilesDir.Path, fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                }
                Toast.MakeText(this, "指定的PDF檔案已刪除", ToastLength.Short).Show();
            };

            var readPdfUsingDefaultAppButton = FindViewById<Button>(Resource.Id.ReadPDFUsingDefaultAppButton);
            readPdfUsingDefaultAppButton.Click += (sender, e) =>
            {
                StartActivity(typeof (ReadPdfUsingDefaultAppActivity));
            };
            var readPdfUsingWebViewButton = FindViewById<Button>(Resource.Id.ReadPDFUsingWebViewButton);
            readPdfUsingWebViewButton.Click += (sender, e) =>
            {
                StartActivity(typeof (ReadPdfUsingWebViewActivity));
            };

        }

        private async void CreatePdfFileFromWebUrlAsync(string url)
        {
            var message = "指定的PDF檔案已存在";
            var fileName = "MyPDFDemoFile.pdf";
            var fullPath = Path.Combine(FilesDir.Path, fileName);

            if (!File.Exists(fullPath))
            {
                using (var httpClient = new HttpClient())
                {
                    using (var stream = await httpClient.GetStreamAsync(url))
                    {
                        using (var file = File.Create(fullPath))
                        {
                            await stream.CopyToAsync(file);
                            message = "指定的PDF檔案下載完成";
                        }
                    }
                }
            }
            Toast.MakeText(this,  message, ToastLength.Short).Show();
        }

        private async void CreatePdfFileFromLocalFile()
        {
            var message = "指定的PDF檔案已存在";
            var fileName = "MyPDFDemoFile.pdf";
            var fullPath = Path.Combine(FilesDir.Path, fileName);
            if (!File.Exists(fullPath))
            {
                using (var stream = Assets.Open(fileName))
                {
                    using (var file = File.Create(fullPath))
                    {
                        await stream.CopyToAsync(file);
                        message = "指定的PDF檔案已建立完成";
                    }
                }
            }
            Toast.MakeText(this, message, ToastLength.Short).Show();
        }
    }
}

