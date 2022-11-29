using System;
using System.Collections.Generic;
using System.Text;

namespace School.Entities
{
    internal class Student
    {
        public int StudentId { get; set; } 
        public string StudentName { get; set; }
        public string StudentLAstName { get; set; }
        public string StudentEmail { get; set; }
        public Student(int StudentId, string StudentName, string StudentLAstName)
        {
            this.StudentId = StudentId;
            this.StudentName = StudentName;
            this.StudentLAstName = StudentLAstName;
            StudentEmail = GenerateEmail();
        }
        public Student(string StudentName, string StudentLAstName)
        {
            this.StudentName = StudentName;
            this.StudentLAstName = StudentLAstName;
            StudentEmail = GenerateEmail();
        }

        public string GenerateEmail()
        {
            return $"{StudentName}.{StudentLAstName}{StudentId}@Student.gov.do";
        }
        public string ToStringStudent()
        {
            return $"Id: {StudentId} | Nombre: {StudentName}| Apellido: {StudentLAstName} | Email: {StudentEmail}";
        }

    }
}
