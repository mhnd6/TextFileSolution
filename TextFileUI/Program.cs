using DataAccessLibrary;
using DataAccessLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace TextFileUI
{
    internal class Program
    {
        private static IConfiguration _config;
        private static string textFile;
        private static TextFileDataAccess db = new TextFileDataAccess();

        static void Main(string[] args)
        {

            InitializeConfiguration();
            textFile = _config.GetValue<string>("TextFile");

            ContactModel user1 = new ContactModel();
            user1.FirstName = "Mohanned";
            user1.LastName = "Akbar";
            user1.EmailAddresses.Add("mrmohandtv@gmail.com");
            user1.EmailAddresses.Add("m.akbar.hotmail");
            user1.PhoneNumbers.Add("5555-09877");
            user1.PhoneNumbers.Add("5555-23456");

            ContactModel user2 = new ContactModel();
            user2.FirstName = "Amjad";
            user2.LastName = "bukhari";
            user2.EmailAddresses.Add("Amjartv@gmail.com");
            user2.EmailAddresses.Add("m.Bukhri.hotmail");
            user2.PhoneNumbers.Add("5555-76285");
            user2.PhoneNumbers.Add("5555-123455");

            //CreateContact(user1);
            //CreateContact(user2);
            //GetAllContacts();

            RemoveUser();
            GetAllContacts();

            Console.ReadLine();
        }

        public static void RemoveUser()
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts.RemoveAt(0);
            db.WriteAllRecords(contacts, textFile);
        }

        public static void RemovePhoneNumberFromUser(string phoneNumber)
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts[0].PhoneNumbers.Remove(phoneNumber);
            db.WriteAllRecords(contacts, textFile);
        }
        private static void UpdateContactsFirstName(string firstName)
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts[0].FirstName = firstName;
            db.WriteAllRecords(contacts, textFile);
        }
        
        private static void GetAllContacts()
        {
            var contacts = db.ReadAllRecords(textFile);

            foreach (var contact in contacts)
            {
                Console.WriteLine($"{ contact.FirstName } { contact.LastName}");
            }
        }
        private static void CreateContact(ContactModel contact)
        {
            var contacts = db.ReadAllRecords(textFile);
            contacts.Add(contact);
            db.WriteAllRecords(contacts, textFile);
        }

        private static void InitializeConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            _config = builder.Build();
        }


    }
}
