using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CasaDoCodigo.Models;
using CasaDoCodigo.Models.ViewModels;
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

            //Pedido pedido = pedidoRepository.GetPedido();

            List<ItemPedido> itens = pedidoRepository.GetPedido().Itens;
            CarrinhoViewModel carrinhoViewModel = new CarrinhoViewModel(itens);
            return base.View(carrinhoViewModel);
        }

        public IActionResult Cadastro()
        {
            var pedido = pedidoRepository.GetPedido();

            if (pedido == null)
            {
                return RedirectToAction("Carrossel");
            }
            return View(pedido.Cadastro);
        }

        // Esse comando 'HttpPost', impede que acesse a view 'Resumo' digitando o caminho diretamente na url
        // Por exemplo: http://localhost:50080/Pedido/resumo
        [HttpPost]
        //Protegendo uma Action com ValidateAntiForgeryToken
        [ValidateAntiForgeryToken]
        public IActionResult Resumo(Cadastro cadastro)
        {
            //comando para a validação no servidor
            if (ModelState.IsValid)
            {
                //Comando para passar os dados do pedido para a view de Resumo
                Pedido pedido = pedidoRepository.UpdateCadastro(cadastro);
                return View(pedido);
            }
            return RedirectToAction("Cadastro");

            
        }

        [HttpPost]
        // o comando abaixo é usado para proteger contra ataques 
        [ValidateAntiForgeryToken]
        public UpdateQuantidadeResponse UpdateQuantidade([FromBody]ItemPedido itemPedido)
        {
           return pedidoRepository.UpDateQuantidade(itemPedido);
        }
    }
}