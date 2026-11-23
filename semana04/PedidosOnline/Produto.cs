public class Produto
{
    private string _nome;
    private string _idProduto;
    private double _preco;
    private int _quantidade;

    public Produto(string nome, string idProduto, double preco, int quantidade)
    {
        _nome = nome;
        _idProduto = idProduto;
        _preco = preco;
        _quantidade = quantidade;
    }

    public string GetNome() => _nome;
    public void SetNome(string nome) => _nome = nome;

    public string GetIdProduto() => _idProduto;
    public void SetIdProduto(string idProduto) => _idProduto = idProduto;

    public double GetPreco() => _preco;
    public void SetPreco(double preco) => _preco = preco;

    public int GetQuantidade() => _quantidade;
    public void SetQuantidade(int quantidade) => _quantidade = quantidade;

    // Preço por unidade multiplicado pela quantidade
    public double CalcularCustoTotal()
    {
        return _preco * _quantidade;
    }
}