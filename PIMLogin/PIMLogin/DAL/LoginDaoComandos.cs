using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PIMLogin.DAL
{
    class LoginDaoComandos
    {
        public bool tem =false;
        public string mensagem ="";//tudo ok
        SqlCommand cmd = new SqlCommand();
        Conexao con = new Conexao();
        SqlDataReader dr;
        public bool verificarLogin(String login, string senha)
        {
            //comandos sql para verificar se tem no banco esse login e senha

            cmd.CommandText = "select * from logins where email = @login and senha = @senha";
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@senha", senha);
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)// se foi encontrado
                {
                    tem = true;
                }
                con.desconectar();
                dr.Close();

            }
            catch(SqlException)
            {
                this.mensagem = "Erro com Banco de Dados!";
            }

            return tem;
        }
            
        public string cadastrar(string email, string senha, string confsenha)
        {
            tem = false;
            //comandos para inserir 
            if(senha.Equals(confsenha))
            {
                cmd.CommandText = "insert into logins values (@e,@s);";
                cmd.Parameters.AddWithValue("@e", email);
                cmd.Parameters.AddWithValue("@s", senha);
                try
                {
                    
                    cmd.Connection = con.Conectar();
                    cmd.ExecuteNonQuery();
                    con.desconectar();
                    this.mensagem = "Cadastrado com sucesso!";
                    tem = true;


                }catch(SqlException)
                {
                    this.mensagem = "erro com Banco de Dados";
                }
            }else
            {
                this.mensagem = "Senhas não correspondem!";
            }
           
            return mensagem;
        }
    }
}
