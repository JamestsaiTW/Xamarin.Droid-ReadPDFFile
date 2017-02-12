using System;
using System.IO;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;

using Android.Widget;

namespace XamarinDroidForReadPdfFile
{
    [Activity(Label = "ReadPDFUsingDefaultAppActivity", NoHistory = true)]
    public class ReadPdfUsingDefaultAppActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ReadPdfByDefaultApp();
        }

        private void ReadPdfByDefaultApp()
        {
            var fileName = "MyPDFDemoFile.pdf";
            var fullPath = Path.Combine(FilesDir.Path, fileName);
            if (System.IO.File.Exists(fullPath))
            {
                OpenFile(fullPath);
            }
            else
            {
                Toast.MakeText(this, "指定的PDF檔案不存在", ToastLength.Short).Show();
                
            }
            Finish();
        }

        public async void OpenFile(string filePath)
        {

            var bytes = File.ReadAllBytes(filePath);

            //Copy the private file's data to the EXTERNAL PUBLIC location
            string externalStorageState = Android.OS.Environment.ExternalStorageState;
            string application = "";

            string extension = System.IO.Path.GetExtension(filePath);

            switch (extension?.ToLower())
            {
                case ".doc":
                case ".docx":
                    application = "application/msword";
                    break;
                case ".pdf":
                    application = "application/pdf";
                    break;
                case ".xls":
                case ".xlsx":
                    application = "application/vnd.ms-excel";
                    break;
                case ".jpg":
                case ".jpeg":
                case ".png":
                    application = "image/jpeg";
                    break;
                default:
                    application = "*/*";
                    break;
            }
            var externalPath = Android.OS.Environment.ExternalStorageDirectory.Path + "/MyPDFDemoFile" + extension;
            File.WriteAllBytes(externalPath, bytes);

            var file = new Java.IO.File(externalPath);
            file.SetReadable(true);
            var uri = Android.Net.Uri.FromFile(file);
            var intent = new Intent(Intent.ActionView);
            intent.SetDataAndType(uri, application);
            intent.SetFlags(ActivityFlags.ClearWhenTaskReset | ActivityFlags.NewTask);

            try
            {
                StartActivity(intent);
            }
            catch (Exception)
            {
                Toast.MakeText(this, $"此手機上沒有可讀取{extension?.ToUpper()}的App", ToastLength.Short).Show();
            }
            finally
            {
                await Task.Delay(3000);
                File.Delete(externalPath);
            }
        }
    }
}