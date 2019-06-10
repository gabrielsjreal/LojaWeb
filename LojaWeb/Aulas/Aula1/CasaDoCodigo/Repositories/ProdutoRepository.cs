using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public IList<Produto> GetProdutos()
        {
            return dbSet.ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            // o foreach é usado pasa instaciar os produtos já com os dados do arquivo .json
            foreach (var livro in livros)
            {
                // o método Any(), verifica se a condição é verdadeira ou não
                // comando para não permitir que elementos duplicados entrem no banco
                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
                }
              
            }
            // comando para salvar as alterações no banco
            contexto.SaveChanges();
        }
    }
}
