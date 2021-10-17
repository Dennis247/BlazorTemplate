using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorTemplate.api.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    public class Person
    {
        public string Name { get; set; }
        public Contact Contact { get; set; }
    }
    public class Contact
    {
        public string PhoneNumber { get; set; }
    }

    public class PersonController : Controller
    {
        public static List<Person> Persons = new List<Person>();

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult AddPerson(Person person)
        {
            Persons.Add(person);
            return RedirectToAction("List");
        }

        [HttpGet]
        public  IActionResult List()
        {
            return View(Persons);
        }

    }

}
