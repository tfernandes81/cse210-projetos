using System.Collections.Generic;
using System.Text;

public class Pedido
{
    private List<Produto> _produtos;
    private Cliente _cliente;

    private const double FreteEUA = 5.0;
    private const double FreteInternacional = 35.0;

    public Pedido(Cliente cliente)
    {
        _cliente = cliente;
        _produtos = new List<Produto>();
    }

    public Cliente GetCliente() => _cliente;
    public void SetCliente(Cliente cliente) => _cliente = cliente;

    public List<Produto> GetProdutos() => _produtos;

    public void AdicionarProduto(Produto produto)
    {
        _produtos.Add(produto);
    }

    // Soma dos produtos + frete único
    public double CalcularCustoTotal()
    {
        double total = 0;
        foreach (Produto produto in _produtos)
        {
            total += produto.CalcularCustoTotal();
        }

        total += _cliente.MoraNosEUA() ? FreteEUA : FreteInternacional;
        return total;
    }

    // Etiqueta de embalagem: nome e ID de cada produto
    public string ObterEtiquetaEmbalagem()
    {
        StringBuilder sb = new StringBuilder();
        foreach (Produto produto in _produtos)
        {
            sb.AppendLine($"{produto.GetNome()} (ID: {produto.GetIdProduto()})");
        }
        return sb.ToString().TrimEnd();
    }

    // Etiqueta de envio: nome e endereço do cliente
    public string ObterEtiquetaEnvio()
    {
        return $"{_cliente.GetNome()}\n{_cliente.GetEndereco().ObterEnderecoCompleto()}";
    }
}