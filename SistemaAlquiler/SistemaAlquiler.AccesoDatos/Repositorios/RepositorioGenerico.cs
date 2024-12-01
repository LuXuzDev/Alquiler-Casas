using SistemaAlquiler.AccesoDatos.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Web.Mvc;

namespace SistemaAlquiler.AccesoDatos.Repositorios;

public class RepositorioGenerico<TEntity> : IRepositorioGenerico<TEntity> where TEntity : class
{
    private readonly DB_Context db;
    public RepositorioGenerico(DB_Context db)
    {
        this.db = db;
    }
    public async Task<TEntity> obtenerTodos(Expression<Func<TEntity,bool>> filtro)
    {
        try
        {
            TEntity entidad = await db.Set<TEntity>().FirstOrDefaultAsync(filtro);
            return entidad;
        }
        catch
        {
            throw;
        }

    }
    public async Task<TEntity> crear(TEntity entidad)
    {
        try
        {
            db.Set<TEntity>().Add(entidad);
            await db.SaveChangesAsync();
            return entidad;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> editar(TEntity entidad)
    {
        try
        {
            db.Set<TEntity>().Update(entidad);
            await db.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> eliminar(TEntity entidad)
    {
        try
        {
            db.Set<TEntity>().Remove(entidad);
            await db.SaveChangesAsync();
            return true;
        }
        catch
        {
            throw;
        }
    }
    public async Task<IQueryable<TEntity>> obtener(Expression<Func<TEntity, bool>> filtro = null)
    {
        IQueryable<TEntity> consulta = filtro == null ? db.Set<TEntity>() : db.Set<TEntity>().Where(filtro);
        return consulta;
    }

    public async Task<IQueryable<TEntity>> obtener(Expression<Func<TEntity, bool>> filtro, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        try
        {
            IQueryable<TEntity> consulta = filtro == null ? db.Set<TEntity>() : db.Set<TEntity>().Where(filtro);
            foreach (var includeProperty in includeProperties)
            {
                consulta = consulta.Include(includeProperty);
            }
            return consulta;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
