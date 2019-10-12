# SecretSanta

# Set up participants
```
List<string> email = new List<string> { "example@example.com", "example1@example.com", "example2@example.com" };
```

# Setting up SMTP
```
string username = "";
string password = "";
string server = "";
SmtpClient smtpClient = new SmtpClient(server);
smtpClient.Port = 587;
smtpClient.Credentials = new System.Net.NetworkCredential(username, password);
```
