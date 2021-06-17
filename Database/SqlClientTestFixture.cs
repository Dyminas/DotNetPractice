using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data;

namespace Database
{
    [TestFixture]
    public class SqlClientTestFixture
    {
        private string _connectionString;

        [SetUp]
        public void Setup()
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = @"(localdb)\MSSQLLocalDB",
                InitialCatalog = "master",
                IntegratedSecurity = true,
                ConnectTimeout = 30,
                Encrypt = false,
                TrustServerCertificate = false,
                ApplicationIntent = ApplicationIntent.ReadWrite
            };
            _connectionString = connectionStringBuilder.ConnectionString;

            // Create test table
            const string sql = @"
                IF OBJECT_ID(N'TestTable', N'U') IS NULL
                BEGIN
                    CREATE TABLE TestTable
                    (
                        ID int NOT NULL,
                        Description varchar(255) NOT NULL,
                        CONSTRAINT PK_TestTable PRIMARY KEY CLUSTERED(ID)
                    )
                END";
            ExecuteNonQuery(sql);

            // Insert test data
            InsertTestData();
        }

        [TearDown]
        public void TearDown()
        {
            const string sql = "DROP TABLE IF EXISTS TestTable";
            ExecuteNonQuery(sql);
        }

        private void ExecuteNonQuery(string sql)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand command = new(sql, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void InsertTestData()
        {
            DataTable data = BuildDataTable();
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlBulkCopy bulkCopy = new(connection))
                {
                    bulkCopy.DestinationTableName = data.TableName;
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        bulkCopy.ColumnMappings.Add(data.Columns[i].ColumnName, data.Columns[i].ColumnName);
                    }
                    bulkCopy.WriteToServer(data);
                }
            }
        }

        private static DataTable BuildDataTable()
        {
            DataTable dataTable = new("TestTable");

            dataTable.Columns.Add("ID", typeof(int));
            dataTable.Columns.Add("Description", typeof(string));

            for(int i = 1; i <= 50; i++)
            {
                DataRow row = dataTable.NewRow();
                row["ID"] = i;
                row["Description"] = $"Test Record {i}; Created at: {DateTime.Now}";
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }

        [Test]
        public void TestPaginationQuery()
        {
            const int pageIndex = 3, pageSize = 5,
                      startRow = (pageIndex - 1) * pageSize;

            const string sql = @"
                SELECT TOP (@pageSize) *
                FROM
                (
                    SELECT ROW_NUMBER() OVER(ORDER BY ID ASC) AS RowNumber, ID, Description
                    FROM TestTable
                ) AS Temp
                WHERE RowNumber > (@startRow)";

            var idList = new List<int>();

            using (SqlConnection connection = new(_connectionString))
            {
                SqlCommand command = new(sql, connection);
                command.Parameters.AddWithValue("@pageSize", pageSize);
                command.Parameters.AddWithValue("@startRow", startRow);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    var idIndex = reader.GetOrdinal("ID");
                    var descriptionIndex = reader.GetOrdinal("Description");

                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetString(descriptionIndex));
                        idList.Add(reader.GetInt32(idIndex));
                    }
                }
            }

            for (int i = 1; i < idList.Count; i++)
            {
                Assert.LessOrEqual(idList[i - 1], idList[i]);
            }
        }
    }
}