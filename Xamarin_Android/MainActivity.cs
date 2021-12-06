using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;

namespace Xamarin_Android
{
    //Jede Android-Activity steht für eine Aktion oder ein Layout, welche durch die App durchgeführt
    //oder angezeigt wird. Der 'Code Behind' einer Activity ist eine C#-Klasse, welche mit dem 'Activity'
    //-Attribut gekennzeichnet ist. Hier kann auch der evtl. angezeigte Titel und der verwendete Style
    //definiert werden.
    //Soll die Activity die zuerst angezeigte Activity (=Startseite) der App sein, muss hier die Property
    //'MainLauncher' auf true stehen.
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        //Properties, in welchen die Steuerelemente des Layouts für den Zugriff durch C# abgelegt werden
        public Button Btn_Ok { get; set; }
        public Button Btn_Google { get; set; }
        public Button Btn_Picture { get; set; }
        public EditText Edt_Input { get; set; }

        //Methode, welche beim Starten (Initialisieren) der Activity ausgeführt wird
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Aufruf der Base-OnCreate()-Methode (Grundlegende Activity-Initialisierung)
            base.OnCreate(savedInstanceState);
            //Initialisierung der Xamarin-Essentials
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            //Zuweisung und Aktivierung eines Layouts (aus dem layout-Ordner) zu dieser Activity. Dies
            //erfolgt mittels der Ressourcen-Klassen).
            SetContentView(Resource.Layout.activity_main);

            //Zuweisung der UI-Elemente zu den Properties mittels der FindViewById<>()-Methode,
            //welche die Resource-Klassen nach der angegebenen Id durchsucht.
            Btn_Ok = FindViewById<Button>(Resource.Id.activity_main_Btn_Ok);
            Edt_Input = FindViewById<EditText>(Resource.Id.activity_main_Edt_Input);
            Btn_Google = FindViewById<Button>(Resource.Id.activity_main_Btn_Google);
            Btn_Picture = FindViewById<Button>(Resource.Id.activity_main_Btn_ShowPicture);

            //Zuweisung einer Methode zu einem Click-Event eines Buttons.
            //Diese Methode kreiert einen Toast (kl. Anzeige am unteren Bildschirmrand) und zeigt ihn an.
            Btn_Ok.Click += (s, e) => Toast.MakeText(this, $"Ihre gewählte Zahl ist {Edt_Input.Text}.", ToastLength.Long).Show();
            //EditText.Click wird mit Klick auf den Haken in der Tastatur ausgeführt
            Edt_Input.Click += (s, e) => Toast.MakeText(this, $"Ihre gewählte Zahl ist {Edt_Input.Text}.", ToastLength.Long).Show();

            //Impliziter Intent (Verweis auf eine Activity, welche mit dem ihrem Typen zugeordneten
            //Standartprogramm geöffnet wird) am Beispiel eines Webpage-Aufrufs im Standartbrowser.

            //Erstellung des Intents
            Intent impliziterIntent = new Intent(Intent.ActionView, Android.Net.Uri.Parse("https://placeimg.com/640/800/any"));
            //Zuweisung des Click-Events mit der StartActivity()-Methode, welcher der Intent übergeben wird
            Btn_Google.Click += (s, e) => StartActivity(impliziterIntent);

            //Expliziter Intent (Verweis auf eine Activity, welche in einer genau definierten App ausgeführt wird)
            //Am Beispiel des Öffnens eines neuen Layouts

            //Erstellung des Intents
            Intent expliziterIntent = new Intent(this, typeof(ShowPictureActivity));
            //Zuweisung des Click-Events
            Btn_Picture.Click += (s, e) => StartActivity(expliziterIntent);

        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}