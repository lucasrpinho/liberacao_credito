using liberacao_credito.Context;
using liberacao_credito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace liberacao_credito.Services
{
    public class FinanciamentoService
    {
        readonly DataContext _ctx;

        public FinanciamentoService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Financiamento> BuscaFinanciamento(long? id, Financiamento? financiamento)
        {
            if (id == null)
            {
                return null;
            }

            financiamento = await _ctx.Financiamentos
                .Include(f => f.TipoCredito)
            .Include(f => f.Parcela)
            .Include(f => f.Cliente)
            .FirstOrDefaultAsync(f => f.Id == id);

            return financiamento;
        }

        public async void CriaParcelas(Financiamento financiamento)
        {
            Parcela parcela;
            for (byte i = 1; i == financiamento.QtdParcelas; i++)
            {
                parcela = new Parcela
                {
                    FinanciamentoId = financiamento.Id,
                    NumParcela = i,
                    ValorParcela = financiamento.ValorTotal / financiamento.QtdParcelas,
                    DataVencimento = financiamento.DataVencimentoPrimeiro.AddMonths(i - 1)
                };
                _ctx.Parcelas.Add(parcela);
            }
            await _ctx.SaveChangesAsync();
        }

        public Financiamento PersonalizarFinanciamento(Financiamento financiamento)
        {
            financiamento.DataVencimentoUltimo = financiamento.DataVencimentoPrimeiro.AddMonths(financiamento.QtdParcelas - 1);
            return financiamento;
        }

        public decimal RetornaTotalCJuros(Financiamento financiamento)
        {
            decimal tempo = (decimal)financiamento.QtdParcelas / 12;

            decimal juros = financiamento.ValorTotal * (_ctx.TipoCreditos.First(p => p.Id == financiamento.TipoCreditoId).Taxa / 100) * tempo;

            return financiamento.ValorTotal + juros;

        }

        public bool FinanciamentoExists(long id)
        {
            return (_ctx.Financiamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<bool> ConfirmarRemoção(long id)
        {
            if (_ctx.Financiamentos == null)
            {
                return false;
            }
            var financiamento = await _ctx.Financiamentos.FindAsync(id);
            if (financiamento != null)
            {
                _ctx.Financiamentos.Remove(financiamento);
            }

            await _ctx.SaveChangesAsync();

            return true;
        }

        public async Task<Financiamento> Remoção(long? id)
        {
            if (id == null || _ctx.Financiamentos == null)
            {
                return null;
            }

            var financiamento = await _ctx.Financiamentos
                .Include(f => f.Cliente)
                .Include(f => f.TipoCredito)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (financiamento == null)
            {
                return null;
            }

            return financiamento;
        }

        public async Task<Financiamento> Edit(long? id)
        {
            if (id == null || _ctx.Financiamentos == null)
            {
                return null;
            }

            var financiamento = await _ctx.Financiamentos.FindAsync(id);
            if (financiamento == null)
            {
                return null;
            }

            return financiamento;
        }

        public async Task<bool> Edit(Financiamento financiamento)
        {
            try
            {
                _ctx.Update(financiamento);
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinanciamentoExists(financiamento.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public SelectList RetornaListaView(string cpf)
        {
            return new SelectList(_ctx.Clientes, "CPF", "CPF", cpf);
        }

        public SelectList RetornaListaView(int TipoCreditoId)
        {
            return new SelectList(_ctx.TipoCreditos, "Id", "Descricao", TipoCreditoId);
        }

        public IDictionary<string, SelectList> RetornaListaView()
        {
            var dict = new Dictionary<string, SelectList>
            {
                { "CPF", new SelectList(_ctx.Clientes, "CPF", "CPF") },
                { "TipoCreditoId", new SelectList(_ctx.TipoCreditos, "Id", "Descricao") }
            };

            return dict;
        }

        public async Task<bool> SalvarFinanciamento(Financiamento financiamento)
        {
            var tr = _ctx.Database.BeginTransaction();
            try
            {
                PersonalizarFinanciamento(financiamento);
                _ctx.Add(financiamento);
                await _ctx.SaveChangesAsync();
                CriaParcelas(financiamento);
                tr.Commit();
                return true;
            }
            catch
            {
                tr.Rollback();
                return false;
            }
        }

        public IIncludableQueryable<Financiamento, TipoCredito?> IndexView()
        {
            var result = _ctx.Financiamentos.Include(f => f.Cliente).Include(f => f.TipoCredito);
            return result;
        }

        public List<string> ValidarFinanciamento(Financiamento financiamento)
        {
            var ErrorMsg = new List<string>();

            if (financiamento.ValorTotal > 1000000)
            {
                ErrorMsg.Add("O valor máximo a ser liberado para qualquer tipo de empréstimo é de R$ 1.000.000,00.");
            }

            if (financiamento.QtdParcelas < 5 || financiamento.QtdParcelas > 72)
            {
                ErrorMsg.Add("A quantidade de parcelas deve ser entre 5 e 72.");
            }

            if (financiamento.TipoCreditoId == 3 && financiamento.ValorTotal < 15000)
            {
                ErrorMsg.Add("Para pessoa jurídica, o valor mínimo a ser liberado é de R$ 15.000,00.");
            }

            var dataMinima = DateTime.Now.AddDays(15);
            var dataMaxima = DateTime.Now.AddDays(40);

            if (financiamento.DataVencimentoPrimeiro.Date < dataMinima.Date || financiamento.DataVencimentoPrimeiro.Date > dataMaxima.Date)
            {
                ErrorMsg.Add($"A data do primeiro vencimento deve estar entre {dataMinima.ToShortDateString()} e {dataMaxima.ToShortDateString()}.");
            }

            return ErrorMsg;
        }
    }
}
