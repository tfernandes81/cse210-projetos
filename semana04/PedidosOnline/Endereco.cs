public class Endereco
{
    private string _rua;
    private string _cidade;
    private string _estado;
    private string _pais;

    public Endereco(string rua, string cidade, string estado, string pais)
    {
        _rua = rua;
        _cidade = cidade;
        _estado = estado;
        _pais = pais;
    }

    public string GetRua() => _rua;
    public void SetRua(string rua) => _rua = rua;

    public string GetCidade() => _cidade;
    public void SetCidade(string cidade) => _cidade = cidade;

    public string GetEstado() => _estado;
    public void SetEstado(string estado) => _estado = estado;

    public string GetPais() => _pais;
    public void SetPais(string pais) => _pais = pais;

    // Retorna true se o endereço for nos EUA
    public bool EstaNosEUA()
    {
        string pais = _pais.Trim().ToUpper();
        return pais == "USA" || pais == "EUA" || pais == "US" || pais == "UNITED STATES";
    }

    // Retorna o endereço completo em uma string com quebras de linha
    public string ObterEnderecoCompleto()
    {
        return $"{_rua}\n{_cidade}, {_estado}\n{_pais}";
    }
}