using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace ToDoList.Models
{
  public class Task
  {
    private string _description;
    private int _id;
    private string _dueDate;

    public Task(string description, string dueDate, int id = 0)
    {
      _description = description;
      _id = id;
      _dueDate = dueDate;
    }

    public override bool Equals(System.Object otherTask)
    {
      if (!(otherTask is Task))
      {
        return false;
      }
      else
      {
        Task newTask = (Task) otherTask;
        bool idEquality = this.GetId() == newTask.GetId();
        bool descriptionEquality = this.GetDescription() == newTask.GetDescription();
        bool duedateEquality = this.GetDueDate() == newTask.GetDueDate();
        return (idEquality && descriptionEquality && duedateEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetDescription().GetHashCode();
    }

    public string GetDescription()
    {
      return _description;
    }
    public int GetId()
    {
      return _id;
    }

    public string GetDueDate()
    {
      return _dueDate;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO tasks (description, duedate) VALUES (@description, @dueDate);";

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter duedate = new MySqlParameter();
      duedate.ParameterName = "@dueDate";
      duedate.Value = this._dueDate;
      cmd.Parameters.Add(duedate);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Task> GetAll()
    {
      List<Task> allTasks = new List<Task> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tasks ORDER BY duedate;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int taskId = rdr.GetInt32(0);
        string taskDescription = rdr.GetString(1);
        string taskDueDate = rdr.GetString(2);
        Task newTask = new Task(taskDescription, taskDueDate, taskId);
        allTasks.Add(newTask);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allTasks;
    }
    public static Task Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM tasks WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int taskId = 0;
      string taskName = "";
      string taskDueDate = "";

      while(rdr.Read())
      {
        taskId = rdr.GetInt32(0);
        taskName = rdr.GetString(1);
        taskDueDate = rdr.GetString(2);
      }
      Task newTask = new Task(taskName, taskDueDate, taskId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newTask;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM tasks;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void DeleteTasks()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();

        MySqlCommand cmd = new MySqlCommand("DELETE FROM tasks WHERE id = @TaskId; DELETE FROM categories_tasks WHERE task_id = @TaskId;", conn);
        MySqlParameter taskIdParameter = new MySqlParameter();
        taskIdParameter.ParameterName = "@TaskId";
        taskIdParameter.Value = this.GetId();

        cmd.Parameters.Add(taskIdParameter);
        cmd.ExecuteNonQuery();

        if (conn != null)
        {
          conn.Close();
        }
      }


      public static List<Task> GetAlphaList()
      {
        List<Task> allTasks = new List<Task> {};
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM tasks ORDER BY duedate;";
        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        while(rdr.Read())
        {
          int taskId = rdr.GetInt32(0);
          string taskDescription = rdr.GetString(1);
          string taskDueDate = rdr.GetString(2);
          Task newTask = new Task(taskDescription, taskDueDate, taskId);
          allTasks.Add(newTask);
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return allTasks;
      }

      public void AddCategory(Category newCategory)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO categories_tasks (category_id, task_id) VALUES (@CategoryId, @TaskId);";

        MySqlParameter category_id = new MySqlParameter();
        category_id.ParameterName = "@CategoryId";
        category_id.Value = newCategory.GetId();
        cmd.Parameters.Add(category_id);

        MySqlParameter task_id = new MySqlParameter();
        task_id.ParameterName = "@TaskId";
        task_id.Value = _id;
        cmd.Parameters.Add(task_id);

        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn !=null)
        {
          conn.Dispose();
        }
      }

      public List<Category> GetCategories()
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT category_id FROM categories_Tasks WHERE task_id = @taskId;";

        MySqlParameter taskIdParameter = new MySqlParameter();
        taskIdParameter.ParameterName = "@taskId";
        taskIdParameter.Value = _id;
        cmd.Parameters.Add(taskIdParameter);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;

        List<int> categoryIds =new List<int> {};
        while(rdr.Read())
        {
          int categoryId = rdr.GetInt32(0);
          categoryIds.Add(categoryId);
        }
        rdr.Dispose();

        List<Category> categories = new List<Category> {};
        foreach (int categoryId in categoryIds)
        {
          var categoryQuery = conn.CreateCommand() as MySqlCommand;
          categoryQuery.CommandText = @"SELECT * FROM categories WHERE id = @CategoryId;";

          MySqlParameter categoryIdParameter = new MySqlParameter();
          categoryIdParameter.ParameterName = "@CategoryId";
          categoryIdParameter.Value = categoryId;
          categoryQuery.Parameters.Add(categoryIdParameter);

          var categoryQueryRdr = categoryQuery.ExecuteReader() as MySqlDataReader;
          while(categoryQueryRdr.Read())
          {
            int thisCategoryId = categoryQueryRdr.GetInt32(0);
            string categoryName = categoryQueryRdr.GetString(1);
            Category foundCategory = new Category(categoryName, thisCategoryId);
            categories.Add(foundCategory);
          }
          categoryQueryRdr.Dispose();
        }
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return categories;
      }
    }
  }
