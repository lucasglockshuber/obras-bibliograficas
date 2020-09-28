using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObrasLiterarias.Context;
using ObrasLiterarias.Model;

namespace ObrasLiterarias.Controllers
{
    public class AutorsController : Controller
    {
        private readonly AppDbContext _context;

        public AutorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Autors
        public async Task<IActionResult> Index(int quantidade = 0)
        {
            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorExists(int id)
        {
            return _context.Autores.Any(e => e.Id == id);
        }

        public async Task<List<string>> GetAutoresFormatados(int quantidade)
        {
               var recebeLista = _context.Autores.Take(quantidade).Select(nome => nome.Nome).ToListAsync();
               return FormatarNomes((IEnumerable<Autor>)recebeLista);
            
        }

        private List<string> FormatarNomes(IEnumerable<Autor> autores)
        {
            List<string> nomes = new List<string>();
            foreach (var autor in autores)
            {
                //sobrenome é o ultimo nome e deve ser sempre todo maiusculo
                //Se nao tiver sobrenome é o nome q fica todo maiusculo FEITO
                //se o ultimo nome for NETO,NETA,FILHO etc e tiver um sobrenome antes coloca ele junto
                //o resto do nome deve ser com a primeira letra maiuscula, exceto da,de,do,das etc
                if (string.IsNullOrEmpty(autor.Sobrenome))
                {
                    nomes.Add(autor.Nome.ToUpper());
                }
                else
                {
                    nomes.Add(CriarStringFinal(autor.Sobrenome, autor.Nome));
                }
            }
            return nomes;
        }

        private string CriarStringFinal(string sobrenome, string nome)
        {
            var arrayNomes = sobrenome.Split(' ');
            string nomeFormatado = "";
            int nomesFormatados = 1;
            switch (arrayNomes[arrayNomes.Length - 1].ToLower())
            {
                case "filho":
                case "filha":
                case "neto":
                case "neta":
                case "sobrinho":
                case "sobrinha":
                case "junior":
                    if (arrayNomes.Length > 1)
                    {
                        nomeFormatado = arrayNomes[arrayNomes.Length - 2].ToUpper();
                        nomesFormatados = 2;
                    }
                    nomeFormatado += $" {arrayNomes[arrayNomes.Length - 1].ToUpper()},";
                    break;
                default:
                    nomeFormatado = arrayNomes[arrayNomes.Length - 1].ToUpper() + ",";
                    break;
            }
            nomeFormatado += $" {nome}";
            for (int i = 0; i <= arrayNomes.Length - 1 - nomesFormatados; i++)
            {
                switch (arrayNomes[i].ToLower())
                {
                    case "da":
                    case "de":
                    case "do":
                    case "das":
                    case "dos":
                        nomeFormatado += $" {arrayNomes[i]}";
                        break;
                    default:
                        nomeFormatado += $" {char.ToUpper(arrayNomes[i][0]) + arrayNomes[i].Substring(1)}";
                        break;
                }
            }
            return nomeFormatado;
        }
    }
}
