using MySql.Data.MySqlClient;
using System;


namespace Nathalia_Escola.Class
{
    public class Funcoes
    {
        BancoDeDados Banco = new BancoDeDados();
        // login
        public static bool Login(string cpf, string senha)
        {
            string query = "SELECT Count(*) FROM pessoa WHERE cpf = '" + cpf + "' AND Senha = '" + senha + "'";

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, BancoDeDados.conn);
                //Create a data reader and Execute the command
                try
                {
                    //Recebe o numero de usuários encontrados com os parametros enviados
                    int count = int.Parse(cmd.ExecuteScalar().ToString());

                    //close Connection
                    BancoDeDados.CloseConnection();

                    //Se encontrou o usuário no BD seta a resposta para true
                    if (count > 0)
                    {
                        return true;
                    }
                    return false;

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // nivel de acesso
        public static string Nivel_acesso(pessoas usuario)
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT nivel_acesso FROM pessoa");
            sb.AppendLine("WHERE cpf = '" + usuario.cpf + "'");

            string nivel = "";

            //Open Connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);

                //ExecuteScalar will return one value
                nivel = cmd.ExecuteScalar() + "";

                //close Connection
                BancoDeDados.CloseConnection();

                return nivel;
            }
            else
            {
                return nivel;
            }
        }

        // registro aluno
        public static void RegistroUsuario(pessoas Pessoa)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO pessoa");
            sb.AppendLine("(cpf, nome,sobrenome, email, telefone, senha, nivel_acesso)");
            sb.AppendLine("VALUES ('" + Pessoa.cpf + "','" + Pessoa.nome + "','" + Pessoa.sobrenome + "','" + Pessoa.email + "',");
            sb.AppendLine("'" + Pessoa.telefone + "','" + Pessoa.senha + "','" + Pessoa.nivacesso + "')");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();



                //close connection
                BancoDeDados.CloseConnection();
            }
        }

        // registro setor

        public static void RegistroSetores(setores Setor)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO setores (setor)");
            sb.AppendLine("VALUES ('" + Setor.setor + "')");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }
        }

        // registro sala
        public static void RegistroSalas(salas Sala)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO salas (sala)");
            sb.AppendLine("VALUES ('" + Sala.sala + "')");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }
        }

        // registro turma

        public static void RegistroTurmas(turmas Turma)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO turma");
            sb.AppendLine("(nome_curso, turno, id_sala, id_prof)");
            sb.AppendLine("VALUES ('" + Turma.nome_curso + "','" + Turma.turno + "',");
            sb.AppendLine("'" + Turma.idsala + "','" + Turma.idprof + "')");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }
        }

        // registro nota
        public static void RegistroNotas(provas Prova)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO provas (nota, id_matricula)");
            sb.AppendLine("VALUES ('" + Prova.nota + "', '" + Prova.idmatricula + "')");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }
        }

        // matricula

        public static void Matriculas(matriculas Matricula)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("INSERT INTO matricula");
            sb.AppendLine("(id_aluno, id_turma)");
            sb.AppendLine("VALUES ('" + Matricula.idaluno + "','" + Matricula.idturma + "')");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();



                //close connection
                BancoDeDados.CloseConnection();
            }
        }

        // listagem ADMINISTRADORES
        public static List<pessoa> Listagem_Admins()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM pessoa");
            sb.AppendLine("WHERE nivel_acesso = 'administrador'");
            //Create a list to store the result
            List<pessoa> listapessoa = new List<pessoa>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    pessoa listagemadmins = new pessoa();
                    listagemadmins.cpf = dataReader[0] + "";
                    listagemadmins.nome = dataReader[1] + "";
                    listagemadmins.email = dataReader[2] + "";
                    listagemadmins.telefone = dataReader[3] + "";
                    listagemadmins.nivacesso = dataReader[5] + "";

                    listapessoa.Add(listagemadmins);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listapessoa;
            }
            else
            {
                return listapessoa;
            }
        }

        // listagem ALUNOS
        public static List<pessoa> Listagem_Alunos()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM pessoa");
            sb.AppendLine("WHERE nivel_acesso = 'usuario'");
            //Create a list to store the result
            List<pessoa> listaalunos = new List<pessoa>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    pessoa listagemalunos = new pessoa();
                    listagemalunos.cpf = dataReader[0] + "";
                    listagemalunos.nome = dataReader[1] + "";
                    listagemalunos.email = dataReader[2] + "";
                    listagemalunos.telefone = dataReader[3] + "";
                    listagemalunos.nivacesso = dataReader[5] + "";
                    listaalunos.Add(listagemalunos);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaalunos;
            }
            else
            {
                return listaalunos;
            }
        }

        // listagem MATRICULA

        public static List<matricula> Listagem_Matricula()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT matricula.id_matricula, pessoa.nome, pessoa.cpf,");
            sb.AppendLine("turma.id_turma, turma.nome_curso FROM matricula");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("ORDER BY pessoa.nome");

            //Create a list to store the result
            List<matricula> matriculas = new List<matricula>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    matricula matriculados = new matricula();

                    matriculados.idmatricula = dataReader[0].ToString();
                    matriculados.idaluno = dataReader[1].ToString();
                    matriculados.idturma = dataReader[2].ToString();
                    matriculados.extraum = dataReader[3].ToString();
                    matriculados.extradois = dataReader[4].ToString();

                    matriculas.Add(matriculados);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return matriculas;
            }
            else
            {
                return matriculas;
            }
        }

        // listagem PROFESSORES
        public static List<pessoa> Listagem_Professor()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM pessoa");
            sb.AppendLine("WHERE nivel_acesso = 'professor'");
            sb.AppendLine("ORDER BY nome");
            //Create a list to store the result
            List<pessoa> listaprofe = new List<pessoa>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    pessoa listagemprofe = new pessoa();
                    listagemprofe.cpf = dataReader[0] + "";
                    listagemprofe.nome = dataReader[1] + "";
                    listagemprofe.email = dataReader[2] + "";
                    listagemprofe.telefone = dataReader[3] + "";
                    listagemprofe.nivacesso = dataReader[5] + "";


                    listaprofe.Add(listagemprofe);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaprofe;
            }
            else
            {
                return listaprofe;
            }
        }

        // listagem SALAS
        public static List<sala> Listagem_Sala()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT id_sala, numero_sala FROM sala");
            //Create a list to store the result
            List<sala> listasalas = new List<sala>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    sala salas = new sala();
                    salas.idsala = dataReader[0] + "";
                    salas.numsala = dataReader[1] + "";

                    listasalas.Add(salas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listasalas;
            }
            else
            {
                return listasalas;
            }
        }

        // listagem ALUNOS
        public static List<pessoa> Listagem_Alunos_Turma(turma Turma)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT matricula.id_matricula, pessoa.cpf, ");
            sb.AppendLine("pessoa.nome, pessoa.email FROM matricula");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("WHERE turma.id_turma = '" + Turma.idturma + "' ");

            //Create a list to store the result
            List<pessoa> listaalunos = new List<pessoa>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    pessoa listagemalunos = new pessoa();
                    listagemalunos.cpf = dataReader[0] + "";
                    listagemalunos.nome = dataReader[1] + "";
                    listagemalunos.email = dataReader[2] + "";
                    listagemalunos.telefone = dataReader[3] + "";
                    listaalunos.Add(listagemalunos);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaalunos;
            }
            else
            {
                return listaalunos;
            }
        }

        // listagem SETORES

        public static List<setor> Listagem_Setor()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM setor");
            //Create a list to store the result
            List<setor> listasetores = new List<setor>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    setor setores = new setor();
                    setores.idsetor = dataReader[0] + "";
                    setores.nomesetor = dataReader[1] + "";

                    listasetores.Add(setores);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listasetores;
            }
            else
            {
                return listasetores;
            }
        }

        // listagem TURMAS

        public static List<turma> Listagem_Turmas()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT turma.id_turma, turma.nome_curso, turma.turno,");
            sb.AppendLine("pessoa.nome, sala.numero_sala FROM turma");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = turma.id_profe");
            sb.AppendLine("INNER JOIN sala ON sala.id_sala = turma.id_sala");
            //Create a list to store the result
            List<turma> listaturma = new List<turma>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    turma turmas = new turma();
                    turmas.idturma = dataReader[0] + "";
                    turmas.nome_curso = dataReader[1] + "";
                    turmas.turno = dataReader[2] + "";
                    turmas.idprofe = dataReader[3] + "";
                    turmas.idsala = dataReader[4] + "";

                    listaturma.Add(turmas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaturma;
            }
            else
            {
                return listaturma;
            }
        }

        // listagem PROVAS P/ PROFESSOR

        public static List<prova> Lista_Notas(prova Prova)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT provas.id_prova, provas.nota, pessoa.nome, ");
            sb.AppendLine("turma.nome_curso, turma.id_turma FROM provas");
            sb.AppendLine("INNER JOIN matricula ON provas.id_matricula = matricula.id_matricula");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("WHERE turma.id_profe = '" + Prova.extrados + "'");

            //Create a list to store the result
            List<prova> listaprova = new List<prova>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    prova provas = new prova();
                    provas.idprova = dataReader[0] + "";
                    provas.nota = dataReader[1] + "";
                    provas.idmatricula = dataReader[2] + "";
                    provas.idturma = dataReader[3] + "";
                    provas.extra = dataReader[4] + "";

                    listaprova.Add(provas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaprova;

            }
            else
            {
                return listaprova;
            }
        }

        // listagem PROVAS P/ ALUNOS

        public static List<prova> Lista_Notas_user(prova Prova)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT provas.id_prova, provas.nota, ");
            sb.AppendLine("turma.nome_curso, turma.id_turma FROM provas");
            sb.AppendLine("INNER JOIN matricula ON provas.id_matricula = matricula.id_matricula");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("WHERE matricula.id_aluno = '" + Prova.extra + "'");

            //Create a list to store the result
            List<prova> listaprova = new List<prova>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    prova provas = new prova();
                    provas.idprova = dataReader[0] + "";
                    provas.nota = dataReader[1] + "";
                    provas.idmatricula = dataReader[2] + "";
                    provas.idturma = dataReader[3] + "";

                    listaprova.Add(provas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaprova;

            }
            else
            {
                return listaprova;
            }
        }

        // informaçoes usuario
        public static List<pessoa> Info_User(pessoa usuario)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM pessoa");
            sb.AppendLine("WHERE pessoa.cpf = '" + usuario.cpf + "' ");

            //Create a list to store the result
            List<pessoa> listapessoa = new List<pessoa>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    pessoa listagempessoa = new pessoa();
                    listagempessoa.cpf = dataReader[0] + "";
                    listagempessoa.nome = dataReader[1] + "";
                    listagempessoa.email = dataReader[2] + "";
                    listagempessoa.telefone = dataReader[3] + "";
                    listagempessoa.senha = dataReader[4] + "";
                    listagempessoa.nivacesso = dataReader[5].ToString();

                    listapessoa.Add(listagempessoa);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listapessoa;
            }
            else
            {
                return listapessoa;
            }
        }

        // informacoes usuario matricula 
        public static List<matricula> InfoUser_matric(string usercpf)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT turma.id_turma, turma.nome_curso, turma.turno, sala.numero_sala, pessoa.nome");
            sb.AppendLine("FROM matricula INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = turma.id_profe");
            sb.AppendLine("INNER JOIN sala ON sala.id_sala = turma.id_sala");
            sb.AppendLine("WHERE matricula.id_aluno = '" + usercpf + "' ");





            //Create a list to store the result
            List<matricula> matriculas = new List<matricula>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    matricula matriculados = new matricula();

                    matriculados.idmatricula = dataReader[0].ToString();
                    matriculados.idaluno = dataReader[1].ToString();
                    matriculados.idturma = dataReader[2].ToString();
                    matriculados.extraum = dataReader[3].ToString();
                    matriculados.extradois = dataReader[4].ToString();

                    matriculas.Add(matriculados);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return matriculas;
            }
            else
            {
                return matriculas;
            }
        }

        // matricula user especifico
        public static List<matricula> listing_mat(matricula mat)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT matricula.id_matricula, matricula.id_aluno,");
            sb.AppendLine("matricula.id_turma, pessoa.nome, turma.nome_curso FROM matricula");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("WHERE matricula.id_matricula = '" + mat.idmatricula + "' ");


            //Create a list to store the result
            List<matricula> matriculas = new List<matricula>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    matricula matriculados = new matricula();

                    matriculados.idmatricula = dataReader[0].ToString();
                    matriculados.idaluno = dataReader[1].ToString();
                    matriculados.idturma = dataReader[2].ToString();
                    matriculados.extraum = dataReader[3].ToString();
                    matriculados.extradois = dataReader[4].ToString();

                    matriculas.Add(matriculados);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return matriculas;
            }
            else
            {
                return matriculas;
            }
        }

        // nota user especifico
        public static List<prova> Listing_Notas(prova pva)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT provas.id_prova, provas.nota, provas.id_matricula, pessoa.nome, ");
            sb.AppendLine("turma.nome_curso FROM provas");
            sb.AppendLine("INNER JOIN matricula ON provas.id_matricula = matricula.id_matricula");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("WHERE provas.id_prova = '" + pva.idprova + "'");

            //Create a list to store the result
            List<prova> listaprova = new List<prova>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    prova provas = new prova();
                    provas.idprova = dataReader[0] + "";
                    provas.nota = dataReader[1] + "";
                    provas.idmatricula = dataReader[2] + "";
                    provas.idturma = dataReader[3] + "";
                    provas.extra = dataReader[4].ToString();

                    listaprova.Add(provas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaprova;

            }
            else
            {
                return listaprova;
            }
        }

        // matricula professor especifico
        public static List<matricula> listing_mat_prof(matricula mat)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT matricula.id_matricula, pessoa.nome,");
            sb.AppendLine("turma.nome_curso FROM matricula");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = matricula.id_aluno");
            sb.AppendLine("INNER JOIN turma ON turma.id_turma = matricula.id_turma");
            sb.AppendLine("WHERE turma.id_profe = '" + mat.extraum + "' ");


            //Create a list to store the result
            List<matricula> matriculas = new List<matricula>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    matricula matriculados = new matricula();

                    matriculados.idmatricula = dataReader[0].ToString();
                    matriculados.idaluno = dataReader[1].ToString();
                    matriculados.idturma = dataReader[2].ToString();

                    matriculas.Add(matriculados);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return matriculas;
            }
            else
            {
                return matriculas;
            }
        }

        // turma user especifico
        public static List<turma> listing_turma(turma tma)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT * FROM turma");
            sb.AppendLine("WHERE turma.id_turma = '" + tma.idturma + "' ");
            //Create a list to store the result
            List<turma> listaturma = new List<turma>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    turma turmas = new turma();
                    turmas.idturma = dataReader[0] + "";
                    turmas.nome_curso = dataReader[1] + "";
                    turmas.turno = dataReader[2] + "";
                    turmas.idsala = dataReader[3] + "";
                    turmas.idprofe = dataReader[4] + "";

                    listaturma.Add(turmas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaturma;
            }
            else
            {
                return listaturma;
            }
        }

        // turmas por professor
        public static List<turma> InfoProf_turmas(string usercpf)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SELECT turma.id_turma, turma.nome_curso, turma.turno,");
            sb.AppendLine("sala.numero_sala FROM turma");
            sb.AppendLine("INNER JOIN pessoa ON pessoa.cpf = turma.id_profe");
            sb.AppendLine("INNER JOIN sala ON sala.id_sala = turma.id_sala");
            sb.AppendLine("WHERE pessoa.cpf = '" + usercpf + "' ");
            //Create a list to store the result
            List<turma> listaturma = new List<turma>();

            //Open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list

                while (dataReader.Read())
                {
                    turma turmas = new turma();
                    turmas.idturma = dataReader[0] + "";
                    turmas.nome_curso = dataReader[1] + "";
                    turmas.turno = dataReader[2] + "";
                    turmas.idsala = dataReader[3] + "";

                    listaturma.Add(turmas);
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                BancoDeDados.CloseConnection();

                //return list to be displayed
                return listaturma;
            }
            else
            {
                return listaturma;
            }
        }
        // deletar salas

        public static void dltsala(sala Sala)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM sala WHERE");
            sb.AppendLine("sala.id_sala = '" + Sala.idsala + "' ");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }
        // deletar usuarios

        public static void dltuser(pessoa Pessoa)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM pessoa WHERE");
            sb.AppendLine("pessoa.cpf = '" + Pessoa.cpf + "' ");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // deletar setores

        public static void dltsetor(setor Setor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM setor WHERE");
            sb.AppendLine("setor.id_setor = '" + Setor.idsetor + "' ");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // deletar turmas

        public static void dltturma(turma Turma)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM turma WHERE");
            sb.AppendLine("turma.id_turma = '" + Turma.idturma + "' ");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // deletar nota
        public static void dltnota(prova Prova)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM provas WHERE");
            sb.AppendLine("provas.id_prova = '" + Prova.idprova + "' ");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // deletar matricula

        public static void dltmatricula(matricula Matricula)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM matricula WHERE");
            sb.AppendLine("matricula.id_matricula = '" + Matricula.idmatricula + "' ");
            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // editar usuario

        public static void edituser(pessoa Pessoa, pessoa usuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE pessoa SET cpf = '" + Pessoa.cpf + "', nome = '" + Pessoa.nome + "', ");
            sb.AppendLine("email = '" + Pessoa.email + "', telefone = '" + Pessoa.telefone + "', ");
            sb.AppendLine("senha = '" + Pessoa.senha + "', nivel_acesso = '" + Pessoa.nivacesso + "' ");
            sb.AppendLine("WHERE pessoa.cpf = '" + usuario.cpf + "' ");

            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // editar turma

        public static void editturma(turma Turma)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE turma SET nome_curso = '" + Turma.nome_curso + "', ");
            sb.AppendLine("turno = '" + Turma.turno + "', id_sala = '" + Turma.idsala + "', "); ;
            sb.AppendLine("id_profe = '" + Turma.idprofe + " ");
            sb.AppendLine("WHERE turma.id_turma = '" + Turma.idturma + "' ");

            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // editar nota

        public static void editnota(prova Prova)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE provas SET nota = '" + Prova.nota + "', ");
            sb.AppendLine("id_matricula = '" + Prova.idmatricula + "' ");
            sb.AppendLine("WHERE provas.id_prova = '" + Prova.idprova + "' ");

            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }

        // editar matricula

        public static void editmat(matricula Matricula)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE matricula SET id_aluno = '" + Matricula.idaluno + "', id_turma = '" + Matricula.idturma + "' ");
            sb.AppendLine("WHERE matricula.id_matricula = '" + Matricula.idmatricula + "' ");

            //open connection
            if (BancoDeDados.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(sb.ToString(), BancoDeDados.conn);
                cmd.ExecuteReader();
                //close connection
                BancoDeDados.CloseConnection();
            }

        }




    }
}

