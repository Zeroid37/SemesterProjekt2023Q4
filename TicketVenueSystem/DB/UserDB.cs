using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVenueSystem.Model;

namespace TicketVenueSystem.DB
{
    internal class UserDB : UserDAO {
        public Boolean addUserToDB(User user) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            int insertedRowsNoPerson = 0;
            int insertedRowsNoUser = 0;
            string queryStringPerson = "insert into Person(firstName, lastName, addressId_FK, phone, email, password, isAdmin, type)" +
                                 "values(@FIRSTNAME, @LASTNAME, @ADDRESSID_FK, @PHONE, @EMAIL, @PASSWORD, @ISADMIN, @TYPE)";
            string queryStringUser = "insert into User(userId, email_fk) values(@USERID, @EMAIL_FK)";

            using (con) {
                using (SqlCommand cmd = new SqlCommand(queryStringPerson, con)) {
                    int addressId = addAddressToDB(user.address);

                    cmd.Parameters.AddWithValue("FIRSTNAME", user.firstName);
                    cmd.Parameters.AddWithValue("LASTNAME", user.lastName);
                    cmd.Parameters.AddWithValue("ADDRESSID_FK", addressId);
                    cmd.Parameters.AddWithValue("PHONE", user.phoneNo);
                    cmd.Parameters.AddWithValue("EMAIL", user.email);
                    cmd.Parameters.AddWithValue("PASSWORD", user.password);
                    cmd.Parameters.AddWithValue("ISADMIN", user.isAdmin);
                    cmd.Parameters.AddWithValue("TYPE", user.type);

                    insertedRowsNoPerson = cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(queryStringUser, con)) {
                    cmd.Parameters.AddWithValue("USERID", user.userId);
                    cmd.Parameters.AddWithValue("EMAIL_FK", user.email);

                    insertedRowsNoUser = cmd.ExecuteNonQuery();
                }


            }
            return (insertedRowsNoPerson > 0 && insertedRowsNoUser > 0);
        }

        public Boolean addEventOrganizerToDB(EventOrganizer eventOrganizer) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            int insertedRowsNoPerson = 0;
            int insertedRowsNoOrganizer = 0;
            string queryStringPerson = "insert into Person(firstName, lastName, addressId_FK, phone, email, password, isAdmin, type)" +
                                 "values(@FIRSTNAME, @LASTNAME, @ADDRESSID_FK, @PHONE, @EMAIL, @PASSWORD, @ISADMIN, @TYPE)";
            string queryStringUser = "insert into EventOrganizer(organizerId, email_fk) values(@ORGANIZERID, @EMAIL_FK)";

            using (con) {
                using (SqlCommand cmd = new SqlCommand(queryStringPerson, con)) {
                    int addressId = addAddressToDB(eventOrganizer.address);

                    cmd.Parameters.AddWithValue("FIRSTNAME", eventOrganizer.firstName);
                    cmd.Parameters.AddWithValue("LASTNAME", eventOrganizer.lastName);
                    cmd.Parameters.AddWithValue("ADDRESSID_FK", addressId);
                    cmd.Parameters.AddWithValue("PHONE", eventOrganizer.phoneNo);
                    cmd.Parameters.AddWithValue("EMAIL", eventOrganizer.email);
                    cmd.Parameters.AddWithValue("PASSWORD", eventOrganizer.password);
                    cmd.Parameters.AddWithValue("ISADMIN", eventOrganizer.isAdmin);
                    cmd.Parameters.AddWithValue("TYPE", eventOrganizer.type);

                    insertedRowsNoPerson = cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(queryStringUser, con)) {
                    cmd.Parameters.AddWithValue("USERID", eventOrganizer.organizerId);
                    cmd.Parameters.AddWithValue("EMAIL_FK", eventOrganizer.email);

                    insertedRowsNoOrganizer = cmd.ExecuteNonQuery();
                }


            }
            return (insertedRowsNoPerson > 0 && insertedRowsNoOrganizer > 0);
        }

        public int addAddressToDB(Address address) {
            DBConnect DBC = DBConnect.getInstance();
            SqlConnection con = DBC.getConnection();

            int addressId = -1;

            string queryStringZipCity = "insert into ZipCity(zip, city) values(@ZIP, @CITY)";

            string queryStringAddress = "insert into Address(street, houseNo, zip_FK) values (@STREET, @HOUSENO, @ZIP_FK)";

            using (con) {
                using (SqlCommand cmd = new SqlCommand(queryStringZipCity, con)) {
                    cmd.Parameters.AddWithValue("ZIP", address.zip);
                    cmd.Parameters.AddWithValue("CITY", address.city);
                    cmd.ExecuteNonQuery();
                }
                using (SqlCommand cmd = new SqlCommand(queryStringAddress, con)) {
                    cmd.Parameters.AddWithValue("STREET", address.zip);
                    cmd.Parameters.AddWithValue("HOUSENO", address.houseNo);
                    cmd.Parameters.AddWithValue("ZIP_FK", address.zip);

                    addressId = Convert.ToInt32(cmd.ExecuteScalar());
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
