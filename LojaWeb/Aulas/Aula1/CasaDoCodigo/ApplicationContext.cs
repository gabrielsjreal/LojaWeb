using CasaDoCodigo.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // comandos para o Entity localizar a classe e gerar os comandos para a criação do banco de dados
            // o  método 'HasKey' é para definir que a classe contém uma chave primária
            modelBuilder.Entity<Produto>().HasKey(t => t.Id);

            modelBuilder.Entity<Pedido>().HasKey(t => t.Id);
            // o comando abaixo é para definir o relacionamento das tabelas, nesse caso abaixo por exemplo - n x n(muitos)
            modelBuilder.Entity<Pedido>().HasMany(t => t.Itens).WithOne(t => t.Pedido);
            // o comando abaixo é para definir o relacionamento das tabelas, nesse caso abaixo por exemplo - 1x1
            modelBuilder.Entity<Pedido>().HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();

            modelBuilder.Entity<ItemPedido>().HasKey(t => t.Id);
            modelBuilder.Entity<ItemPedido>().HasOne(t => t.Pedido);
            modelBuilder.Entity<ItemPedido>().HasOne(t => t.Produto);

            modelBuilder.Entity<Cadastro>().HasKey(t => t.Id);
            modelBuilder.Entity<Cadastro>().HasOne(t => t.Pedido);
        }
    }
}
