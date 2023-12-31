﻿using Microsoft.Extensions.Configuration;
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
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Add a user to the database
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean true or false</returns>
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
        /// <summary>
        /// Add an event organizer to the database
        /// </summary>
        /// <param name="eventOrganizer"></param>
        /// <returns>Boolean</returns>
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

        /// <summary>
        /// Adds a person to the database
        /// </summary>
        /// <param name="person"></param>
        /// <param name="con"></param>
        /// <param name="transaction"></param>
        /// <returns>Boolean</returns>
        private Boolean addPersonToDB(Person person, SqlConnection con, SqlTransaction transaction) {
            int insertedRows = 0;
            string addPersonToDBQuery = "insert into Person(firstName, lastName, addressId_FK, phone, dateOfBirth, email, isAdmin, type, aspNetUsersId_FK)" +
                                 "values(@FIRSTNAME, @LASTNAME, @ADDRESSID_FK, @PHONE, @DATEOFBIRTH, @EMAIL, @ISADMIN, @TYPE, null)";


            using (SqlCommand cmd = new SqlCommand(addPersonToDBQuery, con, transaction)) {
                try {
                    int addressId = addAddressToDB(person.address, con, transaction);
                    cmd.CommandText = addPersonToDBQuery;

                    cmd.Parameters.AddWithValue("FIRSTNAME", person.firstName);
                    cmd.Parameters.AddWithValue("LASTNAME", person.lastName);
                    cmd.Parameters.AddWithValue("ADDRESSID_FK", addressId);
                    cmd.Parameters.AddWithValue("PHONE", person.phoneNo);
                    cmd.Parameters.AddWithValue("DATEOFBIRTH", person.dateOfBirth);
                    cmd.Parameters.AddWithValue("EMAIL", person.email);
                    cmd.Parameters.AddWithValue("ISADMIN", person.isAdmin);
                    cmd.Parameters.AddWithValue("TYPE", person.type);

                    insertedRows = cmd.ExecuteNonQuery();
                } catch (SqlException) {
                    throw;
                }
            }
            return (insertedRows > 0);
        }

        /// <summary>
        /// Add an address to the database
        /// </summary>
        /// <param name="address"></param>
        /// <param name="con"></param>
        /// <param name="transaction"></param>
        /// <returns>int id of the added address</returns>
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

        /// <summary>
        /// Get a user by their email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User</returns>
        public User getUserByEmail(string email)
        {
            String getUserByEmailQuery = "SELECT userID from Users where email_FK = @EMAIL_FK";
            String getPersonByEmailQuery = "SELECT firstName, lastName, addressId_FK, phone, dateOfBirth, isAdmin FROM Person where email=@EMAIL";
            User user = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getUserByEmailQuery, con))
            using (SqlCommand cmd2 = new SqlCommand(getPersonByEmailQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("EMAIL_FK", email);
                cmd2.Parameters.AddWithValue("EMAIL", email);
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    String firstName = reader.GetString(reader.GetOrdinal("firstName"));
                    String lastName = reader.GetString(reader.GetOrdinal("lastName"));
                    Address address = getAddressByAdressId(reader.GetInt32(reader.GetOrdinal("addressId_FK")));
                    String phone = reader.GetString(reader.GetOrdinal("phone"));
                    DateTime dateOfBirth = reader.GetDateTime(reader.GetOrdinal("dateOfBirth"));
                    Boolean isAdmin = reader.GetBoolean(reader.GetOrdinal("isAdmin"));

                    user.firstName = firstName;
                    user.lastName = lastName;
                    user.address = address;
                    user.phoneNo = phone;
                    user.dateOfBirth = dateOfBirth;
                    user.isAdmin = isAdmin;
                }
                reader.Close();
                SqlDataReader reader2 = cmd.ExecuteReader();
                while (reader2.Read())
                {
                    user.userId = reader2.GetString(reader2.GetOrdinal("userId"));
                }
            }
            return user;
        }

        /// <summary>
        /// Get an event organizer by their id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>EventOrganizer</returns>
        public EventOrganizer getEventOrganizerByID(string ID)
        {
            String getUserByEmailQuery = "SELECT email_fk from EventOrganizer where organizerId = @ORGANIZER_ID";
            String getPersonByEmailQuery = "SELECT firstName, lastName, addressId_FK, phone, dateOfBirth, isAdmin FROM Person where email=@EMAIL";
            EventOrganizer evOrg = new EventOrganizer();
            evOrg.organizerId = ID;

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getUserByEmailQuery, con))
            using (SqlCommand cmd2 = new SqlCommand(getPersonByEmailQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("ORGANIZER_ID", ID);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    evOrg.email = reader.GetString(reader.GetOrdinal("email_FK"));
                }
                reader.Close();

                cmd2.Parameters.AddWithValue("EMAIL", evOrg.email);
                reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    String firstName = reader.GetString(reader.GetOrdinal("firstName"));
                    String lastName = reader.GetString(reader.GetOrdinal("lastName"));
                    Address address = getAddressByAdressId(reader.GetInt32(reader.GetOrdinal("addressId_FK")));
                    String phone = reader.GetString(reader.GetOrdinal("phone"));
                    DateTime dateOfBirth = reader.GetDateTime(reader.GetOrdinal("dateOfBirth"));
                    Boolean isAdmin = reader.GetBoolean(reader.GetOrdinal("isAdmin"));

                    evOrg.firstName = firstName;
                    evOrg.lastName = lastName;
                    evOrg.address = address;
                    evOrg.phoneNo = phone;
                    evOrg.dateOfBirth = dateOfBirth;
                    evOrg.isAdmin = isAdmin;
                }
            }
            return evOrg;
        }
        /// <summary>
        /// Get user by their Id
        /// </summary>
        /// <param name="userID"></param>
        /// <returns>User</returns>
        public User getUserByUserID(string userID)
        {
            String getUserByEmailQuery = "SELECT userId, email_FK from Users where userId = @USERID";
            String getPersonByEmailQuery = "SELECT firstName, lastName, addressId_FK, phone, dateOfBirth, isAdmin FROM Person where email=@EMAIL";
            User user = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmdUser = new SqlCommand(getUserByEmailQuery, con))
            using (SqlCommand cmdPerson = new SqlCommand(getPersonByEmailQuery, con))
            {
                con.Open();
                cmdUser.Parameters.AddWithValue("USERID", userID);
                SqlDataReader readerUser = cmdUser.ExecuteReader();
                while (readerUser.Read())
                {
                    user.userId = readerUser.GetString(readerUser.GetOrdinal("userId"));
                    user.email = readerUser.GetString(readerUser.GetOrdinal("email_FK"));
                    
                }
                readerUser.Close();
                cmdPerson.Parameters.AddWithValue("EMAIL", user.email);
                SqlDataReader readerPerson = cmdPerson.ExecuteReader();
                while (readerPerson.Read())
                {
                    String firstName = readerPerson.GetString(readerPerson.GetOrdinal("firstName"));
                    String lastName = readerPerson.GetString(readerPerson.GetOrdinal("lastName"));
                    Address address = getAddressByAdressId(readerPerson.GetInt32(readerPerson.GetOrdinal("addressId_FK")));
                    String phone = readerPerson.GetString(readerPerson.GetOrdinal("phone"));
                    DateTime dateOfBirth = readerPerson.GetDateTime(readerPerson.GetOrdinal("dateOfBirth"));
                    Boolean isAdmin = readerPerson.GetBoolean(readerPerson.GetOrdinal("isAdmin"));

                    user.firstName = firstName;
                    user.lastName = lastName;
                    user.address = address;
                    user.phoneNo = phone;
                    user.dateOfBirth = dateOfBirth;
                    user.isAdmin = isAdmin;
                }
            }
            return user;
        }


        /// <summary>
        /// Get an address by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Address</returns>
        private Address getAddressByAdressId(int id)
        {
            Address a = new Address();

            string getAddressQuery = "select street, houseNo, zip_FK from Address where id = @ID";
            string getZipCityQuery = "select zip, city from ZipCity where zip = @ZIP";

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getAddressQuery, con))
            using (SqlCommand cmd2 = new SqlCommand(getZipCityQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("ID", id);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    a.street = reader.GetString(reader.GetOrdinal("street"));
                    a.houseNo = reader.GetString(reader.GetOrdinal("houseNo"));
                    a.zip = reader.GetString(reader.GetOrdinal("zip_FK"));
                }
                reader.Close();
                cmd2.Parameters.AddWithValue("ZIP", a.zip);
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    a.city = reader2.GetString(reader2.GetOrdinal("city"));
                }
            }
            return a;
        }

        /// <summary>
        /// Set the AspNet ID to match the ID in User/EventOrganizer by their email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="aspNetId"></param>
        /// <returns>Boolean</returns>
        public bool setAspNetIdByEmail(string email, string aspNetId)
        {
            string setAspNetIdQuery = "UPDATE Person set aspNetUsersId_FK = @ASPNETID where email = @EMAIL";

            int affectedRows = 0;

            using (SqlConnection con = new SqlConnection(connectionString))
            using ( SqlCommand cmd = new SqlCommand(setAspNetIdQuery, con))
            {
                con.Open();
                cmd.Parameters.AddWithValue("ASPNETID", aspNetId);
                cmd.Parameters.AddWithValue("EMAIL", email);
                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows > 0;
        }

        /// <summary>
        /// Get a list of all eventorganizers
        /// </summary>
        /// <returns>List of EventOrganizer</returns>
        public List<EventOrganizer> getAllEventOrganizers()
        {
            string getAllEventOrganizersQuery = "SELECT organizerId, email_FK from eventOrganizer";
            String getPersonByEmailQuery = "SELECT firstName, lastName, addressId_FK, phone, dateOfBirth, isAdmin FROM Person where email=@EMAIL";
            List<EventOrganizer> eList = new List<EventOrganizer>();

            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(getAllEventOrganizersQuery, con))
            using (SqlCommand cmd2 = new SqlCommand(getPersonByEmailQuery, con))
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    EventOrganizer eventOrganizer = new EventOrganizer();
                    eventOrganizer.organizerId = reader.GetString(reader.GetOrdinal("organizerId"));
                    eventOrganizer.email = reader.GetString(reader.GetOrdinal("email_FK"));
                    eList.Add(eventOrganizer);
                }
                reader.Close();
                foreach (EventOrganizer e in eList)
                {
                    cmd2.Parameters.AddWithValue("EMAIL", e.email);
                    reader = cmd2.ExecuteReader();
                    while (reader.Read())
                    {
                        e.address = getAddressByAdressId(reader.GetInt32(reader.GetOrdinal("addressId_FK")));
                        e.firstName = reader.GetString(reader.GetOrdinal("firstName"));
                        e.lastName = reader.GetString(reader.GetOrdinal("lastName"));
                        e.phoneNo = reader.GetString(reader.GetOrdinal("phone"));
                        e.dateOfBirth = reader.GetDateTime(reader.GetOrdinal("dateOfBirth"));
                        e.isAdmin = reader.GetBoolean(reader.GetOrdinal("isAdmin"));
                    }
                    reader.Close();
                }
            }
            return eList;
        }
    }
}
