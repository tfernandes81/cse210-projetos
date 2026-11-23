public class Cliente
{
    private string _nome;
    private Endereco _endereco;

    public Cliente(string nome, Endereco endereco)
    {
        _nome = nome;
        _endereco = endereco;
    }

    public string GetNome() => _nome;
    public void SetNome(string nome) => _nome = nome;

    public Endereco GetEndereco() => _endereco;
    public void SetEndereco(Endereco endereco) => _endereco = endereco;

    // Delega a verificação para a classe Endereco
    public bool MoraNosEUA()
    {
        return _endereco.EstaNosEUA();
    }
}