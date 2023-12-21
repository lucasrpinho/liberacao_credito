using liberacao_credito.Context;
using liberacao_credito.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace liberacao_credito.Services
{
    public class ClienteService
    {
        readonly DataContext _ctx;

        public ClienteService(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Cliente>> IndexView()
        {
            if (_ctx.Clientes == null) { return null; }
            return await PaginatedList<Cliente>.CreateAsync(_ctx.Clientes, 1, 10);
        }

        public async Task<Cliente> Details(string? id)
        {
            if (id == null || _ctx.Clientes == null)
            {
                return null;
            }

            return await _ctx.Clientes
                .FirstOrDefaultAsync(m => m.CPF == id);
        }

        public async Task<bool> Create(Cliente cliente)
        {
            cliente.Telefone = new string(cliente.Telefone.Where(char.IsDigit).ToArray());
            cliente.UF = cliente.UF.ToUpper();

            try
            {
                _ctx.Add(cliente);
                await _ctx.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public async Task<Cliente> Edit(string? id)
        {
            if (id == null || _ctx.Clientes == null)
            {
                return null;
            }

            return await _ctx.Clientes.FindAsync(id);
        }

        public async Task<bool> Edit(string id, Cliente cliente)
        {
            
            try
            {
                _ctx.Update(cliente);
                await _ctx.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(cliente.CPF))
                {
                    return false;
                }
                else
                {
                    return false;
                    throw;
                }
            }
            return true;
        }

        public async Task<Cliente> Delete(string? id)
        {
            if (id == null || _ctx.Clientes == null)
            {
                return null;
            }

            return await _ctx.Clientes
                .FindAsync(id);
        }

        public async Task<bool> DeleteConfirmed(string? id)
        {
            if (_ctx.Clientes == null)
            {
                return false;
            }

            var cliente = await _ctx.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return false;
            }

            _ctx.Clientes.Remove(cliente);
            await _ctx.SaveChangesAsync();
            return true;
        }

        public bool ClienteExists(string id)
        {
            return (_ctx.Clientes?.Any(e => e.CPF == id)).GetValueOrDefault();
        }

        //QUERYS DE CLIENTES
        //NÃO IMPLEMENTADO NA VIEW
        public async Task<IEnumerable<Cliente>> GetClientesSP_ParcelasPagas()
        {
            return  await _ctx.Clientes
                .Where(c => c.UF == "SP")
                .Select(cliente => new
                {
                    Cliente = cliente,
                    PercentualPago = (double)cliente.Financiamento
                        .SelectMany(financiamento => financiamento.Parcela)
                        .Count(parcela => parcela.DataPagamento != null) * 100.0
                        / cliente.Financiamento
                            .SelectMany(financiamento => financiamento.Parcela)
                            .Count()
                })
                .Where(resultado => resultado.PercentualPago > 60.0)
                .Select(resultado => resultado.Cliente)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> GetClientes_ParcelasEmAtraso()
        {
            return await (
            from c in _ctx.Clientes
            join f in _ctx.Financiamentos on c.CPF equals f.CPF
            join p in _ctx.Parcelas on f.Id equals p.FinanciamentoId
            where p.DataPagamento == null &&
                  p.DataVencimento < DateTime.Now.AddDays(-5)
            group c by new { c.CPF, c.Nome, c.UF, c.Telefone } into grupo
            select new Cliente
            {
                CPF = grupo.Key.CPF,
                Nome = grupo.Key.Nome,
                UF = grupo.Key.UF,
                Telefone = grupo.Key.Telefone
            })
            .Take(4)
            .ToListAsync();
        }
    }
}
