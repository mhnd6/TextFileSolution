using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DataAccessLibrary
{
    public class TextFileDataAccess
    {
        public List<ContactModel> ReadAllRecords(string textFile)
        {
            if (File.Exists(textFile) == false)
            {
                return new List<ContactModel>();
            }

            var lines = File.ReadAllLines(textFile);    
            List<ContactModel> records = new List<ContactModel>();

            foreach (string line in lines)
            {
                ContactModel record = new ContactModel();
                var vals = line.Split(',');

                if (vals.Length < 4)
                {
                    throw new Exception($"Invalid row of data: { line }");
                }

                record.FirstName = vals[0];
                record.LastName = vals[1];
                record.EmailAddresses = vals[2].Split(';').ToList();
                record.PhoneNumbers = vals[3].Split(';').ToList();

                records.Add(record);
            }

            return records;
        }

        public void WriteAllRecords(List<ContactModel> contacts, string textFile)
        {
            List<string> lines = new List<string>();

            foreach (var c in contacts)
            {
                lines.Add($"{ c.FirstName },{ c.LastName },{ String.Join(';', c.EmailAddresses)},{ String.Join(';', c.PhoneNumbers )}");
            }

            File.WriteAllLines(textFile, lines);
        }
    }
}
