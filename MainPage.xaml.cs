using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
        LanguageTranslatorService translator = null;
        credit credencialServer = new credit();
        bool isRed = false;
        List<String[]> list = new List<string[]>();
        string[] tmp = new string[2] { " ", " " };
        public MainPage()
        {
            String name = credencialServer.CreateName();
            String pass = credencialServer.CreatePass();
            this.InitializeComponent();
            translator = new LanguageTranslatorService(pass, name, "2018-05-01");
            Debug.WriteLine(Directory.GetCurrentDirectory());
        }

        private void Trans_Click(object sender, RoutedEventArgs e)
        {
            var toBeTrans = Input.Text;
            List<String> text = new List<String>();
            text.AddRange(new String[] { toBeTrans });
            var req = new IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model.TranslateRequest();
            tmp[0] = Input.Text;
            req.Text = text;
            req.Source = "en";
            req.Target = "ja";
            var result = translator.Translate(req);
            var tld = result.Translations;
            foreach (IBM.WatsonDeveloperCloud.LanguageTranslator.v3.Model.Translation res in tld)
            {
                Debug.WriteLine(toBeTrans);
                addBox(Input.Text, res.TranslationOutput);
                Debug.WriteLine(res.TranslationOutput);
                Debug.WriteLine(result.WordCount);
                Debug.WriteLine(result.CharacterCount);
                tmp[1] = res.TranslationOutput;
            }
            list.Add(tmp);
            Debug.WriteLine("Success");
        }

        private void addBox(string source, string result)
        {
            isRed = !isRed;
            Grid grid1 = new Grid();
            TextBlock box = new TextBlock(), sor = new TextBlock();
            StackPanel hor = new StackPanel();
            if (isRed) grid1.Background = new SolidColorBrush(Colors.DeepSkyBlue);
            else grid1.Background = new SolidColorBrush(Colors.LightSkyBlue);
            box.Text = result;
            box.HorizontalAlignment = HorizontalAlignment.Center;
            box.Padding = new Thickness(10, 10, 10, 10);
            box.FontSize = 20;
            sor.Text = source;
            sor.HorizontalAlignment = HorizontalAlignment.Center;
            sor.Padding = new Thickness(10, 10, 10, 10);
            sor.FontSize = 20;
            grid1.Margin = new Thickness(5, 5, 5, 5);
            grid1.Children.Add(hor);
            hor.Children.Add(sor);
            hor.Children.Add(box);
            cont.Children.Add(grid1);
            scroller.UpdateLayout();
        }

        private void csv()
        {
            foreach(string[] tmp in list)
            {
                CSV_Writer _Writer = new CSV_Writer();
                _Writer.csvWrite(tmp[0], tmp[1], csvName.Text);
            }
        }

        private void csv_Write_Click(object sender, RoutedEventArgs e)
        {
            csv();
        }
    }
}
