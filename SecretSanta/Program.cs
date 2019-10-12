using System;
using System.Collections.Generic;
using System.Net.Mail;

namespace SecretSanta
{
    class Program
    {
        static void Main(string[] args)
        {
            // enter participants' email
            List<string> email = new List<string> {  };
            Dictionary<string, string> result = GetList(email);

            try
            {
                // set up SMTP
                string username = "";
                string password = "";
                string server = "";
                SmtpClient smtpClient = new SmtpClient(server);
                smtpClient.Port = 587;
                smtpClient.Credentials = new System.Net.NetworkCredential(username, password);

                // send email to purchaser
                foreach(KeyValuePair<string, string> pair in result)
                {  
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("SecretSanta@Gmail.com");
                    mail.To.Add(pair.Key);
                    mail.Subject = "Secret Santa";
                    mail.Body = $"You are buying for: {pair.Value} \n Merry Christmas";

                    smtpClient.Send(mail);
                }
                
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static Dictionary<string, string> GetList(List<string> names)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            List<string> pick = new List<string>();
            
            pick = CloneList(names, pick);

            var random = new Random();

            foreach(var name in names)
            {
                List<string> listOfPeople = new List<string>();
                listOfPeople = CloneList(names, listOfPeople);
                // remove picker from pool
                listOfPeople.Remove(name);

                // list without picker and people that has been picked
                List<string> common = NameExists(pick, listOfPeople);
                int length = common.Count;

                // choose at random
                string chosen = common[random.Next(length)];
                result[name] = chosen;

                // remove chosen from pool
                pick.Remove(chosen);
            }
            return result;
        }

        static List<string> CloneList(List<string> item, List<string> empty)
        {
            foreach(var i in item)
            {
                empty.Add(i);       
            }
            return empty;
        }

        static List<string> NameExists(List<string>list1, List<string>list2)
        {
            List<string> exits = new List<string>();
            foreach(var item in list1)
            {
                if(list2.Contains(item))
                {
                    exits.Add(item);
                }
            }
            return exits;
        }
    }
}
