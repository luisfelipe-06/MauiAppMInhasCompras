using MauiAppMInhasCompras.Models;

namespace MauiAppMInhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    Produto _produto;
    public NovoProduto()
    {
        InitializeComponent();
        _produto = new Produto();
    }

    public NovoProduto(Produto p)
    {
        InitializeComponent();
        _produto = p;

        txt_descricao.Text = p.Descricao;
        txt_quantidade.Text = p.Quantidade.ToString();
        txt_preco.Text = p.Preco.ToString();

        if (!string.IsNullOrEmpty(p.Categoria))
        {
            picker_categoria.SelectedItem = p.Categoria;
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            _produto.Descricao = txt_descricao.Text;
            _produto.Quantidade = Convert.ToInt32(txt_quantidade.Text);
            _produto.Preco = Convert.ToDouble(txt_preco.Text);
            _produto.Categoria = picker_categoria.SelectedItem?.ToString() ?? "Outros";

            if (_produto.Id == 0)
            {
                await App.Db.Insert(_produto);
                await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
            }
            else
            {
                await App.Db.Update(_produto);
                await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");

                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}