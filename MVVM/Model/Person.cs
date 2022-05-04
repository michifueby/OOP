namespace MVVM
{
    using System;

    public class Person
    {
        private string firstName;

        private string lastName;

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName
        {
            get 
            { 
                return firstName; 
            }
            
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Invalid value!");
                }

                this.firstName = value;
            }
        }

        public string LastName
        {
            get 
            { 
                return lastName; 
            }
            
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Invalid value!");
                }

                this.lastName = value;
            }
        }
    }
}

