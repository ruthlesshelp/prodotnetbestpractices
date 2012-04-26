namespace Lender.Slos.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Lender.Slos.Dal.Helper;
    using Lender.Slos.Dao;

    public class StudentDal 
        : IRepository<StudentEntity>
    {
        private const string TableName = "Student";

        private readonly string _connectionString;

        public StudentDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Create(StudentEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var sqlHelper = new DalHelper(_connectionString);

            var parameters =
                new List<SqlParameter>
                    {
                        new SqlParameter("Id", entity.Id),
                        new SqlParameter("HighSchoolName", entity.HighSchoolName),
                        new SqlParameter("HighSchoolCity", entity.HighSchoolCity),
                        new SqlParameter("HighSchoolState", entity.HighSchoolState),
                    };

            var procedureName = string.Format(
                DalHelper.CreateStoredProcFormat,
                TableName);

            try
            {
                var dataSet = sqlHelper.ExecuteStoredProcedure(
                    procedureName,
                    parameters);

                // Returns createdId if a record was created.
                var createdId = default(int);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    var table = dataSet.Tables[0];

                    foreach (DataRow row in table.Rows)
                    {
                        createdId = row.Field<int>("Id");

                        // Retrieve returns the first record.
                        break;
                    }
                }

                return createdId;
            }
            catch (Exception exception)
            {
                // Throw an exception if the Id was not returned.
                throw new InvalidOperationException("Failed to create.", exception);
            }
        }

        public StudentEntity Retrieve(int id)
        {
            var sqlHelper = new DalHelper(_connectionString);

            var parameters =
                new List<SqlParameter>
                    {
                        new SqlParameter("Id", id)
                    };

            var procedureName = string.Format(
                DalHelper.RetrieveStoredProcFormat,
                TableName);

            var dataSet = sqlHelper.ExecuteStoredProcedure(
                procedureName,
                parameters);

            StudentEntity entity = null;
            if (dataSet != null &&
                dataSet.Tables.Count > 0)
            {
                var table = dataSet.Tables[0];
                foreach (DataRow row in table.Rows)
                {
                    entity =
                        new StudentEntity
                        {
                            Id = row.Field<int>("Id"),
                            HighSchoolName = row.Field<string>("HighSchoolName"),
                            HighSchoolCity = row.Field<string>("HighSchoolCity"),
                            HighSchoolState = row.Field<string>("HighSchoolState"),
                        };
                }
            }

            return entity;
        }

        public void Update(StudentEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity is invalid for Update, the Id must be greater than 0.");
            }

            var sqlHelper = new DalHelper(_connectionString);

            var parameters =
                new List<SqlParameter>
                    {
                        new SqlParameter("Id", entity.Id),
                        new SqlParameter("HighSchoolName", entity.HighSchoolName),
                        new SqlParameter("HighSchoolCity", entity.HighSchoolCity),
                        new SqlParameter("HighSchoolState", entity.HighSchoolState),
                    };

            var procedureName = string.Format(
                DalHelper.UpdateStoredProcFormat,
                TableName);

            var rowsAffected = sqlHelper.ExecuteStoredProcedureNonQuery(
                procedureName,
                parameters);

            if (rowsAffected != 1)
            {
                throw new InvalidOperationException("Entity saving failed during Update.");
            }
        }

        public void Delete(StudentEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id <= 0)
            {
                throw new InvalidOperationException("Entity is invalid for Update, the Id must be greater than 0.");
            }

            var sqlHelper = new DalHelper(_connectionString);

            var parameters =
                new List<SqlParameter>
                    {
                        new SqlParameter("Id", entity.Id),
                    };

            var procedureName = string.Format(
                DalHelper.DeleteStoredProcFormat,
                TableName);

            var rowsAffected = sqlHelper.ExecuteStoredProcedureNonQuery(
                procedureName,
                parameters);

            if (rowsAffected != 1)
            {
                throw new InvalidOperationException("Entity saving failed during Update.");
            }
        }
    }
}
