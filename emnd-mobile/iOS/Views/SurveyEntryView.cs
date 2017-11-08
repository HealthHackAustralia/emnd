using Foundation;
using System;
using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.iOS.Views;
using MvvmCross.iOS.Views.Presenters.Attributes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Serilog;

namespace Emnd.iOS
{
    // Switch to     [MvxChildPresentation]
    [MvxFromStoryboard("SurveyEntryView")]
    [MvxTabPresentation(WrapInNavigationController = true, TabIconName = "home", TabName = "Start")]
    public partial class SurveyEntryView : MvxViewController<SurveyEntryViewModel>
    {
        public SurveyEntryView(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            // TODO: base view to get rid of keyboard

            base.ViewDidLoad();
            Log.Information("Survey Start load");

            this.NavigationItem.Title = "Start Here";
            var NavButton = new UIBarButtonItem();
            NavButton.Title = "START";
            NavButton.Clicked += delegate {
                ViewModel.ShowBodyMapCommand.Execute(null);
            };
            NavigationItem.RightBarButtonItem = NavButton;


            MySlider.SetThumbImage(UIImage.FromFile("SliderButton.png"), UIControlState.Normal);
            ComparisonSlider.SetThumbImage(UIImage.FromFile("SliderButton.png"), UIControlState.Normal);
            SleepSlider.SetThumbImage(UIImage.FromFile("SliderButton.png"), UIControlState.Normal);

            var set = this.CreateBindingSet<SurveyEntryView, SurveyEntryViewModel>();
            set.Bind(ParticipantNameEntry).To(vm => vm.Survey.ParticipantName);
            set.Bind(ParticipantIdEntry).To(vm => vm.Survey.ParticipantID);
            set.Bind(WeightEntry).To(vm => vm.Survey.Weight);
            //set.Bind(SendButton).To(vm => vm.ShowBodyMapCommand);
            set.Apply();
            //SendButton.TouchUpInside += (sender, e) => {ViewModel.ShowBodyMapCommand.Execute();};

            //this.FeedbackTextField.ShouldReturn += SearchBar_ShouldReturn;
            //this.FeedbackButton.TouchUpInside += FeedbackButton_TouchUpInside;
            //RewardTable.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            Log.Information("Survey Start appeared");
        }
    }
}