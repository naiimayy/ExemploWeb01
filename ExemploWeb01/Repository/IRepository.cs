using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    interface IRepository
    {
        int Inserir(Fruta fruta);

        bool Apagar(int id);

        bool Atualizar(Fruta fruta);

        Fruta ObterPeloId(int id);

        List<Fruta> ObterTodos(string busca);
    }
}
