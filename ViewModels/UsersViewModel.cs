using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminClient.ViewModels
{
    internal class UsersViewModel : ViewModelBase
    {

        private Users _users;

        public Users selectedUser    
        {
            get { return _users; }
            set { _users = value; 
                OnPropertyChanged();
                getDataNames();
            }

        }



        public ObservableCollection<Users> UsersList { get; set; }
        public ObservableCollection<string> NamesList { get; set; }



        public UsersViewModel()
        {
            UsersList = new ObservableCollection<Users>();
            initialize();
        }

        public async void getDataUsers()
        {
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<Users>();
            List<Users> users = await client.GetAll();
            
            if (users != null)
            {
                foreach (Users user in users)
                {
                    UsersList.Add(user);
                }

            }
        }

        public void getDataNames()
        {
            if (selectedUser != null)
            {
                foreach (string Name in selectedUser.Names)
                {
                    NamesList.Add(Name);
                }

            }

        }

        public void initialize()
        {
            getDataUsers();
        }

    }
}
