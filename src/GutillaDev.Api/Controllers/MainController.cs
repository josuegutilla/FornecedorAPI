using GutillDev.Business.Intefaces;
using GutillDev.Business.Notificacoes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;

namespace GutillaDev.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        public MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao(); //vai na lista de de erros e verifica se tem erros.
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
            {
                return Ok(new //se não tiver notificação
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new //se tiver notificação
            {
                success = false,
                erros = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!ModelState.IsValid) NotificarErroModelInvalida(modelState); //se a modelState não for válida
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(n => n.Errors); //seleciono somente os erros da modelState

            foreach(var erro in erros) //pra cada erro encontrado 
            {
                var errosMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message; //se o erro n tiver excecões restorna só a mensagem, se tiver excecões retorna a mensagem da excecão
                NotificarErros(errosMsg); //passo os erros para o método q add na lista
            }
        }

        protected void NotificarErros(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem)); //add na lista de erros os erros da modelState
        }
    }
}
