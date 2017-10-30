using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [HttpGet("/Categories")]
      public ActionResult Results2()
      {
        return View("Results",Category.GetAll());
      }

      [HttpPost("/Categories")]
      public ActionResult Results()
      {
        Category newCategory = new Category (Request.Form["inputCategory"]);
        newCategory.Save();
        return View (Category.GetAll());
      }

      [HttpGet("/Categories/{id}")]
      public ActionResult ResultTask(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(id);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", categoryTasks);
        return View(model);
      }

      [HttpPost("/Categories/{id}")]
      public ActionResult ResultTask2(int id)
      {
        string taskDescription = Request.Form["inputTask"];
        Task newTask = new Task(taskDescription,(Request.Form["inputDate"]));
        newTask.Save();
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(Int32.Parse(Request.Form["category-id"]));
        selectedCategory.AddTask(newTask);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        model.Add("tasks", categoryTasks);
        model.Add("category", selectedCategory);
        return View("ResultTask", model);
      }

      [HttpGet("/Categories/{id}/tasks/new")]
      public ActionResult TaskForm(int id)
      {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(id);
        List<Task> allTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", allTasks);
        return View(model);
      }



      [HttpPost("/Tasks/Delete")]
      public ActionResult DeletePage2()
      {
        Task.DeleteAll();
        return View();
      }

      [HttpPost("/Categories/{id}/Delete")]
      public ActionResult DeleteCategory(int id)
      {
        Category foundCategory = Category.Find(id);
        foreach (Task classTask in foundCategory.GetTasks())
          {
            classTask.DeleteTasks();
          }
        foundCategory.DeleteCategory();
        return View("DeletePage3");
      }

      [HttpPost("/Categories/Delete")]
      public ActionResult DeletePage()
      {
        Category.DeleteAll();
        return View();
      }

      [HttpGet("/TaskList")]
      public ActionResult AlphaList()
      {
        return View(Task.GetAlphaList());
      }

    }
  }
