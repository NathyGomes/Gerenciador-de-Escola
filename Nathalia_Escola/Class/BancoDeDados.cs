﻿using MySql.Data.MySqlClient;
using System;


namespace Nathalia_Escola.Class
{
    public class BancoDeDados
    {
            private static MySqlConnection conn;
            private static string server;
            private static string database;
            private static string uid;
            private static string password;

            //Constructor
            public static void DBConnect()
            {
                Initialize();
            }

            //Initialize values
            private static void Initialize()
            {
                //server = "localhost";
                server = "localhost";
                //database = "connectcsharptomysql";
                database = "db_nathalia";
                //uid = "username";
                uid = "localhost";
                //password = "password";
                password = "";
                string myConnectionString;
                myConnectionString = "SERVER=" + server + ";" + "DATABASE=" +
                database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

                try
                {
                    conn = new MySqlConnection();
                    conn.ConnectionString = myConnectionString;
                    //conn.Open();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    //MessageBox.Show(ex.Message);
                }
            }

            //open connection to database
            private static bool OpenConnection()
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (MySqlException ex)
                {
                    //When handling errors, you can your application's response based 
                    //on the error number.
                    //The two most common error numbers when connecting are as follows:
                    //0: Cannot connect to server.
                    //1045: Invalid user name and/or password.
                    switch (ex.Number)
                    {
                        case 0:
                            Console.WriteLine("Cannot connect to server.  Contact administrator");
                            break;

                        case 1045:
                            Console.WriteLine("Invalid username/password, please try again");
                            break;
                    }
                    return false;
                }

            }

            //Close connection
            private static bool CloseConnection()
            {
                try
                {
                    conn.Close();
                    return true;
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            // REGISTRAR USUARIO

            public static void Cadastro_Usuario(usuario usuario)
            {
                string query = "INSERT INTO usuario(nome, sobrenome, email, senha,celular, genero) VALUES ( '" + usuario.nome + "','" + usuario.sobrenome + "','" + usuario.email + "','"+ usuario.senha + "','" + usuario.celular + "','" + usuario.genero + "')";
                //open connection
                if (OpenConnection() == true)
                {
                    //create command and assign the query and connection from the constructor
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    //Execute command
                    cmd.ExecuteNonQuery();

                    //close connection
                    CloseConnection();
                }
            }

        public static bool Login(String email, string senha)
            {
            string query = "SELECT Count(*) FROM USUARIO WHERE Email = '" + email + "' AND Senha = '" + senha + "'";

            //Open connection
            if (OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, conn);
                //Create a data reader and Execute the command
                try
                {
                    //Recebe o numero de usuários encontrados com os parametros enviados
                    int count = int.Parse(cmd.ExecuteScalar() + "");

                    //close Connection
                    CloseConnection();

                    //Se encontrou o usuário no BD seta a resposta para true
                    if (count > 0){
                     return true;
                    }
                    return false;
                }
                catch (MySqlException ex){
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else{
               return false;
            }
        }

    }
}