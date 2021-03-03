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
            if (retorno.Sucesso)
                return Ok(retorno);
            else
                return NotFound(retorno.MensagemErro);
        }

        [HttpGet("{cpf}")]
        public IActionResult GetSingle(string cpf)
        {   
            if ( ! DB.ValidarStringCpf(cpf) )
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
            Pessoa pessoa = DB.GetPessoa(update.Cpf);
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
            if ( ! DB.ValidarStringCpf(cpf) )
                return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");
            Pessoa p = DB.ApagarNoBancoDados(cpf);
            if(p.Sucesso)
                return Ok(p);
            else
                return NotFound(p.MensagemErro);            
        }
    }
}