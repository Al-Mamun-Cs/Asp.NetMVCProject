using AuthATMProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthATMProject.Models.Interface
{
    public interface IClientRepository
    {
        List<ClientListViewModel> GetClients();
        Client GetClientById(int id);
        void SaveClient(Client obj);
        void UpdateClient(Client obj);
        void DeleteClient(int id);
        List<AccountsType> GetAccounts();
    }
}
