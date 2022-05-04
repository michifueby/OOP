namespace MVVM.ViewModel
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    public class PersonManagementVM
    {
        private readonly ICommand removeCommand;

        public ObservableCollection<PersonVM> People { get; set; }

        public PersonManagementVM()
        {
            this.removeCommand = new Command(obj =>
            {
                if (obj is PersonVM vm)
                {
                    this.People.Remove(vm);
                }
            });

            var people = new List<Person>()
            {
                new Person("Max", "Mustermann"),
                new Person("Markus", "Mustermann"),
                new Person("Michael", "Mustermann")
            };

            var vms = people.Select(p => new PersonVM(p, this.removeCommand));

            this.People = new ObservableCollection<PersonVM>(vms);
        }

        public ICommand AddCommand
        {
            get
            {
                return new Command(obj =>
                {
                    var person = new Person("New", "Person");
                    var personVM = new PersonVM(person, this.removeCommand);
                    this.People.Add(personVM);
                });
            }
        }
    }
}
