using testRender.View;

namespace testRender;

public partial class MainPage : ContentPage
{
	

	public MainPage()
	{
		InitializeComponent();
		ConnBtn.Clicked += (sender, obj) =>
		{
			Navigation.PushAsync(new dbConnect());
		};

    }

	
}


