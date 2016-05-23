using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input.Inking;
using Windows.Globalization;
using Windows.UI.Text.Core;
using Windows.UI.Popups;
using Newtonsoft.Json;
using System.IO;
using Windows.UI.Xaml;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GanaStudy
{
    public sealed partial class MainPage : Page
    {
        List<Character> Hiragana;
        List<Character> Katagana;
        public MainPage()
        {
            try
            {
                InitializeComponent();
                Hiragana = new List<Character>();
                Katagana = new List<Character>();
                if (!(File.Exists(Directory.GetCurrentDirectory() + @"/kata") && File.Exists(Directory.GetCurrentDirectory() + @"/hira")))
                {
                    SetupGana();
                }
                else
                {
                    LoadGana();
                }
                GanaList.Width = this.Width;
                int CharactersPerLine = (int)(GanaList.Width / 100);
                
                int i = 0;
                int lines = -1;
                foreach(Character gana in Hiragana)
                {
                    if(i == CharactersPerLine || i == 0)
                    {
                        RowDefinition newrow = new RowDefinition();
                        newrow.Height = new GridLength(1, GridUnitType.Star);
                        GanaList.RowDefinitions.Add(newrow);
                        lines++;
                        i = 0;
                    }
                    ColumnDefinition newcol = new ColumnDefinition();
                    newcol.Width = new GridLength(1, GridUnitType.Star);
                    GanaList.ColumnDefinitions.Add(newcol);
                    CheckBox checkbox = new CheckBox();
                    checkbox.Content = gana.Gana;
                    checkbox.Width = 200;
                    checkbox.HorizontalAlignment = HorizontalAlignment.Stretch;
                    checkbox.VerticalAlignment = VerticalAlignment.Stretch;
                    GanaList.Children.Add(checkbox);
                    Grid.SetRow(checkbox,lines);
                    Grid.SetColumn(checkbox, i);
                    i++;

                }
            } catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
            }
            
        }
        private void LoadGana()
        {
            string jsonkatagana = File.ReadAllText(Directory.GetCurrentDirectory() + @"/kata");
            string jsonhiragana = File.ReadAllText(Directory.GetCurrentDirectory() + @"/hira");
            Katagana = JsonConvert.DeserializeObject<List<Character>>(jsonkatagana);
            Hiragana = JsonConvert.DeserializeObject<List<Character>>(jsonhiragana);
        }
        private void SetupGana()
        {
            // Simple CSV Data with Hira-/Katagana and Romaji
            #region romaji
            string sRomajiCSV = "a,i,u,e,o,ka,ki,ku,ke,ko,sa,si,su,se,so,ta,ti,tu,te,to,na,ni,nu,ne,no,ha,hi,hu,he,ho,ma,mi,mu,me,mo,ya,yu,yo,ra,ri,ru,re,ro,wa,wo,n";
            string[] aRomaji = sRomajiCSV.Split(',');
            #endregion
            #region Hiragana
            string sHiraganaCSV = "あ,い,う,え,お,か,き,く,け,こ,さ,し,す,せ,そ,た,ち,つ,て,と,な,に,ぬ,ね,の,は,ひ,ふ,へ,ほ,ま,み,む,め,も,や,ゆ,よ,ら,り,る,れ,ろ,わ,を,ん";
            string[] aHiragana = sHiraganaCSV.Split(',');
            if(aRomaji.Count() == aHiragana.Count())
            {
                for (int i = 0; i < sRomajiCSV.Split(',').Count(); i++)
                {
                    Character new_character = new Character(aHiragana[i], aRomaji[i]);
                    Hiragana.Add(new_character);
                }
            }

            string jsonhiragana = JsonConvert.SerializeObject(Hiragana);
            #endregion
            #region Katagana
            string sKataganaCSV = "ア,イ,ウ,エ,オ,カ,キ,ク,ケ,コ,サ,シ,ス,セ,ソ,タ,チ,ツ,テ,ト,ナ,ニ,ヌ,ネ,ノ,ハ,ヒ,フ,ヘ,ホ,マ,ミ,ム,メ,モ,ヤ,ユ,ヨ,ラ,リ,ル,レ,ロ,ワ,ヲ,ン";
            string[] aKatagana = sKataganaCSV.Split(',');
            if (aRomaji.Count() == aKatagana.Count())
            {
                for (int i = 0; i < sRomajiCSV.Split(',').Count(); i++)
                {
                    Character new_character = new Character(aKatagana[i], aRomaji[i]);
                    Katagana.Add(new_character);
                }
            }
            string jsonkatagana = JsonConvert.SerializeObject(Katagana);
            #endregion
            
            File.WriteAllText(Directory.GetCurrentDirectory() + @"/kata", jsonkatagana);
            File.WriteAllText(Directory.GetCurrentDirectory() + @"/hira", jsonhiragana);
        }


        private async void MessageBox(MessageDialog dialog)
        {
            await dialog.ShowAsync();
        }
    }
}
