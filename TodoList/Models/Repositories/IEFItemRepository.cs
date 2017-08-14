using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
    public class IEFItemRepository: IItemRepository
    {
        ToDoListContext db = new ToDoListContext();
        public IEFItemRepository(ToDoListContext conn = null)
        {
            if(conn == null)
            {
                this.db = new ToDoListContext();
            }
            else
            {
                this.db = conn;
            }
        }

        public IQueryable<Item> Items
        { get { return db.Items; } }


        public Item Save(Item item)
        {
            db.Items.Add(item);
            db.SaveChanges();
            return item;
        }

        public Item Edit(Item item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return item;
        }

        public void Remove(Item item)
        {
            db.Items.Remove(item);
            db.SaveChanges();
        }

        public void RemoveAll()
        {
            var ListItems = db.Items.ToList();
            foreach(var item in ListItems)
            {
                db.Items.Remove(item);
            }
            db.SaveChanges();
        }
    }
}
