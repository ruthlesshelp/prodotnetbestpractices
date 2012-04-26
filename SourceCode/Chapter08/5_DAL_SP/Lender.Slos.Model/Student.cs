namespace Lender.Slos.Model
{
    using System;

    using Lender.Slos.Dao;

    public class Student
    {
        private readonly IRepository<IndividualEntity> _individualRepo;
        private readonly IRepository<StudentEntity> _studentRepo;

        private DateTime? _today;

        public Student()
            : this(RepositoryFactory.IndividualRepo, RepositoryFactory.StudentRepo)
        {
        }

        internal Student(
            IRepository<IndividualEntity> individualRepo,
            IRepository<StudentEntity> studentRepo)
        {
            _individualRepo = individualRepo;
            _studentRepo = studentRepo;
            this.HighSchool = new School();
        }

        public int Id { get; internal set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleInitial { get; set; }

        public string Suffix { get; set; }

        public DateTime DateOfBirth { get; set; }

        public School HighSchool { get; private set; }

        public DateTime Today
        {
            get { return this._today ?? DateTime.Today; }
            internal set { this._today = value; }
        }

        public static Student FindById(int id)
        {
            var student = new Student();

            student.Get(id);

            return student;
        }

        public void Get(int studentId)
        {
            var studentEntity = _studentRepo.Retrieve(studentId);
            if (studentEntity == null)
            {
                throw new InvalidOperationException("Student not found.");
            }

            var individualEntity = _individualRepo.Retrieve(studentEntity.Id);
            if (individualEntity == null)
            {
                throw new InvalidOperationException("Individual is invalid.");
            }

            this.LoadData(individualEntity, studentEntity);
        }

        public void Save()
        {
            // Comment out the call to Validate to see the test fail
            this.Validate();

            var individualEntity =
                new IndividualEntity
                    {
                        Id = this.Id,
                        LastName = LastName,
                        FirstName = FirstName,
                        MiddleName = MiddleInitial,
                        Suffix = Suffix,
                        DateOfBirth = DateOfBirth,
                    };

            var studentId = individualEntity.Id;
            if (studentId > 0)
            {
                _individualRepo.Update(individualEntity);
            }
            else
            {
                studentId = _individualRepo.Create(individualEntity);
            }

            var studentEntity = _studentRepo.Retrieve(studentId);
            if (studentEntity != null)
            {
                Id = studentEntity.Id;
                
                studentEntity.HighSchoolName = HighSchool.Name;
                studentEntity.HighSchoolCity = HighSchool.City;
                studentEntity.HighSchoolState = HighSchool.State;

                _studentRepo.Update(studentEntity);

                return;
            }
            
            var retVal =
                _studentRepo.Create(
                    new StudentEntity
                        {
                            Id = studentId,
                            HighSchoolName = this.HighSchool.Name,
                            HighSchoolCity = this.HighSchool.City,
                            HighSchoolState = this.HighSchool.State,
                        });

            if (retVal != studentId)
            {
                throw new InvalidOperationException(
                    string.Format("Create failed for student (Id={0})", 
                    studentId));
            }

            Id = studentId;
        }

        public void Remove(bool removeIndividual = false)
        {
            var individualId = this.Id;
            if (individualId <= 0)
            {
                throw new InvalidOperationException("Student Id is invalid.");
            }

            var studentEntity = _studentRepo.Retrieve(individualId);
            _studentRepo.Delete(studentEntity);

            if (!removeIndividual)
            {
                return;
            }

            var individualEntity = _individualRepo.Retrieve(individualId);
            _individualRepo.Delete(individualEntity);
        }

        public void Validate()
        {
            if (this.DateOfBirth > this.Today)
            {
                throw new InvalidOperationException(string.Format(
                    "Student DOB is after today {0})",
                    this.DateOfBirth.ToShortDateString()));
            }

            var earliestDateOfBirth = this.Today.AddYears(-26);
            var latestDateOfBirth = this.Today.AddYears(-16);

            if (DateOfBirth > latestDateOfBirth)
            {
                throw new InvalidOperationException(string.Format(
                    "Student must 16 years old or older (DOB is after {0})",
                    latestDateOfBirth.ToShortDateString()));
            }

            if (DateOfBirth <= earliestDateOfBirth)
            {
                throw new InvalidOperationException(string.Format(
                    "Student must not be 27 years of age or older (DOB is on or before {0})",
                    earliestDateOfBirth.ToShortDateString()));
            }
        }

        internal void LoadData(
            IndividualEntity individual, 
            StudentEntity student)
        {
            Id = student.Id;

            LastName = individual.LastName;
            FirstName = individual.FirstName;
            MiddleInitial = GetMiddleInitial(individual.MiddleName);
            Suffix = individual.Suffix;
            DateOfBirth = individual.DateOfBirth;

            this.HighSchool = 
                new School
                    {
                        Name = student.HighSchoolName, 
                        City = student.HighSchoolCity, 
                        State = student.HighSchoolState,
                    };
        }

        private static string GetMiddleInitial(string middleName)
        {
            return !string.IsNullOrWhiteSpace(middleName) && middleName.Length > 0
                       ? middleName[0].ToString()
                       : null;
        }
    }
}
