using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AdminClient.Model
{
    internal class DataRepo
    {
        public static DataRepo instance = new DataRepo();
        public List<NameMatch> matches;
        public List<Name> names;
        public List<Users> users;


        private DataRepo() 
        {
            matches = new List<NameMatch>();
            names = new List<Name>();
            users = new List<Users>();



            UpdateAllMatches();
            UpdateAllNames();
            UpdateAllUser();
            if (names.Count == 0)
            {

                names.Add(new Name { Id = 1, name = "test", Popularity = 1, Occerrence = 111, Gender = Gender.boy });
                names.Add(new Name { Id = 1, name = "test2", Popularity = 2, Occerrence = 111, Gender = Gender.boy });
                names.Add(new Name { Id = 1, name = "test3", Popularity = 3, Occerrence = 111, Gender = Gender.boy });
                names.Add(new Name { Id = 1, name = "test4", Popularity = 4, Occerrence = 111, Gender = Gender.boy });
            }
        }
        public async void UpdateAllMatches()
        {
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<NameMatch>();
            List<NameMatch> TempList = new List<NameMatch>();
            TempList = await client.GetAll();
            matches = TempList;
        }
        public async void UpdateAllNames()
        {
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<Name>();
            List<Name> TempList = new List<Name>();
            TempList = await client.GetAll();
            names = TempList;
        }
        public async void UpdateAllUser()
        {
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<Users>();
            List<Users> TempList = new List<Users>();
            TempList = await client.GetAll();

            users = TempList;

        }



        public Name GetNameFromString(string s)
        {
            var name = from Name in names where Name.name == s select Name;
            return name.First();

        }
        public Users GetUsersFromString(string s)
        {
            var user = from Users in users where Users.Email == s select Users;
            return user.First();
        }

        public List<Name> GetMatchedNames(Users user, Users partner)
        {
            List<string> temp = user.Names;
            List<Name> matched = new List<Name>();

           
            foreach (var item in user.Names)
            {
                if (partner.Names.Contains(item))
                {
                    matched.Add(GetNameFromString(item));
                }
            }
            return matched;
        }
    }
}
