using System.Text;

Console.WriteLine(DateTime.Now.ToLongDateString()/*.ToLongTimeString()*/);
StringBuilder StringBuilder = new StringBuilder();
StringBuilder.Append(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
Console.WriteLine(StringBuilder.ToString());