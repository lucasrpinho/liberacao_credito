using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using liberacao_credito.Context;
using liberacao_credito.Models;
using System.Linq.Expressions;
using liberacao_credito.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace liberacao_credito.Controllers
{
    public class FinanciamentoController : Controller
    {
        private readonly DataContext _context;
        private readonly FinanciamentoService _service;
        private static bool isSuccess = false;
        private decimal _totalCJuros;
        private decimal _valorJuros;

        public FinanciamentoController(FinanciamentoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var dataCtx = _service.IndexView();
            return View(await dataCtx.ToListAsync());
        }

        public async Task<IActionResult> Details(long? id)
        {

            Financiamento financiamento = new Financiamento();
            await _service.BuscaFinanciamento(id, financiamento);

            if (financiamento == null)
            {
                return NotFound();
            }

            return View(financiamento);
        }

        public IActionResult Create()
        {
            PreencherDadosDropDownLists();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CPF,ValorTotal,DataVencimentoPrimeiro,QtdParcelas,TipoCreditoId")] Financiamento financiamento)
        {
            if (ModelState.IsValid)
            {
                var validacaoCredito = ValidarFinanciamento(financiamento);

                if (validacaoCredito.Any())
                {
                    ViewBag.FinanciamentoAnalise = validacaoCredito;
                    ViewBag.isSuccess = false;
                    PreencherDadosDropDownLists(financiamento);
                    return View(financiamento);
                }
                else
                {
                    _totalCJuros = _service.RetornaTotalCJuros(financiamento);
                    _valorJuros = _totalCJuros - financiamento.ValorTotal;

                    isSuccess = true;
                    ViewBag.isSuccess = isSuccess;
                    ViewBag.totalCJuros = _totalCJuros;
                    ViewBag.valorJuros = _valorJuros;
                    var succ = await _service.SalvarFinanciamento(financiamento);
                    if (succ)
                    {
                        PreencherDadosDropDownLists();
                        return View(new Financiamento());
                    }
                    else
                    {
                        isSuccess = false;
                        PreencherDadosDropDownLists(financiamento);
                        return StatusCode(500, "Erro interno ao processar liberação de crédito.");
                    }
                }
            }
            return View(financiamento);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            var financiamento = await _service.Edit(id);

            if (financiamento == null)
            {
                return NotFound();
            }

            PreencherDadosDropDownLists(financiamento);
            return View(financiamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,CPF,ValorTotal,DataVencimentoUltimo,TipoCreditoId")] Financiamento financiamento)
        {
            if (id != financiamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var succ = await _service.Edit(financiamento);
                return succ == true ? RedirectToAction(nameof(Index)) : NotFound();            
            }

            PreencherDadosDropDownLists(financiamento);
            return View(financiamento);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            var financiamento = await _service.Remoção(id);

            if (financiamento == null) {
                return NotFound(); 
            }

            return View(financiamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var removeu = await _service.ConfirmarRemoção(id);
            return removeu == true ? RedirectToAction(nameof(Index)) : Problem("Não existem financiamentos para remover.");
        }

        private bool FinanciamentoExists(long id)
        {
            return _service.FinanciamentoExists(id);
        }

        #region "custom" 
        private void PreencherDadosDropDownLists(Financiamento financiamento)
        {
            ViewData["CPF"] = _service.RetornaListaView(financiamento.CPF);
            ViewData["TipoCreditoId"] = _service.RetornaListaView(financiamento.TipoCreditoId);
        }

        private void PreencherDadosDropDownLists()
        {
            var dict = _service.RetornaListaView();
            ViewData["CPF"] = dict.First(p => p.Key == "CPF").Value;
            ViewData["TipoCreditoId"] = dict.First(p => p.Key == "TipoCreditoId").Value;
        }

        public class FinanciamentoAnalise
        {
            public string ErrorMsg { get; set; }

            public FinanciamentoAnalise(string errorMsg)
            {
                ErrorMsg = errorMsg;
            }
        }
        private List<FinanciamentoAnalise> ValidarFinanciamento(Financiamento financiamento)
        {
            var errors = new List<FinanciamentoAnalise>();

            if (financiamento.ValorTotal > 1000000)
            {
                errors.Add(new FinanciamentoAnalise("O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00."));
            }

            if (financiamento.QtdParcelas < 5 || financiamento.QtdParcelas > 72)
            {
                errors.Add(new FinanciamentoAnalise("A quantidade de parcelas deve ser entre 5 e 72."));
            }

            if (financiamento.TipoCreditoId == 3 && financiamento.ValorTotal < 15000)
            {
                errors.Add(new FinanciamentoAnalise("Para pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00."));
            }

            var dataMinima = DateTime.Now.AddDays(15);
            var dataMaxima = DateTime.Now.AddDays(40);

            if (financiamento.DataVencimentoPrimeiro.Date < dataMinima.Date || financiamento.DataVencimentoPrimeiro.Date > dataMaxima.Date)
            {
                errors.Add(new FinanciamentoAnalise($"A data do primeiro vencimento deve estar entre {dataMinima.ToShortDateString()} e {dataMaxima.ToShortDateString()}."));
            }

            return errors;
        }
        #endregion
    }
}
