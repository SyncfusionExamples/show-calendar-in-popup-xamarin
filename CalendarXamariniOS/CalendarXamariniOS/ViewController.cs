using CoreGraphics;
using Foundation;
using Syncfusion.iOS.PopupLayout;
using Syncfusion.SfCalendar.iOS;
using System;
using UIKit;

namespace CalendarXamariniOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        SfPopupLayout popupLayout;
        UIView rootView;
        CustomView customView;
        UIButton showPopupButton, clearButton, okButton;
        UILabel headerContent;
        SFCalendar calendar;
        UIStackView uIStackView;
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //// Initialize SfCalendar
            calendar = new SFCalendar();
            calendar.MonthChanged += Calendar_MonthChanged;
            calendar.HeaderHeight = 0;
            calendar.MonthViewSettings.TodaySelectionBackgroundColor = UIColor.FromRGB(137, 82, 83);

            //// Initialize SfPopUpLayout
            popupLayout = new SfPopupLayout();

            headerContent = new UILabel();
            headerContent.TextAlignment = UITextAlignment.Center;
            headerContent.BackgroundColor = UIColor.FromRGB(137, 82, 83);
            headerContent.TextColor = UIColor.White;
            headerContent.Font = UIFont.FromName("Helvetica-Bold", 16);

            // Adding Header view of the SfPopupLayout
            popupLayout.PopupView.HeaderView = headerContent;
            popupLayout.PopupView.ShowCloseButton = false;

            // Adding Calendar as ContentView of the SfPopupLayout
            popupLayout.PopupView.ContentView = calendar;

            clearButton = new UIButton();
            clearButton.SetTitle("Clear", UIControlState.Normal);
            clearButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            clearButton.BackgroundColor = UIColor.Gray;
            clearButton.Frame = new CGRect(100, 10, 80, 30);
            clearButton.Layer.CornerRadius = 10;
            clearButton.ClipsToBounds = true;

            okButton = new UIButton();
            okButton.SetTitle("Ok", UIControlState.Normal);
            okButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            okButton.BackgroundColor = UIColor.Gray;
            okButton.Frame = new CGRect(190, 10, 80, 30);
            okButton.Layer.CornerRadius = 10;
            okButton.ClipsToBounds = true;

            clearButton.TouchDown += ClearButton_TouchDown;
            okButton.TouchDown += OkButton_TouchDown;

            uIStackView = new UIStackView();
            uIStackView.Axis = UILayoutConstraintAxis.Horizontal;
            uIStackView.UserInteractionEnabled = true;
            uIStackView.Add(clearButton);
            uIStackView.Add(okButton);

            // Adding Footer view of the SfPopupLayout
            popupLayout.PopupView.FooterView = uIStackView;

            rootView = new UIView();
            rootView = GetContentOfPopup();
            this.View.AddSubview(rootView);
        }

        private void OkButton_TouchDown(object sender, EventArgs e)
        {
            this.popupLayout.IsOpen = false;
        }

        private void ClearButton_TouchDown(object sender, EventArgs e)
        {
            this.calendar.ClearSelection();
        }

        private void Calendar_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            NSDateFormatter nSDateFormatter = new NSDateFormatter();
            nSDateFormatter.DateFormat = (NSString)@"MMMM yyyy";
            //// Header Conternt updated here.
            headerContent.Text = nSDateFormatter.ToString(e.NavigatedMonth);
        }

        private UIView GetContentOfPopup()
        {
            customView = new CustomView();
            customView.BackgroundColor = UIColor.White;

            showPopupButton = new UIButton();
            showPopupButton.SetTitle("Click to show Popup", UIControlState.Normal);
            showPopupButton.SetTitleColor(UIColor.Black, UIControlState.Normal);
            showPopupButton.BackgroundColor = UIColor.White;
            showPopupButton.TouchDown += ShowPopupButton_TouchDown;

            customView.AddSubview(showPopupButton);
            return customView;
        }

        private void ShowPopupButton_TouchDown(object sender, EventArgs e)
        {
            popupLayout.Show();
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            rootView.Frame = new CGRect(0, 20, this.View.Frame.Width, this.View.Frame.Height - 20);
            calendar.Frame = new CGRect(0, 20, this.View.Frame.Width - 100, this.View.Frame.Height - 200);
            uIStackView.Frame = new CGRect(0, 20, this.View.Frame.Width, this.View.Frame.Height);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}