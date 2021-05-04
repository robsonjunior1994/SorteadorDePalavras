using SorteadorDePalavras.Models;
using SorteadorDePalavras.Repository;
using SorteadorDePalavras.ResponseRequest;
using System;
using System.Collections.Generic;

namespace SorteadorDePalavras.Services
{
    public class WordService : IWordService
    {
        readonly IWordData _wordRepository;
        public WordService(IWordData wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public Word GetWord()
        {
            var listWord = GetAll();
            var numRandom = new Random();
            Word wordRandom = listWord[numRandom.Next(listWord.Count)];
            return wordRandom;
        }

        public bool Register(WordRequest w)
        {
            Word word = new Word(w.Name);

            if (word.IsValid())
            {
                _wordRepository.Save(word);
                return true;
            }

            return false;
        }

        public List<Word> GetAll()
        {
            List<Word> listWord = new List<Word>();

            listWord = _wordRepository.GetAll();

            return listWord;
        }

        public bool Delete(int id)
        {
            if(id > 0)
            {
                Word w = _wordRepository.Get(id);

                if(w != null)
                {
                    _wordRepository.Delete(id);
                    return true;
                }
                
            }

            return false;
        }

        public bool Edit(WordRequest word)
        {
            Word wordFromTheDataBase = _wordRepository.Get(word.IdExterno);
            if(wordFromTheDataBase != null)
            {
                wordFromTheDataBase.Name = word.Name;
                _wordRepository.Update(wordFromTheDataBase);
                return true;
            }

            return false;
        }
    }
}
