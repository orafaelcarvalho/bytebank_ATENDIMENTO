using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;
using bytebank_ExportarDados;
using Newtonsoft.Json;
using orafaelcarvalho_GeradorChavePix;
using System;
using System.IO;
using System.Xml.Serialization;

namespace bytebank_ATENDIMENTO.bytebank.Atendimento
{
    internal class ByteBankAtendimento
    {
        private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>(){
          new ContaCorrente(95){Saldo=100,Titular = new Cliente{Cpf="11111",Nome ="Henrique"}},
          new ContaCorrente(95){Saldo=200,Titular = new Cliente{Cpf="22222",Nome ="Pedro"}},
          new ContaCorrente(94){Saldo=60,Titular = new Cliente{Cpf="33333",Nome ="Marisa"}}
        };

        public void AtendimentoCliente()
        {
            try
            {
                char opcao = '0';
                while (opcao != '9')
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===       Atendimento            ===");
                    Console.WriteLine("===1 - Cadastrar Conta           ===");
                    Console.WriteLine("===2 - Listar Contas             ===");
                    Console.WriteLine("===3 - Remover Conta             ===");
                    Console.WriteLine("===4 - Ordenar Contas            ===");
                    Console.WriteLine("===5 - Pesquisar Conta           ===");
                    Console.WriteLine("===6 - Exportar contas JSON      ===");
                    Console.WriteLine("===7 - Exportar contas XML       ===");
                    Console.WriteLine("===8 - Gerar chave PIX           ===");
                    Console.WriteLine("===9 - Gerar várias chaves PIX   ===");
                    Console.WriteLine("===q - Sair do Sistema          ===");
                    Console.WriteLine("====================================");
                    Console.WriteLine("\n\n");
                    Console.Write("Digite a opção desejada: ");
                    try
                    {
                        opcao = Console.ReadLine()[0];
                    }
                    catch (Exception excecao)
                    {
                        throw new ByteBankException(excecao.Message);
                    }

                    switch (opcao)
                    {
                        case '1':
                            CadastrarConta();
                            break;
                        case '2':
                            ListarContas();
                            break;
                        case '3':
                            RemoverContas();
                            break;
                        case '4':
                            OrdenarContas();
                            break;
                        case '5':
                            PesquisarContas();
                            break;
                        case '6':
                            ExportarContas("Json");
                            break;
                        case '7':
                            ExportarContas("Xml");
                            break;
                        case '8':
                            GerarChavePix();
                            break;
                        case '9':
                            GerarChavesPix();
                            break;
                        case 'q':
                            EncerrarAplicacao();
                            break;
                        default:
                            Console.WriteLine("Opcao não implementada.");
                            Console.ReadKey();
                            break;
                    }
                }
            }
            catch (ByteBankException excecao)
            {
                Console.WriteLine($"{excecao.Message}");
            }

        }

        private void GerarChavesPix()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("===   CHAVE ALEATÓRIA - PIX    ===");
            Console.WriteLine("==================================");
            Console.WriteLine("\n");

            int qtd = 1;

            try
            {
                Console.WriteLine("Digite a quantidade de chaves que você precisa gerar: ");
                var entrada = Console.ReadLine();
                qtd = int.Parse(entrada);
            }
            catch (Exception excecao)
            {
                throw new ByteBankException(excecao.Message);
            }

            var listaDeChaves = GeradorPix.GetChavesPix(qtd);

            Console.WriteLine("\n Lista de chaves solicitadas:");
            foreach (var chave in listaDeChaves)
            {
                Console.WriteLine(chave);
            }

            Console.WriteLine("\n... Pressione ENTER para voltar ao menu ...");
            Console.ReadKey();
        }

        private void GerarChavePix()
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("===   CHAVE ALEATÓRIA - PIX    ===");
            Console.WriteLine("==================================");
            Console.WriteLine("\n");

            Console.WriteLine(GeradorPix.GetChavePix());
            
            Console.WriteLine("\n... Pressione ENTER para voltar ao menu ...");
            Console.ReadKey();
        }

        private void ExportarContas(string formato)
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine($"===    EXPORTAR CONTAS {formato}    ===");
            Console.WriteLine("==================================");
            Console.WriteLine("\n");

            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não existem dados para exportação ...");
                Console.ReadKey();
            }
            else
            {
                var formatoArquivo = FormatoArquivos.Json;

                if (formato == "Json")
                {
                    formatoArquivo = FormatoArquivos.Json;
                }
                if (formato == "Xml")
                {
                    formatoArquivo = FormatoArquivos.Xml;
                }

                var caminho = @"D:\Projetos\bytebank_ATENDIMENTO\export";
                ExportarDados<ContaCorrente>.SalvarDados(caminho, formatoArquivo, _listaDeContas);

                Console.WriteLine($"... Pressione ENTER para voltar ao menu ...");
                Console.ReadKey();
            }
        }

        private void EncerrarAplicacao()
        {
            Console.WriteLine("... Encerrando a aplicação ...");
            Console.ReadKey();
        }

        private void PesquisarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===    PESQUISAR CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Deseja pesquisar por (1) NUMERO DA CONTA ou (2)CPF TITULAR ? ou " +
                "(3)NÚMERO AGENCIA");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.Write("Informe o número da Conta: ");
                        string _numeroConta = Console.ReadLine();
                        ContaCorrente consultaConta = ConsultaPorNumeroConta(_numeroConta);
                        Console.WriteLine(consultaConta.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        Console.Write("Informe o CPF do Titular: ");
                        string _cpf = Console.ReadLine();
                        ContaCorrente consultaCpf = ConsultaPorCPFTitular(_cpf);
                        Console.WriteLine(consultaCpf.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 3:
                    {
                        Console.Write("Informe o Nº da Agência: ");
                        int _numeroAgencia = int.Parse(Console.ReadLine());
                        var contasPorAgencia = ConsultaPorAgencia(_numeroAgencia);
                        ExibirListaDeContas(contasPorAgencia);
                        Console.ReadKey();
                        break;
                    }
                default:
                    Console.WriteLine("Opção não implementada.");
                    break;
            }

        }

        private void ExibirListaDeContas(List<ContaCorrente> contasPorAgencia)
        {
            if (contasPorAgencia == null)
            {
                Console.WriteLine(" ... A consulta não retornou dados ...");
            }
            else
            {
                foreach (var item in contasPorAgencia)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private List<ContaCorrente> ConsultaPorAgencia(int numeroAgencia)
        {
            var consulta = (
                         from conta in _listaDeContas
                         where conta.Numero_agencia == numeroAgencia
                         select conta).ToList();
            return consulta;
        }
        private ContaCorrente ConsultaPorCPFTitular(string? cpf)
        {
            //ContaCorrente conta = null;
            //for (int i = 0; i < _listaDeContas.Count; i++)
            //{
            //    if (_listaDeContas[i].Titular.Cpf.Equals(cpf))
            //    {
            //        conta = _listaDeContas[i];
            //    }
            //}
            //return conta;

            return _listaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
        }

        private ContaCorrente ConsultaPorNumeroConta(string? numeroConta)
        {
            //ContaCorrente conta = null;
            //for (int i = 0; i < _listaDeContas.Count; i++)
            //{
            //    if (_listaDeContas[i].Conta.Equals(numeroConta))
            //    {
            //        conta = _listaDeContas[i];
            //    }
            //}
            //return conta;

            //return _listaDeContas.Where(conta => conta.Conta == numeroConta).FirstOrDefault();

            return (from conta in _listaDeContas
                    where conta.Conta.Equals(numeroConta)
                    select conta).FirstOrDefault();
        }

        private void OrdenarContas()
        {
            _listaDeContas.Sort();
            Console.WriteLine("... Lista de Contas ordenadas ...");
            Console.ReadKey();
        }

        private void RemoverContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===      REMOVER CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Informe o número da Conta: ");
            string numeroConta = Console.ReadLine();
            ContaCorrente conta = null;
            foreach (var item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }
            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da lista! ...");
            }
            else
            {
                Console.WriteLine(" ... Conta para remoção não encontrada ...");
            }
            Console.ReadKey();
        }

        private void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     LISTA DE CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não há contas cadastradas! ...");
                Console.ReadKey();
                return;
            }
            foreach (ContaCorrente item in _listaDeContas)
            {
                Console.WriteLine("===  Dados da Conta  ===");
                //Console.WriteLine("Número da Conta : " + item.Conta);
                //Console.WriteLine("Saldo da Conta : " + item.Saldo);
                //Console.WriteLine("Titular da Conta: " + item.Titular.Nome);
                //Console.WriteLine("CPF do Titular  : " + item.Titular.Cpf);
                //Console.WriteLine("Profissão do Titular: " + item.Titular.Profissao);
                Console.WriteLine(item.ToString());
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.ReadKey();
            }

        }

        private void CadastrarConta()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===   CADASTRO DE CONTAS    ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.WriteLine("=== Informe dados da conta ===");
            
            Console.Write("Número da Agência: ");
            int numeroAgencia = int.Parse(Console.ReadLine());

            ContaCorrente conta = new ContaCorrente(numeroAgencia);

            Console.WriteLine($"Número da conta [NOVA] : {conta.Conta}");

            Console.Write("Informe o saldo inicial: ");
            conta.Saldo = double.Parse(Console.ReadLine());

            Console.Write("Infome nome do Titular: ");
            conta.Titular.Nome = Console.ReadLine();

            Console.Write("Infome CPF do Titular: ");
            conta.Titular.Cpf = Console.ReadLine();

            Console.Write("Infome Profissão do Titular: ");
            conta.Titular.Profissao = Console.ReadLine();

            _listaDeContas.Add(conta);

            Console.WriteLine("... Conta cadastrada com sucesso! ...");
            Console.ReadKey();
        }
    }
}
