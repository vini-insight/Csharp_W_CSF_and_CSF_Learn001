using System;
using System.Collections.Generic;
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
        // catch (System.Exception)
        // {
        //     throw; // relança exceção e preserva a pilha stack trace
        // }
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

    // public static string BuscarNoBancoDados() // RETORNA TODAS AS PESSOAS NO BD
    public static List<Pessoa> BuscarNoBancoDados() // RETORNA TODAS AS PESSOAS NO BD
    {
        using var con = new MySqlConnection(CS);        
        // Pessoa aux = new Pessoa();
        List<Pessoa> pessoas = new List<Pessoa>();
        try
        {
            con.Open();
            string sql = "SELECT * FROM pessoas";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();
            // string consulta = "";
            // if(rdr.HasRows) // se pesquisa bem sucedida
            // {                                
                while (rdr.Read())
                {
                    // Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetChar(4));
                    // consulta += "" + rdr.GetString(1) + " " + rdr.GetString(2) + " " + rdr.GetString(3) + " " + rdr.GetChar(4) + "\n";
                    // aux.Nome = rdr.GetString(1);
                    // aux.Cpf = rdr.GetString(2);
                    // aux.DataNascimento = rdr.GetString(3);                    
                    // aux.Sexo = rdr.GetString(4);
                    // new Pessoa { Nome = rdr.GetString(1), Cpf = rdr.GetString(2), DataNascimento = rdr.GetString(3), Sexo = rdr.GetString(4) };

                    pessoas.Add(new Pessoa { Nome = rdr.GetString(1), Cpf = rdr.GetString(2), DataNascimento = rdr.GetString(3), Sexo = rdr.GetString(4) });
                }
                // return consulta;
                return pessoas;             
        }
        catch (System.Exception)        
        {
            throw; // relança exceção e preserva a pilha stack trace
        }
        finally
        {
            con.Close();
        }
    }

    // public static Pessoa GetPessoa(Pessoa p)
    public static Pessoa GetPessoa(String cpf)
    {
        using var con = new MySqlConnection(CS);
        try
        {            
            con.Open();                        
            string sql = "SELECT * FROM pessoas WHERE cpf = '" + cpf + "'";
            using var cmd = new MySqlCommand(sql, con);
            using MySqlDataReader rdr = cmd.ExecuteReader();            
            Pessoa retornada = new Pessoa();            
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
            // else throw new Exception("NÃO ENCONTRADO");
            else return new Pessoa { Sucesso = false, MensagemErro = "NÃO ENCONTRADO"};
        }
        catch (System.Exception)
        {
            throw; // relança exceção e preserva a pilha stack trace
        }
        finally
        {
            con.Close();
        }
    }

    public static Pessoa AtualizarPropriedades(PessoaPut update, Pessoa pessoa)
    {
        if (update.Nome != null) pessoa.Nome = update.Nome;
        if (update.Cpf != null) pessoa.Cpf = update.Cpf;
        if (update.DataNascimento != null) pessoa.DataNascimento = update.DataNascimento;                
        if (update.Sexo != null) pessoa.Sexo = update.Sexo.ToUpper();
        return pessoa;
    }
    
    public static bool VerificarCpf(string cpf) // para validar string cpf enviada na requisição de get by cpf e delete
    {
        if(cpf.Length == 11)
        {
            char[] chars = cpf.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (!char.IsDigit(chars[i])) return false; // throw new Exception("DIGITE APENAS OS NUMEROS, sem pontos, virugulas, traços, espaços nem letras");                
            }            
            return true;
        }
        else return false;
    }
}
