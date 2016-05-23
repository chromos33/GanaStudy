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
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input.Inking;
using Windows.Globalization;
using Windows.UI.Text.Core;
using Windows.UI.Popups;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace GanaStudy
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StudyPage : Page
    {
        private StudyPage rootPage;
        InkRecognizerContainer inkRecognizerContainer = null;
        private IReadOnlyList<InkRecognizer> recoView = null;
        private Language previousInputLanguage = null;
        private CoreTextServicesManager textServiceManager = null;
        private ToolTip recoTooltip;
        private InkRecognizer japrecog;
        public StudyPage()
        {
            this.InitializeComponent();
            rootPage = this;
            Gana.InkPresenter.InputDeviceTypes =
                Windows.UI.Core.CoreInputDeviceTypes.Mouse |
                Windows.UI.Core.CoreInputDeviceTypes.Pen;
            InkDrawingAttributes drawingAttributes = new InkDrawingAttributes();
            Gana.InkPresenter.StrokesCollected += InkPresenter_StrokesCollected;
            drawingAttributes.Color = Windows.UI.Colors.Black;
            drawingAttributes.IgnorePressure = false;
            drawingAttributes.FitToCurve = true;
            Gana.InkPresenter.UpdateDefaultDrawingAttributes(drawingAttributes);
            inkRecognizerContainer = new InkRecognizerContainer();
            recoView = inkRecognizerContainer.GetRecognizers();
            if (recoView.Count() > 0)
            {
                foreach (InkRecognizer recognizer in recoView)
                {
                    if (recognizer.Name == "Microsoft 日本語手書き認識エンジン")
                    {
                        inkRecognizerContainer.SetDefaultRecognizer(recognizer);
                    }
                }
            }
            else
            {
                var dialog = new MessageDialog("Please Install Japanese Handwriting");
                MessageBox(dialog);
            }
        }
        private async void InkPresenter_StrokesCollected(InkPresenter sender, InkStrokesCollectedEventArgs args)
        {
            IReadOnlyList<InkStroke> currentStrokes = Gana.InkPresenter.StrokeContainer.GetStrokes();
            if (currentStrokes.Count > 0)
            {

                var recognitionResults = await inkRecognizerContainer.RecognizeAsync(Gana.InkPresenter.StrokeContainer, InkRecognitionTarget.All);

                if (recognitionResults.Count > 0)
                {
                    // Display recognition result
                    string str = "";
                    foreach (var r in recognitionResults)
                    {
                        str += r.GetTextCandidates()[0];
                    }
                    preview.Text = str;
                }
            }

        }
        private async void MessageBox(MessageDialog dialog)
        {
            await dialog.ShowAsync();
        }
    }
}
