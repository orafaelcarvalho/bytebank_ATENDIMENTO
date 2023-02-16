using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytebank_ATENDIMENTO.bytebank.Modelos
{
    public class Pessoa
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public Pessoa(string nome, string sobrenome)
        {
            Nome = nome;
            Sobrenome = sobrenome;
        }

        public override string ToString()
        {

            return $"\n === DADOS DA PESSOA === \n" +
                   $"Nome: {this.Nome} \n" +
                   $"Sobrenome: {this.Sobrenome} \n";
        }
    }
}
