using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using liberacao_credito.Context;
using liberacao_credito.Models;
using liberacao_credito.Services;

namespace liberacao_credito.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ClienteService _service;

        public ClientesController(ClienteService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var clientes = await _service.IndexView();
            return clientes != null ? View(clientes) : Problem("A entidade de clientes não foi configurada na base de dados.");
        }

        public async Task<IActionResult> Details(string id)
        {
            var cliente = await _service.Details(id);
            return cliente != null ? View(cliente) : NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CPF,Nome,UF,Telefone")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _service.Create(cliente);                
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var cliente = await _service.Edit(id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CPF,Nome,UF,Telefone")] Cliente cliente)
        {
            if (id != cliente.CPF)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var succ = await _service.Edit(id, cliente);
                return succ == true ? RedirectToAction(nameof(Index)) : NotFound();
            }

            return View(cliente);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var cliente = await _service.Delete(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var succ = await _service.DeleteConfirmed(id);
            return succ == true ? RedirectToAction(nameof(Index)) : Problem("O cliente não foi encontrado ou a base de dados está sem informações de cliente.");
        }

        private bool ClienteExists(string id)
        {
            return _service.ClienteExists(id);
        }
    }
}
