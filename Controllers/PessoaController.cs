using Microsoft.AspNetCore.Mvc;
using Models;

namespace Csharp_W_CSF_and_CSF_Learn001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            PessoaResposta retorno = DB.GetListPessoa();
            // retorno.Pessoa.Clear(); retorno.MensagemErro = "LISTA VAZIA"; // PARA TESTAR LISTA VAZIA SEM EXCLUIR DO BD
            // if (retorno.Sucesso && retorno.Pessoa.Count > 0)

            /*
                    Fernando, na sugestão de código que você fez estavam dois métodos GetPessoa, sendo que um retornava uma lista e o outro uma única pessoa.
                    por isso eu alterei o nome do que retorna uma lista para GetListPessoa, assim facilita o entendimento.

                    ainda sobre este método,

                    o valor de incialização padrão de boolean em C# é false;
                    por isso a tua sugestão de código não é aprovada no if do controller e sempre retorna 404 mesmo quando tem um item na lista.
                    
                    para que tua sugestão funcionasse,

                    lá no método GetListPessoa() eu teria que sempre que terminar de ler o retorno do BD dar um resposta.Sucesso = true antes de retornar a busca;
                    
                    OU, a outra opção, vc já tinha dito que não era boa prática,
                    
                    que era inicializar a propriedade em RespostaHttp.cs desse jeito: public bool Sucesso { get; set; } = true;

                    por isso eu mantive a condição do if do controller como estava antes.
                    aguardo sua resposta sobre como devo proceder sobre a melhor prática: deixo como está ou faço uma das opções acima?
            */
            
            if (retorno.Pessoa.Count > 0)            
                return Ok(retorno);
            else
                return NotFound(retorno.MensagemErro);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetSingle(string cpf)
        {   
            if ( ! DB.VerificarCpf(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");

            Pessoa retornada = DB.GetPessoa(cpf);

            if(retornada.Sucesso)
                return Ok(retornada);
            else
                return NotFound(retornada.MensagemErro);
        }        

        [HttpPost]
        public IActionResult Post(Pessoa newPessoa)
        {
            if ( ! ModelState.IsValid )
                return BadRequest(ModelState);

            Pessoa nova = DB.InserirNoBancoDados(newPessoa);

            if(nova.Sucesso)
                return Ok(nova);
            else
                return BadRequest(nova.MensagemErro);
        }

        
        [HttpPut]        
        public IActionResult Put(PessoaPut update)
        {
            if ( ! ModelState.IsValid )
                return BadRequest(ModelState);
            Pessoa pessoa = DB.GetPessoa(update.Cpf); // possiveis melhorias: atualizar direto na quere UPDATE sem precisar getpessoa nem o atualizarpropriedades. fazer testes depois.
            if (pessoa.Sucesso)
            {
                Pessoa aux = DB.AtualizarPropriedades(update, pessoa);                
                pessoa = DB.AtualizarNoBancoDados(aux);
            }
            if(pessoa.Sucesso)
                return Ok(pessoa);
            else
                return NotFound(pessoa.MensagemErro);            
        }
        
        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            if ( ! DB.VerificarCpf(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");
            Pessoa p = DB.ApagarNoBancoDados(cpf);            
            if(p.Sucesso)
                return Ok(p);
            else
                return NotFound(p.MensagemErro); // 404 não encontrado            
        }
    }
}