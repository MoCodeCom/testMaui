using testRender.ViewModel;

namespace testRender.View;

public partial class dbTest : ContentPage
{
	
    private readonly ProductViewModel _viewModel;
	/*
	public dbTest()
	{
		ProductViewModel viewmodel = new ProductViewModel();
	}*/

    public dbTest(ProductViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadProuctsAsync();
	}
	
}
