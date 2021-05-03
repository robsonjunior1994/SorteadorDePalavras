using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SorteadorDePalavras.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Word(string name)
        {
            this.Name = name;
        }

        internal bool IsValid()
        {
            if (NameIsValid())
            {
                return true;
            }
            return false;
        }

        private bool NameIsValid()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }

            return true;
        }
    }
}
