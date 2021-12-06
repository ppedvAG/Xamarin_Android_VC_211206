using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Xamarin_Android
{
    [Activity(Label = "ShowPictureActivity")]
    public class ShowPictureActivity : Activity
    {
        //Control-Properties
        public EditText EdT_Height { get; set; }
        public EditText EdT_Width { get; set; }
        public Button Btn_Go { get; set; }
        public ImageView Img_Content { get; set; }
        public ProgressBar PrB_LoadingPicture { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Aufruf der Base-Methode (Grundlegende Activity-Initialisierung)
            base.OnCreate(savedInstanceState);

            //Verknüpfen und Öffenen der Layout-Datei (im layout-Ordner)
            SetContentView(Resource.Layout.activity_showpicture);

            //Zuweisung und Initialisierung der Controls
            EdT_Height = FindViewById<EditText>(Resource.Id.EdT_Height);
            EdT_Width = FindViewById<EditText>(Resource.Id.EdT_Width);
            Btn_Go = FindViewById<Button>(Resource.Id.Btn_Go);
            Img_Content = FindViewById<ImageView>(Resource.Id.Img_Content);
            PrB_LoadingPicture = FindViewById<ProgressBar>(Resource.Id.PrB_LoadingPicture);

            //Zuweisung einer "Weiterleitung" zum jeweils nächsten Control durch den Bestätigen-Button im Eingabefenster
            //Focus auf nächsten EditText (EdT_Height)
            EdT_Height.Click += (s, e) => EdT_Width.RequestFocus();
            //Markieren des Inhalts von EdT_Height
            EdT_Height.Click += (s, e) => EdT_Width.SelectAll();
            //Ausführen der Btn_Go.Click-Methode
            EdT_Width.Click += GetRandomPic;
            //Verbergen der Tastatur
            EdT_Width.Click += (s, e) =>
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(EdT_Width.WindowToken, 0);
            };

            //Zuweisung einer Methode zum Click-Event des Buttons
            Btn_Go.Click += GetRandomPic;
        }

        private async void GetRandomPic(object sender, EventArgs e)
        {
            //Parsen der durch den Benutzer eingegebenen Höhe und Breite
            int breite = int.Parse(EdT_Width.Text);
            int hoehe = int.Parse(EdT_Height.Text);

            //Initialisieren des ProgressDialogs
            //ProgressDialog progressDialog = new ProgressDialog(this);
            //progressDialog.SetMessage("Downloading Picture...");

            Img_Content.Visibility = ViewStates.Gone;
            PrB_LoadingPicture.Visibility = ViewStates.Visible;

            //Öffnen des WebClients
            using (WebClient client = new WebClient())
            {
                //Öffnen des ProgressDialogs
                //progressDialog.Show();

                //Herunterladen des Bilds als Byte-Array
                byte[] bild = await client.DownloadDataTaskAsync($"http://placeimg.com/{breite}/{hoehe}/any");
                //Umwandlung des Arrays in ein Bitmap
                Android.Graphics.Bitmap bitmap = Android.Graphics.BitmapFactory.DecodeByteArray(bild, 0, bild.Length);

                //Setzen des Bildes in den ImageView
                Img_Content.SetImageBitmap(bitmap);

                //Schließen des ProgressDialogs
                //progressDialog.Dismiss();
            };

            PrB_LoadingPicture.Visibility = ViewStates.Gone;
            Img_Content.Visibility = ViewStates.Visible;
        }
    }
}