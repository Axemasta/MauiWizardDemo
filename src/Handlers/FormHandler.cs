using Microsoft.Maui.Handlers;

#if IOS
using UIKit;
using Foundation;
#endif

namespace WizardDemo.Handlers;

public static class FormHandler
{
    // https://dev.to/vhugogarcia/remove-entry-and-picker-borders-in-net-maui-2pk2
    public static void RemoveBorders()
    {
        EntryHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });

        PickerHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS
            handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
            handler.PlatformView.Layer.BorderWidth = 0;
            handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
        });
    }
}