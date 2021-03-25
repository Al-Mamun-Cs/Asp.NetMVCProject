using AuthATMProject.Models.Interface;
using AuthATMProject.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AuthATMProject.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void DeleteClient(int id)
        {
            Client delObj = GetClientById(id);
            db.Clients.Remove(delObj);
            db.SaveChanges();
        }

        public List<AccountsType> GetAccounts()
        {
            List<AccountsType> cateList = db.Account.ToList();
            return cateList;
        }

        public Client GetClientById(int id)
        {
            Client obj = db.Clients.SingleOrDefault(p => p.ClientId == id);
            return obj;
        }

        public List<ClientListViewModel> GetClients()
        {
            List<ClientListViewModel> viewlist = new List<ClientListViewModel>();
            viewlist = db.Clients.Select(p => new ClientListViewModel
            {
                ClientId = p.ClientId,
                ClientName = p.ClientName,
                DateOfBirth = p.DateOfBirth,
                Email = p.Email,
                Phone = p.Phone,
                IsActive = p.IsActive,
                ImageName = p.ImageName,
                ImageURL = p.ImageURL,
                AccountsTitle = p.Account.AccountsTitle,
                AccountsTypeId = p.AccountsTypeId
            }).ToList();
            return viewlist;
        }

        public void SaveClient(Client obj)
        {
            db.Clients.Add(obj);
            db.SaveChanges();
        }

        public void UpdateClient(Client obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }
    }
}