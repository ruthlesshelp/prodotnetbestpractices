namespace Lender.Slos.Dal
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    using Lender.Slos.Dal.Helper;
    using Lender.Slos.Dao;

    public class ApplicationDal : IRepository<ApplicationEntity>
    {
        private const string TableName = "Application";

        private readonly string _connectionString;

        public ApplicationDal(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Create(ApplicationEntity entity)
        {
            // Guard against invalid arguments.
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            if (entity.Id > 0)
            {
                throw new InvalidOperationException("Entity is invalid for Create, the Id must be 0.");
            }

            var sqlHelper = new DalHelper(_connectionString);

            var parameters =
                new List<SqlParameter>
                    {
                        new SqlParameter("StudentId", entity.StudentId),
                        new SqlParameter("Principal", entity.Principal),
                        new SqlParameter("AnnualPercentageRate", entity.AnnualPercentageRate),
                        new SqlParameter("TotalPayments", entity.TotalPayments),
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

        public ApplicationEntity Retrieve(int id)
        {
            // Guard against invalid arguments.
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id");
            }

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

            // Retrieve returns null if a record isn't found.
            ApplicationEntity entity = null;

            if (dataSet != null && dataSet.Tables.Count > 0)
            {
                var table = dataSet.Tables[0];

                foreach (DataRow row in table.Rows)
                {
                    entity = 
                        new ApplicationEntity
                            {
                                Id = row.Field<int>("Id"),
                                StudentId = row.Field<int>("StudentId"),
                                Principal = row.Field<decimal>("Principal"),
                                AnnualPercentageRate = row.Field<decimal>("AnnualPercentageRate"),
                                TotalPayments = row.Field<int>("TotalPayments"),
                            };
                    
                    // Retrieve returns the first record.
                    return entity;
                }
            }

            return entity;
        }

        public void Update(ApplicationEntity entity)
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
                        new SqlParameter("StudentId", entity.StudentId),
                        new SqlParameter("Principal", entity.Principal),
                        new SqlParameter("AnnualPercentageRate", entity.AnnualPercentageRate),
                        new SqlParameter("TotalPayments", entity.TotalPayments),
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

        public void Delete(ApplicationEntity entity)
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
