using MauiAppMInhasCompras.Models;
namespace MauiAppMInhasCompras.Views;

public class ResumoCategoria
{
	public string Categoria { get; set; }
	public double Total { get; set; }

	public string TotalFormatado => Total.ToString("c");
}

public partial class RelatorioCategoria : ContentPage
{
	public RelatorioCategoria()
	{
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		try
		{
			List<Produto> todos = await App.Db.GetAll();
			var resumo = todos
				.GroupBy(p => string.IsNullOrEmpty(p.Categoria) ? "Sem Categoria" : p.Categoria)
				.Select(g => new ResumoCategoria
				{
					Categoria = g.Key,
					Total = g.Sum(p => p.Total)
				})
				.OrderByDescending(r => r.Total)
				.ToList();
			lst_relatorio.ItemsSource = resumo;

			double totalGeral = resumo.Sum(r => r.Total);
			lbl_total_geral.Text = totalGeral.ToString("c");
        }
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}