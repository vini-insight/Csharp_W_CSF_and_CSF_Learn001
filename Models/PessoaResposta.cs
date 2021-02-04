using System.Collections;
using System.Collections.Generic;

namespace Models
{
    public class PessoaResposta : RespostaHttp
    {
        public IList<Pessoa> Pessoa { get; set; }
        
        public PessoaResposta(IList<Pessoa> Pessoa)
        {
            this.Pessoa = Pessoa;
        }
    }
}