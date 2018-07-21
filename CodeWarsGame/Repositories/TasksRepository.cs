using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CodeWarsGame.Models;

namespace CodeWarsGame.Repositories
{
    public class TasksRepository : IDisposable
    {
        private readonly CodeWarsGameContext _db;

        public TasksRepository()
        {
            _db = new CodeWarsGameContext();
        }

        public Task GetTaskById(int taskId)
        {
            return _db.Tasks.FirstOrDefault(x => x.Id == taskId);
        }
       
        public List<SelectListItem> GetTasksList()
        {
            return _db.Tasks.Select(x => new SelectListItem
            {
                Text = x.TaskName,
                Value = x.Id.ToString()
            }).ToList();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}