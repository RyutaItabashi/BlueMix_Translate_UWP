using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using IBM.WatsonDeveloperCloud.LanguageTranslator.v3;
using BlueMixTranslator.Assets;

namespace BlueMixTranslator
{
    public sealed partial class MainPage : Page
    {
        LanguageTranslatorService translator = new LanguageTranslatorService();
        credit credencialServer = new credit();
        public MainPage()
        {
            String name = credencialServer.CreateName();
            String pass = credencialServer.CreatePass();
            translator.SetCredential(pass, name);
            this.InitializeComponent();
        }

        private void Trans_Click(object sender, RoutedEventArgs e)
        {
            var toBeTrans = Input.Text;
            List<String> text = new List<String>();
            text.AddRange(new String[] { toBeTrans });
            var req = new IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model.TranslateRequest();
            req.Text = text;
            req.Source = "en";
            req.Target = "ja";
            var result = translator.Translate(req);
            var tld = result.Translations;
            foreach (IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model.Translation res in tld)
            {
                Debug.WriteLine(toBeTrans);
                tresult.Text = res.TranslationOutput;
                Debug.WriteLine(res.TranslationOutput);
                Debug.WriteLine(result.WordCount);
                Debug.WriteLine(result.CharacterCount);
            }
            Debug.WriteLine("Success");
        }
    }
}
