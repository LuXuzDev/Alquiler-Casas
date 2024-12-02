using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using SistemaAlquiler.Entidades;

namespace SistemaAlquiler.AccesoDatos.Interfaces;

    public interface IRepositorioGenerico<TEntity> where TEntity : class
    {
        Task<TEntity> obtenerTodos(Expression<Func<TEntity, bool>> filtro);
        Task<TEntity> crear(TEntity entidad);
        Task<bool> editar(TEntity entidad);
        Task<bool> eliminar(TEntity entidad);
        Task<IQueryable<TEntity>> obtener(Expression<Func<TEntity, bool>> filtro = null);
        Task<IQueryable<TEntity>> obtener(Expression<Func<TEntity, bool>>? filtro = null, params Expression<Func<TEntity, object>>[] includeProperties);

}

