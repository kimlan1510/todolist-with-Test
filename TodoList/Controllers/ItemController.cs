using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class ItemController : Controller
    {
        //private ToDoListContext db = new ToDoListContext();
        private IItemRepository itemRepo;
        public ItemController(IItemRepository thisRepo = null)
        {
            if(thisRepo == null)
            {
                this.itemRepo = new IEFItemRepository();
            }
            else
            {
                this.itemRepo = thisRepo;
            }
        }
        public IActionResult Index()
        {

            return View(itemRepo.Items.ToList());
        }
        public IActionResult Details(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Item item)
        {
            itemRepo.Save(item);
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }

        [HttpPost]
        public IActionResult Edit(Item item)
        {
            itemRepo.Edit(item);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            return View(thisItem);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisItem = itemRepo.Items.FirstOrDefault(items => items.ItemId == id);
            itemRepo.Remove(thisItem);
            return RedirectToAction("Index");
        }
        
    }
}