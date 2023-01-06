using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Util;

Console.WriteLine("Boas Vindas ao ByteBank, Atendimento.");

#region Exemplos Arrays em C#

//TestaArrayInt();
//TestaBuscarPalavra();
void TestaArrayInt()
{
    int[] idades = new int[5];

    idades[0] = 10;
    idades[1] = 11;
    idades[2] = 12;
    idades[3] = 13;
    idades[4] = 14;

    Console.WriteLine($"Tamanho do array: {idades.Length}");

    int acumulador = 0;
    for (int i = 0; i < idades.Length; i++)
    {
        int idade = idades[i];
        Console.WriteLine($"Indice[{i}] = {idade}");
        acumulador += idade;
    }

    Console.WriteLine($"Média de idade: {acumulador / idades.Length}");
}

void TestaBuscarPalavra()
{
    string[] arrayDePalavras = new string[5];
    for (int i = 0; i < arrayDePalavras.Length; i++)
    {
        Console.Write($"Digite {i+1} palavra: ");
        arrayDePalavras[i] = Console.ReadLine();
    }
    Console.WriteLine("Digite a palavra a ser encontrada:");
    var busca = Console.ReadLine();

    foreach (string palavra in arrayDePalavras)
    {
        if (palavra.Equals(busca))
        {
            Console.WriteLine($"Palavra encontrada = {busca}.");
            break;
        }
    }
}

Array amostra = Array.CreateInstance(typeof(double), 5);
amostra.SetValue(5.9, 0);
amostra.SetValue(1.8, 1);
amostra.SetValue(7.1, 2);
amostra.SetValue(10, 3);
amostra.SetValue(6.9, 4);

// [5.9][1.8][7.1][10][6.9]

//TestaMediana(amostra);
void TestaMediana(Array array)
{
    if((array == null) || (array.Length==0))
    {
        Console.WriteLine("Array para cáculo da mediana está vazio ou nulo.");
    }

    double[] numerosOrdenados = (double[])array.Clone();
    Array.Sort(numerosOrdenados); // Ordem ascendente

    int tamanho = numerosOrdenados.Length;
    int meio = tamanho / 2;
    double mediana = (tamanho % 2 != 0) ? numerosOrdenados[meio] :
        (numerosOrdenados[meio] + numerosOrdenados[meio - 1]) / 2;

    Console.WriteLine($"Com base na amostra a mediana = {mediana}");
}

//TestaArrayDeContasCorrentes();
void TestaArrayDeContasCorrentes()
{
    ListaDeContasCorrentes listaDeContas = new ListaDeContasCorrentes();
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-A", 1110));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-B", 20));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-C", 30));    
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-D", 40));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-E", 50));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-F", 60));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-G", 880));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-H", 70));
    listaDeContas.Adicionar(new ContaCorrente(874, "12345-I", 70));
    var contaDoRafael = new ContaCorrente(123, "11912-1", 50000.12);
    listaDeContas.Adicionar(contaDoRafael);

    //Console.WriteLine("------------------------------------------");
    //listaDeContas.ExibeLista();
    //Console.WriteLine("-------------------");
    //var contaComMaiorSaldo = listaDeContas.MaiorSaldo();
    //Console.WriteLine($"Conta com maior saldo: {contaComMaiorSaldo.Conta} / {contaComMaiorSaldo.Numero_agencia}");
    //Console.WriteLine($"Saldo desse filho da puta: {contaComMaiorSaldo.Saldo}");

    //Console.WriteLine("------------------------------------------");
    //listaDeContas.Remover(contaDoRafael);
    //listaDeContas.ExibeLista();
    //Console.WriteLine("-------------------");
    //contaComMaiorSaldo = listaDeContas.MaiorSaldo();
    //Console.WriteLine($"Conta com maior saldo: {contaComMaiorSaldo.Conta} / {contaComMaiorSaldo.Numero_agencia}");
    //Console.WriteLine($"Saldo desse filho da puta: {contaComMaiorSaldo.Saldo}");

    for (int i = 0; i < listaDeContas.Tamanho; i++)
    {
        ContaCorrente conta = listaDeContas[i];
        Console.WriteLine($"Indice [{i}] = {conta.Conta}/{conta.Numero_agencia}");
    }
}

#endregion

