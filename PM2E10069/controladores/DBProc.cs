using PM2E10069.modelo;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PM2E10069.controladores
{
    public class DBProc
    {
        readonly SQLiteAsyncConnection _connection;
        public DBProc() { }
        public DBProc(string dbpath)
        {
            _connection = new SQLiteAsyncConnection(dbpath);
            /* Crear todos mis objetos de base de datos : tablas */
            _connection.CreateTableAsync<Sitio>().Wait();
        }

        /* Crear el CRUD de BD */

        // Create
        public Task<int> AddSitio(Sitio sitio)
        {
            if (sitio.Id == 0)
            {
                return _connection.InsertAsync(sitio);
            }
            else
            {
                return _connection.UpdateAsync(sitio);
            }
        }

        // Read
        public Task<List<Sitio>> GetAll()
        {
            return _connection.Table<Sitio>().ToListAsync();
        }

        public Task<Sitio> GetById(int id)
        {
            return _connection.Table<Sitio>()
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();
        }

        // Delete 
        public Task<int> DeleteSitio(Sitio sitio)
        {
            return _connection.DeleteAsync(sitio);
        }
    }
}
