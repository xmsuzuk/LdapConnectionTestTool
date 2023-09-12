using System;
using System.DirectoryServices;
using System.Linq;

namespace VodafoneLdapTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Connection String formatı böyle olmalıdır: 'LDAP://DC=yourdomain,DC=com'");
            Console.Write("Connection String girin: ");
            string ldapConnectionString = Console.ReadLine();
            Console.WriteLine("Girilen Değer: '" + ldapConnectionString + "'");

            Console.Write("Kullaınıcı adı girin: ");
            string ldapUsername = Console.ReadLine();
            Console.WriteLine("Girilen Değer: '" + ldapUsername + "'");

            Console.Write("Şifre girin: ");
            string ldapPassword = Console.ReadLine();
            Console.WriteLine("Girilen Değer: '" + ldapPassword + "'");

            drawLineSeperator();

            try
            {
                DirectoryEntry entry = new DirectoryEntry(ldapConnectionString, ldapUsername, ldapPassword);

                DirectorySearcher searcher = new DirectorySearcher(entry);
                searcher.Filter = "(&(objectClass=user))";
                searcher.PageSize = 1000;

                foreach (SearchResult result in searcher.FindAll())
                {
                    if (result.Properties["sAMAccountName"].Count > 0)
                    {
                        string userName = result.Properties["sAMAccountName"][0].ToString();
                        Console.WriteLine(userName);
                    }
                }

                drawLineSeperator();

                Console.WriteLine("Başarılı çalıştı.");
                Console.ReadLine();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Bir hata oluştu.");
                Console.WriteLine("Mesaj: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("InnerExp Mesaj: " + ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine("InnerExp yok.");
                }
                Console.WriteLine("Stack Trace" + ex.StackTrace);
                Console.ReadLine();
                Console.ReadLine();
            }
        }

        static void drawLineSeperator()
        {
            Console.WriteLine("");
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("");
        }
    }
}
