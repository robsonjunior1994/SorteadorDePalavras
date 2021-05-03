using SorteadorDePalavras.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SorteadorDePalavras.Repository
{
    class WordDAO : IWordRepository
    {
        readonly SqlConnection _wordContext;
        public WordDAO()
        {
            
            //_wordContext = new SqlConnection("Server=DESKTOP-L8SMN3M;Database=sorteadordata;Trusted_Connection=true;");
            _wordContext = new SqlConnection("Data Source=DESKTOP-L8SMN3M;Database=sorteadordata;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _wordContext.Open();
        }

        public Word Get(int id)
        {
            try
            {
                id = 1;
                var selectCmd = _wordContext.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Words where @id";

                var paramId = new SqlParameter("id", id);
                selectCmd.Parameters.Add(paramId);

                var resultado = selectCmd.ExecuteReader();


                Word w = new Word(Convert.ToString(resultado["name"]));
                w.Id = Convert.ToInt32(resultado["Id"]);

                return w;
                

            } catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            } finally
            {
                _wordContext.Close();
            }
        }

        public List<Word> GetAll()
        {
            try
            {
                var ListaDeWords = new List<Word>();

                var selectCmd = _wordContext.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Words";

                SqlDataReader result = selectCmd.ExecuteReader();

                while(result.Read())
                {
                    Word w = new Word(result.GetString(1));
                    w.Id = result.GetInt32(0);

                    ListaDeWords.Add(w);
                }

                return ListaDeWords;

            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }
            finally
            {
                //_wordContext.Close();
            }
        }

        public void Save(Word word)
        {
            try
            {
                var insertCmd = _wordContext.CreateCommand();
                insertCmd.CommandText = "INSERT INTO Words (Name) VALUES (@name)";

                var paramName = new SqlParameter("name", word.Name);
                insertCmd.Parameters.Add(paramName);

                insertCmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new SystemException(e.Message, e);
            }

            finally
            {
                _wordContext.Close();
            }
        }
    }
}
