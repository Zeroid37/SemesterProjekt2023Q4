using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    public class UserDB : UserDAO {

        private IConfiguration Configuration;
        private String? connectionString;

        public UserDB(IConfiguration configuration) 
        {
            Configuration = configuration;
            connectionString = Configuration.GetConnectionString("ConnectMsSqlString");
        }


        private Boolean addPersonToDB(Person person, SqlConnection con, SqlTransaction transaction)
        {
            int insertedRows = 0;
            string addPersonToDBQuery = "insert into Person(firstName, lastName, addressId_FK, phone, dateOfBirth, email, password, isAdmin, type)" +
                                 "values(@FIRSTNAME, @LASTNAME, @ADDRESSID_FK, @PHONE, @DATEOFBIRTH, @EMAIL, @PASSWORD, @ISADMIN, @TYPE)";

            
            using (SqlCommand cmd = new SqlCommand(addPersonToDBQuery, con, transaction))
            {
                try
                {
                    int addressId = addAddressToDB(person.address, con, transaction);
                    cmd.CommandText = addPersonToDBQuery;

                    cmd.Parameters.AddWithValue("FIRSTNAME", person.firstName);
                    cmd.Parameters.AddWithValue("LASTNAME", person.lastName);
                    cmd.Parameters.AddWithValue("ADDRESSID_FK", addressId);
                    cmd.Parameters.AddWithValue("PHONE", person.phoneNo);
                    cmd.Parameters.AddWithValue("DATEOFBIRTH", person.dateOfBirth);
                    cmd.Parameters.AddWithValue("EMAIL", person.email);
                    cmd.Parameters.AddWithValue("PASSWORD", person.password);
                    cmd.Parameters.AddWithValue("ISADMIN", person.isAdmin);
                    cmd.Parameters.AddWithValue("TYPE", person.type);

                    insertedRows = cmd.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return (insertedRows > 0);
        }


        public Boolean addUserToDB(User user) {

            int insertedRows = 0;
            
            string addUserToDBQuery = "insert into Users(userId, email_fk) values(@USERID, @EMAIL_FK)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addUserTransaction");
                using (SqlCommand cmd = new SqlCommand(addUserToDBQuery, con, transaction))
                {
                    try
                    {
                        addPersonToDB(user, con, transaction);
                        cmd.CommandText = addUserToDBQuery;
                        cmd.Parameters.AddWithValue("USERID", user.userId);
                        cmd.Parameters.AddWithValue("EMAIL_FK", user.email);

                        insertedRows = cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return (insertedRows > 0);
        }

        public Boolean addEventOrganizerToDB(EventOrganizer eventOrganizer) {

            int insertedRows = 0;
            string addEventOrginizerToDBQuery = "insert into EventOrganizer(organizerId, email_fk) values(@ORGANIZERID, @EMAIL_FK)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlTransaction transaction = con.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted, "addEventOrganizerTransaction");
                using (SqlCommand cmd = new SqlCommand(addEventOrginizerToDBQuery, con, transaction))
                {
                    try
                    {
                        addPersonToDB(eventOrganizer, con, transaction);
                        cmd.Parameters.AddWithValue("ORGANIZERID", eventOrganizer.organizerId);
                        cmd.Parameters.AddWithValue("EMAIL_FK", eventOrganizer.email);

                        insertedRows = cmd.ExecuteNonQuery();
                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                    }
                }
            }
            return (insertedRows > 0);
        }

        public int addAddressToDB(Address address, SqlConnection con, SqlTransaction transaction) {

            int addressId = -1;

            string queryStringZipCity = "insert into ZipCity(zip, city) values(@ZIP, @CITY)";
            string queryStringAddress = "insert into Address(street, houseNo, zip_FK) values (@STREET, @HOUSENO, @ZIP_FK); SELECT CAST(scope_identity() AS int)";

            using(SqlCommand cmd = new SqlCommand(queryStringZipCity, con, transaction))
            {
                try
                {
                    cmd.Parameters.AddWithValue("ZIP", address.zip);
                    cmd.Parameters.AddWithValue("CITY", address.city);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = queryStringAddress;
                    cmd.Parameters.AddWithValue("STREET", address.zip);
                    cmd.Parameters.AddWithValue("HOUSENO", address.houseNo);
                    cmd.Parameters.AddWithValue("ZIP_FK", address.zip);

                    addressId = (int)cmd.ExecuteScalar();
                }
                catch (SqlException)
                {
                    throw;
                }
            }
            return addressId;
        }

        public User getUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
