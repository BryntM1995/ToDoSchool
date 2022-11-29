using System;
using System.Collections.Generic;
using System.Text;

namespace School.Entities
{
    internal class Task
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string TeacherName { get; set; }
        public Student StudentTaskId { get; set; }

        public Task(int TaskId, string TaskName, string TeacherName, Student StudentTaskId)
        {
            this.TaskId = TaskId;
            this.TaskName = TaskName;
            this.TeacherName = TeacherName;
            this.StudentTaskId = StudentTaskId;
        }
        public Task(int TaskId,string TaskName, string TeacherName)
        {
            this.TaskId=TaskId;
            this.TaskName=TaskName;
            this.TeacherName=TeacherName;   
        }
        public string ToStringTask()
        {
            return $"Id: {StudentTaskId.StudentId} | Nombre: {StudentTaskId.StudentName}| Tarea:(ID:{TaskId}) {TaskName} | Asignada por: {TeacherName}";
        }
    }
}
