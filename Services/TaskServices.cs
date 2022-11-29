using School.Entities;
using School.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Task = School.Entities.Task;

namespace School.Services
{
    internal class TaskServices : IService<Task>
    {
        Dictionary<int, Task> taskList = new Dictionary<int, Task>();
        public void AddToRecord(Task task)
        {

            if (VerifyIfTaskWasAssigned(task))
            {
                Console.WriteLine("Esta tarea ya fue asignada a un estudiante.");
            }
            else
            {
                var idWasAdded = taskList.TryAdd(task.TaskId, task);
                if (idWasAdded)
                {
                    Console.WriteLine("Se agrego con exito!");
                }
                else
                {
                    Console.WriteLine("Este estudiante ya se encuentra registrado.");
                }
            }
           
        }

        public List<Task> GetAllRecords()
        {
            List<Task> taskRoster = new List<Task>();
            foreach (var task in taskList.Values)
            {
                taskRoster.Add(task);
            }
            return taskRoster;
        }

        public Task GetRecord(int key)
        {
            var isEmpty = taskList.TryGetValue(key, out var task);
            if (isEmpty)
            {
                return task;
            }
            else
            {
                return null;
            }
        }

        public bool RemoveFromRecord(int key)
        {
            var isExist = taskList.TryGetValue(key, out var task);
            if (isExist)
            {
                taskList.Remove(key);
                return true;
            }
            else
            {
                Console.WriteLine("Este registro no existe.");
                return false;
            }
        }

        public void UpdateFromRecord(Task task)
        {
            if (task.TaskId!=task.StudentTaskId.StudentId| taskList.ContainsKey(task.TaskId))
            {
                taskList[task.TaskId] = task;
                Console.WriteLine("Actualizado con exito.");
            }
            else
            {
                Console.WriteLine("Por favor introduzca valores diferentes a los que ya estaba registrado.");
            }
        }

        public bool VerifyIfTaskWasAssigned(Task task)
        {
            foreach (Task taskHistory in taskList.Values)
            {
                if (task.StudentTaskId == taskHistory.StudentTaskId
                        && task.TaskName.ToLower().Trim().Equals(taskHistory.TaskName.ToLower().Trim()))
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveAllTaskFromOneStudent(int key)
        {
            foreach (Task task in taskList.Values)
            {
                if (key == task.StudentTaskId.StudentId)
                {
                    RemoveFromRecord(task.TaskId);
                }
            }
            Console.WriteLine("Todas las Tareas de este estudiante fueron eliminadas.");
        }
    }
}
