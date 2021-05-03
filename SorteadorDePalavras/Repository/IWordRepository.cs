using SorteadorDePalavras.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorteadorDePalavras.Repository
{
    public interface IWordRepository
    {
        void Save(Models.Word word);
        Word Get(int id);
        List<Word> GetAll();
    }
}
