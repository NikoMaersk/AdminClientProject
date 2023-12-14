using AdminClient.Model;
using AdminClient.Model.DataObjects;
using AdminClient.Utility;
using AdminClient.Utility.HttpHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;

namespace AdminClient.ViewModels
{
    internal class UsersViewModel : ViewModelBase
    {
        public ObservableCollection<Users> UsersList { get; set; }
        public ObservableCollection<string> NamesList { get; set; }
        public ObservableCollection<Name> ListOfMatchedNames { get; set; }
        public ICommand saveUserChanges {  get; set; }

        private Users _users;
        private string _partner { get; set; }
        private Users _partnerAsUser { get; set; }
        private string _name { get; set; }
        private string _email { get; set; }
        private string _password { get; set; }


        public UsersViewModel()
        {
            var test = DataRepo.instance.users;
            saveUserChanges = new DelegateCommand(() => saveChanges());
            UsersList = new ObservableCollection<Users>(DataRepo.instance.users);
            NamesList = new ObservableCollection<string>();
            ListOfMatchedNames = new ObservableCollection<Name>();
        }

        public Users selectedUser
        {
            get { return _users; }
            set
            {
                ListOfMatchedNames.Clear();
                NamesList.Clear();
                _users = value;
                _partner = value.Partner;
                if (_users.Partner != null)
                {
                    _partnerAsUser = getPartnerObject(value.Partner);
                    ListOfMatchedNames = new ObservableCollection<Name>(GetMatchedNames());
                }
                
                _name = value.UserName;
                _email = value.Email;

                getDataNames();

                OnPropertyChanged();
                OnPropertyChanged("name");
                OnPropertyChanged("email");
                OnPropertyChanged("SelectedPartner");
                OnPropertyChanged("ListOfMatchedNames");

            }

        }
        public string SelectedPartner
        {
            get => _partner;
            set { _partner = value;  OnPropertyChanged(); }
            }
        public string name { get => _name; set => _name = value; }
        public string email { get => _email; set => _email= value; }
        public string password { get => _password; set => _password = value; }
        
        private void getDataNames()
        {
            if (selectedUser != null)
            {
                foreach (string Name in selectedUser.Names)
                {
                    NamesList.Add(Name);
                }

            }

        }
        private Users getPartnerObject(string name)
        {
            return DataRepo.instance.GetUsersFromString(name);
        }
        private List<Name> GetMatchedNames()
        {
            return DataRepo.instance.GetMatchedNames(_users, _partnerAsUser);
        }
        private async void saveChanges()
        {
             string oldEmail = _users.Email;
            _users.UserName = _name;
            _users.Email = _email;
            var client = HttpConnectionFactory.Instance.CreateNewHttpConnection<Users>() as HttpUserConnection;
            //await client.Patch(_users,_password, oldEmail);
        }
    }
}
