using System;
using System.Collections.Generic;
using Models;
using MySql.Data.MySqlClient;

public static class DB
{
    private const string CS = @"server=localhost;userid=root;password=root;database=nodejs";
    
    public static Pessoa InserirNoBancoDados(Pessoa p)
    {          
        using var con = new MySqlConnection(CS);  
        try
        {
            p.Sexo = p.Sexo.ToUpper();            
            con.Open();            
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "INSERT INTO pessoas (nome, cpf, dataNascimento, sexo) VALUES ('" + p.Nome + "', '" + p.Cpf + "', '" + p.DataNascimento + "', '" + p.Sexo + "')";            
            cmd.CommandText = query;
            var linhasAfetadas = cmd.ExecuteNonQuery();
            // if (linhasAfetadas != 0) return p;
            p.Sucesso = true;
            return p;
        }
        catch (Exception ex)
        {            
            Pessoa erro = new Pessoa();
            erro.MensagemErro = ex.Message;
            erro.Sucesso = false;
            return erro;
        }    
        // catch (System.Exception)
        // {            
        //     throw; // relança exceção e preserva a pilha stack trace
        // }
        finally
        {
            con.Close();
        }
    }

    public static Pessoa AtualizarNoBancoDados(Pessoa p)
    {          
        using var con = new MySqlConnection(CS);  
        try
        {
            con.Open();            
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "UPDATE pessoas SET nome = '" + p.Nome + "', cpf = '" + p.Cpf + "', dataNascimento = '" + p.DataNascimento + "', sexo = '" + p.Sexo + "' WHERE cpf = '" + p.Cpf + "'";                
            cmd.CommandText = query;
            // var linhasAfetadas = cmd.ExecuteNonQuery();
            // if (linhasAfetadas != 0) return p;            
            cmd.ExecuteNonQuery();
            p.Sucesso = true;
            return p;
        }
        catch (Exception ex)
        {            
            Pessoa erro = new Pessoa();
            erro.MensagemErro = ex.Message;
            erro.Sucesso = false;
            return erro;
        }
        finally
        {
            con.Close();
        }             
    }

    public static Pessoa ApagarNoBancoDados(string cpf)
    {    
        using var con = new MySqlConnection(CS);
        try       
        {
            // Pessoa deletada = new Pessoa { Cpf = cpf };
            // deletada = GetPessoa(deletada);

            con.Open();            
            using var cmd = new MySqlCommand();
            cmd.Connection = con;
            string query = "DELETE FROM pessoas WHERE cpf = '" + cpf + "'";            
            cmd.CommandText = query;
            var linhasAfetadas = cmd.ExecuteNonQuery();            
            if (linhasAfetadas != 0) return new Pessoa { Cpf = cpf, Sucesso = true };
            // if (linhasAfetadas != 0) return deletada;
            // else throw new Exception("NÃO ENCONTRADO");
            // else return null;
            else return new Pessoa { Sucesso = false, MensagemErro = "NÃO ENCONTRADO"};
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            con.Close();
        }
    }        

    public static PessoaResposta GetListPessoa()
    {
        using var con = new MySqlConnection(CS);                
        PessoaResposta resposta = new PessoaResposta();
        try
        {
            con.Open();
            string sql = "SELECT * FROM pessoas";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            
            if(rdr.HasRows) // se pesquisa bem sucedida
            {                                
                while (rdr.Read())
                {
                    resposta.Pessoa.Add(new Pessoa
                    {
                        Nome = rdr.GetString(1),
                        Cpf = rdr.GetString(2),
                        DataNascimento = rdr.GetString(3),
                        Sexo = rdr.GetString(4)
                    });
                }    
                // resposta.Sucesso = true;
                return resposta;
            }
            else
            {
                resposta.Sucesso = false;
                resposta.MensagemErro = "LISTA VAZIA";
                return resposta;
            }
        }
        catch (Exception ex)
        {
            resposta.Sucesso = false;
            resposta.MensagemErro = ex.Message;
            return resposta;
        }
        finally
        {
            con.Close();
        }
    }

    public static Pessoa GetPessoa(String cpf)
    {
        using var con = new MySqlConnection(CS);
        Pessoa retornada = new Pessoa();
        try
        {            
            con.Open();                        
            string sql = "SELECT * FROM pessoas WHERE cpf = '" + cpf + "'";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();              
            if(rdr.HasRows) // se pesquisa bem sucedida
            {
                while (rdr.Read())
                {
                    retornada.Nome = rdr.GetString(1);
                    retornada.Cpf = rdr.GetString(2);
                    retornada.DataNascimento = rdr.GetString(3);                    
                    retornada.Sexo = rdr.GetString(4);
                }
                retornada.Sucesso = true;
                return retornada;
            }
            else
            {
                retornada.Sucesso = false;
                retornada.MensagemErro = "NÃO ENCONTRADO";
                return retornada;
            }
        }
        catch (Exception ex)
        {
            retornada.Sucesso = false;
            retornada.MensagemErro = ex.Message;
            return retornada;
        }        
        finally
        {
            con.Close();
        }
    }

    public static Pessoa AtualizarPropriedades(PessoaPut update, Pessoa pessoa)
    {
        if (update.Nome != null)
            pessoa.Nome = update.Nome;
        if (update.Cpf != null)
            pessoa.Cpf = update.Cpf;
        if (update.DataNascimento != null)
            pessoa.DataNascimento = update.DataNascimento;                
        if (update.Sexo != null)
            pessoa.Sexo = update.Sexo.ToUpper();
        return pessoa;
    }
    
    public static bool VerificarCpf(string cpf) // para validar string cpf enviada na requisição de get by cpf e delete
    {
        if(cpf.Length == 11)
        {
            char[] chars = cpf.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i]))
                    return false; // throw new Exception("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");                
            }            
            return true;
        }
        else return false;
    }
}
