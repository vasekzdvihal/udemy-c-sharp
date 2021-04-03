using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Proxy.ViewModel.Annotations;

namespace Proxy.ViewModel
{
    // mvvm
    // model (Person)
    // view (UI)
    // viewmodel
    
    public class Person
    {
        public string FirstName, LastName;
    }

    public class PersonViewModel : INotifyPropertyChanged
    {
        private Person person;

        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public string FirstName
        {
            get => person.FirstName;
            set
            {
                if(person.FirstName == value) return;
                person.FirstName = value;
                OnPropertyChanged();
            }
        }
        
        public string LastName
        {
            get => person.LastName;
            set
            {
                if(person.LastName == value) return;
                person.LastName = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(FullName));
            }
        }

        public string FullName
        {
            get => $"{FirstName} {LastName}".Trim();
            set
            {
                if (value == null)
                {
                    FirstName = LastName = null;
                    return;
                }

                var items = value.Split();
                if (items.Length > 0)
                    FirstName = items[0]; // may use npc
                if (items.Length > 1)
                    LastName = items[1];
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            person.FirstName = "Jane";
            person.LastName = "Doe";

            var personView = new PersonViewModel(person);
            Console.WriteLine(personView.FullName);
        }
    }
}