using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Linq;

namespace MyMvcApp.Controllers;

public class UserController : Controller
{
    public static System.Collections.Generic.List<User> userlist = new System.Collections.Generic.List<User>();

    // GET: User
    /// <summary>
    /// Displays a list of users.
    /// </summary>
    /// <returns>A view displaying the list of users.</returns>
    public ActionResult Index(string searchString)
    {
        // Implement the Index method here
        var users = from u in userlist
                select u;

    if (!String.IsNullOrEmpty(searchString))
    {
        users = users.Where(u => u.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase));
    }

    return View(users.ToList());
    }
   

    // GET: User/Details/5
    public ActionResult Details(int id)
    {
        // Implement the details method here
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // GET: User/Create
    /// <summary>
    /// Displays a view to create a new user.
    /// </summary>
    public ActionResult Create()
    {
        // Display the create user view
        var user = new User();
        return View(user);
    }

    // POST: User/Create
    [HttpPost]
    public ActionResult Create(User user)
    {
        // Implement the Create method (POST) here
        if (ModelState.IsValid)
        {
            userlist.Add(user);
            return RedirectToAction("Index");
        }
        return View(user);
    }

    // GET: User/Edit/5
    public ActionResult Edit(int id)
    {
        // This method is responsible for displaying the view to edit an existing user with the specified ID.
        // It retrieves the user from the userlist based on the provided ID and passes it to the Edit view.
        // If no user is found with the provided ID, it returns a HttpNotFoundResult.
        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: User/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, User user)
    {
        // This method is responsible for handling the HTTP POST request to update an existing user with the specified ID.
        // It receives user input from the form submission and updates the corresponding user's information in the userlist.
        // If successful, it redirects to the Index action to display the updated list of users.
        // If no user is found with the provided ID, it returns a HttpNotFoundResult.
        // If an error occurs during the process, it returns the Edit view to display any validation errors.
        if (ModelState.IsValid)
        {
            var existingUser = userlist.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            // Update other properties as needed

            return RedirectToAction("Index");
        }
        return View(user);
    }

    // GET: User/Delete/5
    public ActionResult Delete(int id)
    {
        // This method is responsible for displaying the view to delete an existing user with the specified ID.
        // It retrieves the user from the userlist based on the provided ID and passes it to the Delete view.
        // If no user is found with the provided ID, it returns a HttpNotFoundResult.
        // If an error occurs during the process, it returns the Delete view to display any validation errors.
        // Implement the Delete method here

            var user = userlist.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
        // Implement the Delete method (POST) here
        // This method is responsible for handling the HTTP POST request to delete an existing user with the specified ID.
        // It removes the user from the userlist based on the provided ID.
        // If successful, it redirects to the Index action to display the updated list of users.
        // If no user is found with the provided ID, it returns a HttpNotFoundResult.
        // If an error occurs during the process, it returns the Delete view to display any validation errors

        var user = userlist.FirstOrDefault(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }

        userlist.Remove(user);
        return RedirectToAction("Index");
        }
}
