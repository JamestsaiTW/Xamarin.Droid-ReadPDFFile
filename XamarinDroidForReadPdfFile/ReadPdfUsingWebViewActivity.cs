using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace XamarinDroidForReadPdfFile
{
    [Activity(Label = "ReadPDFUsingWebViewActivity", Theme = "@android:style/Theme.NoTitleBar")]
    public class ReadPdfUsingWebViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ReadPdfUsingWebView);

            ReadPdfByWebView();
        }

        private void ReadPdfByWebView()
        {
            var localWebView = FindViewById<WebView>(Resource.Id.LocalWebView);

            localWebView.SetWebViewClient(new WebViewClient());

            var fileName = "MyPDFDemoFile.pdf";
            var fullPath = Path.Combine(FilesDir.Path, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                localWebView.Settings.JavaScriptEnabled = true;
                localWebView.Settings.AllowUniversalAccessFromFileURLs = true;
                localWebView.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", fullPath));
            }
            else
            {
                Toast.MakeText(this, "指定的PDF檔案不存在", ToastLength.Short).Show();
            }
        }
    }
}