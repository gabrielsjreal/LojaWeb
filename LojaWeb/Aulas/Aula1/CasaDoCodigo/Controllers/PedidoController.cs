using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CasaDoCodigo.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IProdutoRepository produtoRepository;
        private readonly IPedidoRepository pedidoRepository;
        private readonly IItemPedidoRepository itemPedidoRepository;
        public PedidoController(IProdutoRepository produtoRepository, IPedidoRepository pedidoRepository,
            IItemPedidoRepository itemPedidoRepository)
        {
            this.pedidoRepository = pedidoRepository;
            this.produtoRepository = produtoRepository;
            this.itemPedidoRepository = itemPedidoRepository;
        }

        public IActionResult Carrossel()
        {
           
            return View(produtoRepository.GetProdutos());
        }

        public IActionResult Carrinho(string codigo)
        {

            if (!string.IsNullOrEmpty(codigo))
            {
                pedidoRepository.AddItem(codigo);
            }

            Pedido pedido = pedidoRepository.GetPedido();

            return View(pedido.Itens);
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        public IActionResult Resumo()
        {
            //Comando para passar os dados do pedido para a view de Resumo
            Pedido pedido = pedidoRepository.GetPedido(); 
            return View(pedido);
        }

        [HttpPost]
        public void UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
            itemPedidoRepository.UpDateQuantidade(itemPedido);
        }
    }
}