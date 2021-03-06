using System;
using System.Collections.Generic;
using Models;
using MySql.Data.MySqlClient;

public static class DB
{    
    private const string CS = @"server=localhost;userid=root;password=root;database=nodejs";
    private static MySqlConnection con;
    private static MySqlCommand cmd;

    static DB()
    {
        con = new MySqlConnection(CS);
        cmd = new MySqlCommand();
    }
    
    public static Pessoa InserirNoBancoDados(Pessoa p)
    {
        try
        {
            p.Sexo = p.Sexo.ToUpper();            
            con.Open();
            cmd.Connection = con;            
            string query = "INSERT INTO pessoas (nome, cpf, dataNascimento, sexo) VALUES ('" + p.Nome + "', '" + p.Cpf + "', '" + p.DataNascimento + "', '" + p.Sexo + "')";            
            cmd.CommandText = query;
            var linhasAfetadas = cmd.ExecuteNonQuery();
            if (linhasAfetadas != 0)
            {
                p.Sucesso = true;
                return p;
            }
            else
            {
                p.Sucesso = false;
                p.MensagemErro = "NÃO ADICIONADA";
                return p;
            }
        }
        catch (Exception ex)
        {            
            p.MensagemErro = ex.Message;
            p.Sucesso = false;
            return p;    
        }
        finally
        {
            con.Close();
        }
    }

    public static Pessoa AtualizarNoBancoDados(Pessoa p)
    {
        try
        {
            con.Open();
            cmd.Connection = con;
            string query = "UPDATE pessoas SET nome = '" + p.Nome + "', cpf = '" + p.Cpf + "', dataNascimento = '" + p.DataNascimento + "', sexo = '" + p.Sexo + "' WHERE cpf = '" + p.Cpf + "'";                
            cmd.CommandText = query;
            var linhasAfetadas = cmd.ExecuteNonQuery();            
            if (linhasAfetadas != 0)
            {
                p.Sucesso = true;
                return p;
            }
            else
            {
                p.Sucesso = false;
                p.MensagemErro = "NÃO ATUALIZADO";
                return p;
            }
        }
        catch (Exception ex)
        {   
            p.Sucesso = false;
            p.MensagemErro = ex.Message;
            return p;
        }
        finally
        {
            con.Close();
        }             
    }

    public static Pessoa ApagarNoBancoDados(string cpf)
    {
        Pessoa p = DB.GetPessoa(cpf);
        if (p.Sucesso)
        {
            try       
            {
                con.Open();
                cmd.Connection = con;
                string query = "DELETE FROM pessoas WHERE cpf = '" + cpf + "'";            
                cmd.CommandText = query;
                var linhasAfetadas = cmd.ExecuteNonQuery();            
                if (linhasAfetadas != 0)
                {
                    p.Sucesso = true;
                    return p;
                }
                else
                {
                    p.Sucesso = false;
                    p.MensagemErro = "NÃO ENCONTRADO";
                    return p;
                }
            }
            catch (Exception ex)
            {
                p.Sucesso = false;
                p.MensagemErro = ex.Message;
                return p;
            }
            finally
            {
                con.Close();
            }
        }
        else
        {
            p.Sucesso = false;
            p.MensagemErro = "NÃO ENCONTRADO";
            return p;
        }
    }        

    public static PessoaResposta GetListPessoa()
    {
        IList<Pessoa> Pessoa = new List<Pessoa>();    
        PessoaResposta resposta = new PessoaResposta(Pessoa);
        try
        {
            con.Open();
            cmd.Connection = con;
            string query = "SELECT * FROM pessoas";
            cmd.CommandText = query;
            MySqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
            {                                
                while (rdr.Read())
                {
                    Pessoa.Add(new Pessoa
                    {
                        Nome = rdr.GetString(1),
                        Cpf = rdr.GetString(2),
                        DataNascimento = rdr.GetString(3),
                        Sexo = rdr.GetString(4)
                    });
                }
                resposta.Sucesso = true;
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
        Pessoa retornada = new Pessoa();
        try
        {            
            con.Open();
            cmd.Connection = con;
            string query = "SELECT * FROM pessoas WHERE cpf = '" + cpf + "'";
            cmd.CommandText = query;
            MySqlDataReader rdr = cmd.ExecuteReader();
            if(rdr.HasRows)
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

    public static bool ValidarStringCpf(string cpf)
    {
        if(cpf.Length == 11)
        {
            char[] chars = cpf.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i]))
                    return false;
            }            
            return true;
        }
        else return false;
    }
}
