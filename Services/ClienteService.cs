using liberacao_credito.Context;
using liberacao_credito.Models;
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
            return await _ctx.Clientes.ToListAsync();
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
    }
}
