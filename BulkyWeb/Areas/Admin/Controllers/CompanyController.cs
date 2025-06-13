using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return View(companies);
        }
        public IActionResult Upsert(int? id)
        {
            
                if (id == null || id == 0)
                {
                    return View(new Company());
                }
                else
                {
                    Company objcompany = _unitOfWork.Company.Get(u => u.Id == id);
                    return View(objcompany);
                }
            
        }
        [HttpPost]
        public IActionResult Upsert(Company objCompany)
        {
            if (ModelState.IsValid)
            {
                if (objCompany.Id == null || objCompany.Id == 0)
                {
                    _unitOfWork.Company.Add(objCompany);
                }
                else
                {
                    _unitOfWork.Company.Update(objCompany);  
                }
                _unitOfWork.Save();
                if (objCompany.Id == 0|| objCompany.Id == null)
                    TempData["success"] = $"Company has been Created SUCCESSFULLY";
                else
                    TempData["success"] = $"Company has been Updated SUCCESSFULLY";
                return RedirectToAction("Index");
            }
            return View(objCompany);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> companies = _unitOfWork.Company.GetAll().ToList();
            return Json(new {data=companies});
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
            if (CompanyToBeDeleted == null)
            {
                return Json(new { succes = false, message = "Error while deleting" });
            }
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { succes = true, message = " Deleted Successfully!!" });

        }
    }
}
