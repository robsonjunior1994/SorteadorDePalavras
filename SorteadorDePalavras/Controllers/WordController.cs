using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SorteadorDePalavras.Models;
using SorteadorDePalavras.ResponseRequest;
using SorteadorDePalavras.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SorteadorDePalavras.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        readonly IWordService _wordService;
        public WordController(IWordService wordService)
        {
            _wordService = wordService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            Word word = _wordService.GetWord();
            if(word != null)
            {
                return Ok(word);
            }

            return BadRequest();

        }

        [HttpPost]
        public IActionResult Register(WordRequest wordRequest)
        {
            if (_wordService.Register(wordRequest))
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("All")]
        public IActionResult GetAll()
        {
            List<Word> words = _wordService.GetAll();
            if (words != null)
            {
                return Ok(words);
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (_wordService.Delete(id))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult Edit(WordRequest word)
        {
            if (_wordService.Edit(word))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
