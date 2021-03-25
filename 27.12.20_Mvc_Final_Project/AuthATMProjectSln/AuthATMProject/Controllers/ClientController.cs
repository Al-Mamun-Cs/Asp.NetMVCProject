using AuthATMProject.Models;
using AuthATMProject.Models.Repositories;
using AuthATMProject.Models.ViewModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthATMProject.Controllers
{
    public class ClientController : Controller
    {
        ClientRepository repoObj = new ClientRepository();
        public ActionResult Index(string SearchString, string CurrentFilter, string SortOrder, int? Page)
        {
            ViewBag.SortNameParam = string.IsNullOrEmpty(SortOrder) ? "name_desc" : "";
            if (SearchString != null)
            {
                Page = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }
            ViewBag.CurrentFilter = SearchString;
            List<ClientListViewModel> Clist = repoObj.GetClients();
            if (!string.IsNullOrEmpty(SearchString))
            {
                Clist = Clist.Where(n => n.ClientName.ToUpper()
                .Contains(SearchString.ToUpper())).ToList();
            }
            switch (SortOrder)
            {
                case "name_desc":
                    Clist = Clist.OrderByDescending(n => n.ClientName).ToList();
                    break;
                default:
                    Clist = Clist.OrderBy(n => n.ClientName).ToList();
                    break;

            }
            int PageSize = 3;
            int PageNumber = (Page ?? 1);
            return View("Index", Clist.ToPagedList(PageNumber, PageSize));
           
            
        }
        //public ActionResult Index()
        //{
        //    List<ClientListViewModel> Clist = repoObj.GetClients();
        //    return View("Index", Clist);
        //}
        [HttpGet]
        public ActionResult Create()
        {
            CreateClientViewModel obj = new CreateClientViewModel();
            List<AccountsType> list = repoObj.GetAccounts().ToList();
            obj.AcList = list;
            return View(obj);
        }
        [HttpPost]
        public ActionResult AddOrEdit(CreateClientViewModel viewobj)
        {
            var result = false;
            Client ClObj = new Client();
            if (ModelState.IsValid)
            {
                if (viewobj.ClientId == 0)
                {
                    ClObj.ClientName = viewobj.ClientName;
                    ClObj.DateOfBirth = viewobj.DateOfBirth;
                    ClObj.Email = viewobj.Email;
                    ClObj.Phone = viewobj.Phone;
                    ClObj.IsActive = viewobj.IsActive;
                    ClObj.AccountsTypeId = viewobj.AccountsTypeId;
                    string fileName = Path.GetFileNameWithoutExtension(viewobj.ImageFile.FileName);
                    string extension = Path.GetExtension(viewobj.ImageFile.FileName);
                    string fileWithExtension = fileName + extension;
                    ClObj.ImageURL = "~/Images/" + fileWithExtension;
                    ClObj.ImageName = fileWithExtension;
                    string fileWithServerPath = Path.Combine(Server.MapPath("~/Images/" + fileName + extension));
                    viewobj.ImageFile.SaveAs(fileWithServerPath);
                    repoObj.SaveClient(ClObj);
                    result = true;
                }
                else
                {
                    ClObj = repoObj.GetClientById(viewobj.ClientId);
                    ClObj.ClientName = viewobj.ClientName;
                    ClObj.DateOfBirth = viewobj.DateOfBirth;
                    ClObj.Email = viewobj.Email;
                    ClObj.Phone = viewobj.Phone;
                    ClObj.IsActive = viewobj.IsActive;
                    ClObj.AccountsTypeId = viewobj.AccountsTypeId;
                    string fileName = Path.GetFileNameWithoutExtension(viewobj.ImageFile.FileName);
                    string extension = Path.GetExtension(viewobj.ImageFile.FileName);
                    string fileWithExtension = fileName + extension;
                    ClObj.ImageURL = "~/Images/" + fileWithExtension;
                    ClObj.ImageName = fileWithExtension;
                    string fileWithServerPath = Path.Combine(Server.MapPath("~/Images/" + fileName + extension));
                    viewobj.ImageFile.SaveAs(fileWithServerPath);
                    repoObj.UpdateClient(ClObj);
                    result = true;
                }
            }
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                CreateClientViewModel obj = new CreateClientViewModel();
                List<AccountsType> list = repoObj.GetAccounts().ToList();
                obj.AcList = list;
                return View("Create", obj);
            }
        }
        [HttpGet]

        public ActionResult Edit(int id)
        {
            Client ClObj = repoObj.GetClientById(id);
            CreateClientViewModel viewObj = new CreateClientViewModel();
            viewObj.ClientId = ClObj.ClientId;
            viewObj.ClientName = ClObj.ClientName;
            viewObj.DateOfBirth = ClObj.DateOfBirth;
            viewObj.Email = ClObj.Email;
            viewObj.Phone = ClObj.Phone;
            viewObj.IsActive = ClObj.IsActive;
            viewObj.ImageName = ClObj.ImageName;
            viewObj.ImageURL = ClObj.ImageURL;
            viewObj.AccountsTypeId = ClObj.AccountsTypeId;
            viewObj.AcList = repoObj.GetAccounts();
            return View(viewObj);
        }
        public ActionResult Delete(int id)
        {
            Client ClObj = repoObj.GetClientById(id);
            CreateClientViewModel viewObj = new CreateClientViewModel();
            viewObj.ClientId = ClObj.ClientId;
            viewObj.ClientName = ClObj.ClientName;
            viewObj.DateOfBirth = ClObj.DateOfBirth;
            viewObj.Email = ClObj.Email;
            viewObj.Phone = ClObj.Phone;
            viewObj.IsActive = ClObj.IsActive;
            viewObj.ImageName = ClObj.ImageName;
            viewObj.ImageURL = ClObj.ImageURL;
            viewObj.AccountsTypeId = ClObj.AccountsTypeId;


            return View(viewObj);
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Client proObj = repoObj.GetClientById(id);
            if (proObj != null)
            {
                repoObj.DeleteClient(proObj.ClientId);
                return RedirectToAction("Index");
            }

            return View();
        }
        public PartialViewResult Details(int ClientId)
        {
            Client CObj = repoObj.GetClientById(ClientId);
            ClientListViewModel viewObj = new ClientListViewModel();
            viewObj.ClientId = CObj.ClientId;
            viewObj.ClientName = CObj.ClientName;
            viewObj.DateOfBirth = CObj.DateOfBirth;
            viewObj.Email = CObj.Email;
            viewObj.Phone = CObj.Phone;
            viewObj.IsActive = CObj.IsActive;
            viewObj.ImageName = CObj.ImageName;
            viewObj.ImageURL = CObj.ImageURL;
            viewObj.AccountsTypeId = CObj.AccountsTypeId;
            return PartialView("_DetailsRecord", viewObj);
        }
    }
}