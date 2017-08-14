using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using ToDoList.Controllers;
using ToDoList.Models;
using Xunit;
using Moq;
using System.Linq;

namespace ToDoTest.ControllersTest
{
    public class ItemControllerTest
    {
        //Set up mock database
        Mock<IItemRepository> mock = new Mock<IItemRepository>();
       
        private void mockDb()
        {
            mock.Setup(m => m.Items).Returns(new Item[]{
                new Item { ItemId = 1, Description = "yell at Alyssa"},
                new Item { ItemId = 2, Description = "yell at David"}
            }.AsQueryable());
        }
        //end setting up mock database
        //Set up Test Database
        IEFItemRepository db = new IEFItemRepository(new TestDbContext());

        [Fact]
        public void mock_ConfirmEntry_Test() //confirm a known entry in database
        {
            //Arrange
            mockDb();
            ItemController controller = new ItemController(mock.Object);
            Item testItem = new Item();
            testItem.Description = "yell at Alyssa";
            testItem.ItemId = 1;

            //Act
            ViewResult indexView = controller.Index() as ViewResult;
            var collection = indexView.ViewData.Model as IEnumerable<Item>;

            //Assert
            Assert.Contains<Item>(testItem, collection);
        }
        [Fact]
        public void Get_ModelListItemIndex_Test()
        {
            //Arrange
            mockDb();
            ViewResult indexView = new ItemController(mock.Object).Index() as ViewResult;

            //Act
            var result = indexView.ViewData.Model;

            //Assert
            Assert.IsType<List<Item>>(result);
        }
        [Fact]
        public void DB_CreateNewEntry_Test()
        {
            //Arrange
            ItemController controller = new ItemController(db);
            Item testItem = new Item();
            testItem.Description = "test Dataase";

            //Act
            controller.Create(testItem);
            var collection = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Item>;

            //Assert
            Assert.Contains<Item>(testItem, collection);
            db.RemoveAll();
        }

    }
}
