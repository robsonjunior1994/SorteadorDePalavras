using SorteadorDePalavras.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SorteadorDePalavras.Repository
{
    class WordDAO : IWordData
    {
        readonly SqlConnection _wordContext;
        public WordDAO()
        {
            
            //_wordContext = new SqlConnection("Server=DESKTOP-L8SMN3M;Database=sorteadordata;Trusted_Connection=true;");
            _wordContext = new SqlConnection("Data Source=DESKTOP-L8SMN3M;Database=sorteadordata;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            _wordContext.Open();
        }

        public void Delete(int id)
        {
            try
            {
                _wordContext.Open();
                var deleteCmd = _wordContext.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM Words WHERE id = @id";

                var paramId = new SqlParameter("id", id);
                deleteCmd.Parameters.Add(paramId);

                deleteCmd.ExecuteNonQuery();

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

        public Word Get(int id)
        {
            try
            {
                Word w = null;
                var selectCmd = _wordContext.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Words where id = @id";

                var paramId = new SqlParameter("id", id);
                selectCmd.Parameters.Add(paramId);

                var resultado = selectCmd.ExecuteReader();

                while (resultado.Read())
                {
                   w = new Word(Convert.ToString(resultado["name"]));
                    w.Id = Convert.ToInt32(resultado["Id"]);
                }

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
                _wordContext.Close();
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

        public void Update(Word w)
        {
            try
            {
                _wordContext.Open();
                var updateCmd = _wordContext.CreateCommand();
                updateCmd.CommandText = "UPDATE Words SET Name = @Name WHERE id = @Id";

                var paramId = new SqlParameter("Id", w.Id);
                updateCmd.Parameters.Add(paramId);

                var paramName = new SqlParameter("Name", w.Name);
                updateCmd.Parameters.Add(paramName);

                updateCmd.ExecuteNonQuery();
            } catch(SqlException e)
            {
                throw new SystemException(e.Message, e);
            } finally
            {
                _wordContext.Close();
            }
        }
    }
}
