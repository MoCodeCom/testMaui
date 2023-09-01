using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using testRender.Data;
using testRender.Model;

namespace testRender.ViewModel
{
	public partial class ProductViewModel : ObservableObject
    {
        private readonly DatabaseContext _databaseContext;

        public ProductViewModel(DatabaseContext databaseContext)
		{
			_databaseContext = databaseContext;
		}

		[ObservableProperty]
		private ObservableCollection<Product> _product = new();

		[ObservableProperty]
		private Product _operatingProduct = new();

		[ObservableProperty]
		private bool _isBusy;

		[ObservableProperty]
		private string _busyText;

		public async Task LoadProuctsAsync()
		{
			await ExecuteAsync(async () =>
			{
				var products = await _databaseContext.GetAllAsync<Product>();
				if (products is not null && products.Any())
				{
					Product ??= new ObservableCollection<Product>();

					foreach (var product in products)
					{
						Product.Add(product);
					}
				}
			}, "Fetching products...");
		}

		[RelayCommand]
		private void SetOperatingProduct(Product? product) => OperatingProduct = product ?? new();

		[RelayCommand]
		private async Task SaveProductAsync()
		{
			if (OperatingProduct is null)
			{
				return;
			}

			var (isValid, errorMessage) = OperatingProduct.Validate();
			if (!isValid)
			{
				await Shell.Current.DisplayAlert("Validation Error", errorMessage, "Ok");
			}

			var busyText = OperatingProduct.Id == 0 ? "Create porduct...":"Updating product...";
			await ExecuteAsync(async () =>
			{
				if (OperatingProduct.Id == 0)
				{
					//Create porduct
					await _databaseContext.AddItemAsync<Product>(OperatingProduct);
					Product.Add(OperatingProduct);
				}
				else
				{
					//Update product
					if (await _databaseContext.UpdateItemAsync<Product>(OperatingProduct))
					{
						var productCopy = OperatingProduct.Clone();
						var index = Product.IndexOf(OperatingProduct);
						Product.RemoveAt(index);
						Product.Insert(index, productCopy);
					}
					else
					{
						await Shell.Current.DisplayAlert("Error","Product Updation Error.","OK");
						return;
					}
				}

				setOperatingProductCommand.Execute(new());

			}, busyText);

		}

		[RelayCommand]
		private async Task DeleteProductAsync(int id)
		{
			await ExecuteAsync(async () =>
			{
				if (await _databaseContext.DeleteItemByKeyAsync<Product>(id))
				{
					var product = Product.FirstOrDefault(p => p.Id == id);
					Product.Remove(product);
				}
				else
				{
					await Shell.Current.DisplayAlert("Delete Error","Product was not deleted","OK");
				}
			},"Deleting product...");
		}


		private async Task ExecuteAsync(Func<Task>operation, string? busyTex=null)
		{
			IsBusy = true;
			BusyText = busyTex ?? "Processing...";
			try
			{
				await operation?.Invoke();
			}
			catch (Exception ex)
			{
				//App.Current.MainPage.DisplayAlert("Error...", ex.Message.ToString(), "OK");
			}
			finally
			{
				IsBusy = false;
				busyTex = "Processing...";
			}
		}
	}
}

