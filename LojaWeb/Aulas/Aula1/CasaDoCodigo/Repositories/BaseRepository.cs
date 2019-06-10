﻿using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class BaseRepository<T> where T : BaseModel
    {
        protected readonly ApplicationContext contexto;
        // comando para não permitir que elementos duplicados entrem no banco
        protected readonly Microsoft.EntityFrameworkCore.DbSet<T> dbSet;

        public BaseRepository(ApplicationContext contexto)
        {
            this.contexto = contexto;
            // comando para não permitir que elementos duplicados entrem no banco
            dbSet = contexto.Set<T>();
        }

    }
}
