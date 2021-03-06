//*********************************************************************
//The interface constraint enables you to specify an interface that a type argument must implement.
//It lets you use the members of the interface within the generic class.
//*********************************************************************

//Interface constraint uses this form of the where clause: where T : interface-name

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C02.Generics
{
    public interface IPhoneNumber
    {
        string Number {get; set;}
        string Name {get; set;}
    }

    // A class of phone numbers for friends.
    // It implements IPhoneNumber.
    class Friend : IPhoneNumber
    {
        public Friend(string n, string num, bool wk)
        {
            Name = n;
            Number = num;
            IsWorkNumber = wk;
        }
        public bool IsWorkNumber { get; private set; }
        // Implement IPhoneNumber.
        public string Number { get; set; }
        public string Name { get; set; }
    }

    // A class of phone numbers for suppliers.
    class Supplier : IPhoneNumber
    {
        public Supplier(string n, string num)
        {
            Name = n;
            Number = num;
        }

        // Implement IPhoneNumber.
        public string Number { get; set; }
        public string Name { get; set; }
    }


    // Notice that this class does not implement IPhoneNumber.
    class EmailFriend
    {
        // ...
    }


    // PhoneList can manage any type of phone list
    // as long as it implements IPhoneNumber.
    class PhoneList<T> where T : IPhoneNumber
    {
        T[] phList;
        int end;
        public PhoneList()
        {
            phList = new T[10];
            end = 0;
        }
        public bool Add(T newEntry)
        {
            if (end == 10) return false;
            phList[end] = newEntry;
            end++;
            return true;
        }
        // Given a name, find and return the phone info.
        public T FindByName(string name)
        {
            for (int i = 0; i < end; i++)
            {
                // Name can be used because it is a member of
                // IPhoneNumber, which is the interface constraint.
                if (phList[i].Name == name)
                    return phList[i];
            }
            // Name not in list.
            throw new Exception();
        }
        // Given a number, find and return the phone info.
        public T FindByNumber(string number)
        {
            for (int i = 0; i < end; i++)
            {
                // Number can be used because it is also a member of
                // IPhoneNumber, which is the interface constraint.
                if (phList[i].Number == number)
                    return phList[i];
            }
            // Number not in list.
            throw new Exception();
        }
    }


    class InterfaceConstraint
    {
        public static void Start()
        {
            // The following code is OK because Friend
            // implements IPhoneNumber.
            PhoneList<Friend> plist = new PhoneList<Friend>();
            plist.Add(new Friend("Tom", "555-1234", true));
            plist.Add(new Friend("Gary", "555-6756", true));
            plist.Add(new Friend("Matt", "555-9254", false));
            try
            {
                // Find the number of a friend given a name.
                Friend frnd = plist.FindByName("Gary");
                Console.Write(frnd.Name + ": " + frnd.Number);
                if (frnd.IsWorkNumber)
                    Console.WriteLine(" (work)");
                else
                    Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Not Found");
            }
            Console.WriteLine();
            
            // The following code is also OK because Supplier
            // implements IPhoneNumber.
            PhoneList<Supplier> plist2 = new PhoneList<Supplier>();
            plist2.Add(new Supplier("Global Hardware", "555-8834"));
            plist2.Add(new Supplier("Computer Warehouse", "555-9256"));
            plist2.Add(new Supplier("NetworkCity", "555-2564"));
            try
            {
                // Find the name of a supplier given a number.
                Supplier sp = plist2.FindByNumber("555-2564");
                Console.WriteLine(sp.Name + ": " + sp.Number);
            }
            catch (Exception)
            {
                Console.WriteLine("Not Found");
            }
            // The following declaration is invalid because EmailFriend
            // does NOT implement IPhoneNumber.
            // PhoneList<EmailFriend> plist3 =
            // new PhoneList<EmailFriend>(); // Error!
        }

    }
}
