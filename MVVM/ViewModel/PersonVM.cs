namespace MVVM.ViewModel
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class PersonVM : INotifyPropertyChanged
    {
        private Person person;

        private string oldFirstName;

        public PersonVM(Person person, ICommand removeCommand)
        {
            this.Person = person;
            this.oldFirstName = person.FirstName;
            this.Selected = false;
            this.RemoveCommand = removeCommand;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Person Person
        {
            get
            {
                return this.person;
            }
            set
            {
                this.person = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.Person.LastName;
            }
            set
            {
                this.Person.LastName = value;
                this.FireOnPropertyChanged();
            }
        }

        public bool Selected
        {
            get
            {
                return this.Person.FirstName == "SELECTED";
            }
            set
            {
                if (value)
                {
                    this.Person.FirstName = "SELECTED";
                }
                else
                {
                    this.Person.FirstName = this.oldFirstName;
                }

                this.FireOnPropertyChanged(nameof(this.Person));
            }
        }

        protected virtual void FireOnPropertyChanged([CallerMemberName] string propName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public ICommand RemoveCommand 
        { 
            get; 
            
            private set; 
        }
    }
}
