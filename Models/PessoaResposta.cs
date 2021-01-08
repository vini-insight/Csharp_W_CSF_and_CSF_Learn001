using System.Collections.Generic;

namespace Models
{
    public class PessoaResposta : RespostaHttp
    {
        public List<Pessoa> Pessoa { get; set; }
        
        public PessoaResposta()
        {
            Pessoa = new List<Pessoa>();
        }
    }
}