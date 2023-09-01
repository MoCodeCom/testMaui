using System.Collections.ObjectModel;
using System.Reflection;
using testRender.Data;
using testRender.Model;

namespace testRender.View;

public partial class dbConnect : ContentPage
{
	public ObservableCollection<Users> UsersList { get; set; } = new ObservableCollection<Users>()
	{
		new Users(){ Id=0, FirstName="mohammed", LastName="alfadhel"},
		new Users(){ Id=1, FirstName="ahmed", LastName="alfadhel"},
		new Users(){ Id=2, FirstName="hasan", LastName="alfadhel"},

	};
	public dbConnect()
	{
		InitializeComponent();
		/*

		var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
		using (Stream s = assembly.GetManifestResourceStream("testRender.sqlDb.db"))
		{
			using (MemoryStream m = new MemoryStream())
			{
				s.CopyTo(m);
				File.WriteAllBytes(usersRepo.DbPath, m.ToArray());
			}
		}

			usersRepo repo = new usersRepo();

		
        
		
        foreach (var u in repo.List())
		{
			UsersList.Add(u);
		}*/
		BindingContext = this;
	}
}
