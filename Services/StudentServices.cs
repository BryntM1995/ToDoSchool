using School.Entities;
using School.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace School.Services
{
    internal class StudentServices : IService<Student>
    {
        Dictionary<int ,Student> studentList = new Dictionary<int,Student>();
        public void AddToRecord(Student student)
        {   
            var idWasAdded = studentList.TryAdd(student.StudentId, student);
            if (idWasAdded)
            {
                Console.WriteLine("Se agrego con exito!");
            }
            else
            {
                Console.WriteLine("Este estudiante ya se encuentra registrado.");
            }
        }

        public List<Student> GetAllRecords()
        {
            List<Student> studentRoster = new List<Student>();
            foreach (var student in studentList.Values)
            {
                studentRoster.Add(student);
            }
            return studentRoster;
        }

        public Student GetRecord(int key)
        {
            var isEmpty = studentList.TryGetValue(key, out var Student);
            if (isEmpty)
            {
                return Student;
            }
            else
            {
                Console.WriteLine("Ningun registro encontrado.");
                return null;
            }
        }

        public bool RemoveFromRecord(int key)
        {
            var isExist=studentList.TryGetValue(key, out var student);
            if (isExist)
            {
                studentList.Remove(key);
                Console.WriteLine("Eliminado con exito!");
                return true;
            }
            else
            {
                Console.WriteLine("Ningun registro encontrado.");
                return false;
            }
        }

        public void UpdateFromRecord(Student student)
        {
            if (studentList.ContainsKey(student.StudentId))
            {
                if (student.StudentName.ToLower().Trim().Equals(GetRecord(student.StudentId).StudentName.ToLower())
               | (student.StudentLAstName.ToLower().Trim().Equals(GetRecord(student.StudentId).StudentLAstName.ToLower()))
               | (student.StudentEmail.ToLower().Trim().Equals(GetRecord(student.StudentId).StudentLAstName.ToLower())))
                {
                    studentList[student.StudentId] = student;
                }
                else
                {
                    Console.WriteLine("Por favor introduzca valores diferentes a los que ya estaba registrado.");
                }
            }
            else
            {
                Console.WriteLine("Este estudiante no existe.");
            }
           
        }
    }
}
