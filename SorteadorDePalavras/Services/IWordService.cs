using SorteadorDePalavras.Models;
using SorteadorDePalavras.ResponseRequest;
using System.Collections.Generic;

namespace SorteadorDePalavras.Services
{
    public interface IWordService
    {
        bool Register(WordRequest word);
        Word GetWord();
        List<Word> GetAll();
    }
}
