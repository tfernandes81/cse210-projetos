using System;

class Program
{
    static void Main(string[] args)
    {
        Emprego emprego1 = new Emprego();
        emprego1._cargo = "Engenheiro de Software";
        emprego1._empresa = "Microsoft";
        emprego1._anoInicio = 2019;
        emprego1._anoFim = 2022;

        Emprego emprego2 = new Emprego();
        emprego2._cargo = "Gerente";
        emprego2._empresa = "Apple";
        emprego2._anoInicio = 2022;
        emprego2._anoFim = 2023;

        Curriculo meuCurriculo = new Curriculo();
        meuCurriculo._nome = "Tiago Fernandes";

        meuCurriculo._empregos.Add(emprego1);
        meuCurriculo._empregos.Add(emprego2);

        meuCurriculo.Exibir();
    }
}