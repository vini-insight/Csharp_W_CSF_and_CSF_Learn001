using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
// using Csharp_W_CSF.Models;

//http://zetcode.com/csharp/mysql/
// on command line type it:
// $ dotnet add package MySql.Data
// for access MySql ADO.NET framerwork. Include the package to our .NET Core project.

namespace Csharp_W_CSF_and_CSF_Learn001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        [HttpGet] // http://localhost:5000/pessoa/getall RETORNA todos
        public IActionResult GetMySql()
        {

            List<Pessoa> retornada = DB.BuscarNoBancoDados();

            // retornada.Clear(); // PARA TESTAR NOTFOUND

            if(retornada.Count > 0) return Ok(retornada);
            else return NotFound("LISTA VAZIA");
        }

        [HttpGet("{cpf}")] // http://localhost:5000/pessoa/1 RETORNA O SEGUNDO DA LISTA
        public IActionResult GetSingle(string cpf)
        {   
            if ( ! DB.VerificarCpf(cpf) ) return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");

            Pessoa retornada = DB.GetPessoa(cpf);

            if(retornada.Sucesso) return Ok(retornada);
            else return NotFound(retornada.MensagemErro);
        }        

        [HttpPost] // http://localhost:5000/pessoa/ NO POSTMAN, CLICA EM BODY, DEPOISCLICA EM RAW, E AGORA CLICA EM JSON. PREENCHE O NOVO DADO EM JSON, E ENVIAR
        public IActionResult AddPessoaBd(Pessoa newPessoa)
        {
            if ( ! ModelState.IsValid ) return BadRequest(ModelState);

            Pessoa nova = DB.InserirNoBancoDados(newPessoa);

            if(nova.Sucesso) return Ok(nova);
            else return BadRequest(nova.MensagemErro);
        }

        
        [HttpPut]        
        public IActionResult PutPessoaBd(PessoaPut update)
        {
            if ( ! ModelState.IsValid ) return BadRequest(ModelState);

            Pessoa pessoa = DB.GetPessoa(update.Cpf); // possiveis melhorias: atualizar direto na quere UPDATE sem precisar getpessoa nem o atualizarpropriedades. fazer testes depois.
            Pessoa aux = DB.AtualizarPropriedades(update, pessoa);
            Pessoa atualizada = DB.AtualizarNoBancoDados(aux);

            if(atualizada.Sucesso) return Ok(atualizada);
            else return NotFound(atualizada.MensagemErro);
        }
        
        [HttpDelete("{cpf}")]
        public IActionResult DeletePessoaBd(string cpf)
        {
            if ( ! DB.VerificarCpf(cpf) ) return BadRequest("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");
            Pessoa p = DB.ApagarNoBancoDados(cpf);            
            
            if(p.Sucesso) return Ok(p);
            else return NotFound(p.MensagemErro); // 404 não encontrado            
        }
    }
}