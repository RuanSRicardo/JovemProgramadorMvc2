using JovemProgramadorMvc2.Models;
using JovemProgramadorMVC2.Data.Repositorio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace JovemProgramadorMvc2.Controllers
{
    public class AlunoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IAlunoRepositorio _alunorepositorio;

        public AlunoController(IConfiguration configuration, IAlunoRepositorio alunoRepositorio)
        {
            _configuration = configuration;
            _alunorepositorio = alunoRepositorio;
        }
        public IActionResult Index(AlunoModel filtroAluno)
        {
            List<AlunoModel> aluno = new();
            if (filtroAluno.Idade >0)
            {
                aluno = _alunorepositorio.FiltroIdade(filtroAluno.Idade, filtroAluno.Operacao);
            }
            if (filtroAluno.Nome != null)
            {
                aluno = _alunorepositorio.FiltroNome(filtroAluno.Nome);
            }
            if (filtroAluno.Contato != null)
            {
                aluno = _alunorepositorio.FiltroContato(filtroAluno.Contato);
            }

            if(filtroAluno.Id == 0)
            {
                aluno = _alunorepositorio.BuscarAlunos();
            }
            return View(aluno);
        }

        public IActionResult Adicionar()
        {
            return View();
        }
        
        public IActionResult Salvo()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            var aluno = _alunorepositorio.BuscarId(id);
            return View("Editar", aluno);
        }

        public IActionResult Atualizar(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.Atualizar(aluno);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Endereco(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.BuscarId(aluno.Id);
            aluno = retorno;
            EnderecoModel enderecoModel = new();

            try
            {
                var cep = aluno.Cep.Replace("-", "");

                using var client = new HttpClient();
                var result = await client.GetAsync(_configuration.GetSection("ApiCep")["BaseUrl"] + cep + "/json");

                if (result.IsSuccessStatusCode)
                {
                    enderecoModel = JsonSerializer.Deserialize<EnderecoModel>(
                        await result.Content.ReadAsStringAsync(), new JsonSerializerOptions() { });

                    if (enderecoModel.complemento == "")
                    {
                        enderecoModel.complemento = "nenhum";
                    }

                    if (enderecoModel.logradouro == "")
                    {
                        enderecoModel.logradouro = "nenhum";
                    }
                }
                else
                {
                    ViewData["mensagem"] = "Erro na busca do endereço!";
                    return View("Index");
                }

            }
            catch (Exception e)
            {
                ViewData["Mensagem"] = "Erro na requisição!";
                return View("Index");
            }


            return View("Endereco", enderecoModel);
        }

        [HttpPost]
        public IActionResult Inserir(AlunoModel aluno)
        {
            var retorno = _alunorepositorio.Inserir(aluno);
            if (retorno != null)
            {
                TempData["Mensagem2"] = "Dados gravados com sucesso!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult BuscarAlunos(AlunoModel aluno)
        {
            _alunorepositorio.BuscarAlunos();
            return RedirectToAction("Index");
        }

        public IActionResult Excluir(int id)
        {
            var retorno = _alunorepositorio.Excluir(id);
            if (retorno == true)
            {
                TempData["Mensagem3"] = "Aluno excluído com sucesso!";
            }
            else
            {
                TempData["Mensagem3"] = "Aluno não foi excluído!";
            }

            return RedirectToAction("Index");
        }
    }


}
