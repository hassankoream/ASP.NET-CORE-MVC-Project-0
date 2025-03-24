using System;

public class Class1
{
    public Class1()
    {
        #region 1 - IEnumerable Vs IQueryable

        /*
         
        ### **Difference Between `IEnumerable` and `IQueryable` in C#**

Both `IEnumerable` and `IQueryable` are used to work with collections of data, but they have key differences in terms of where and how the data processing occurs.

| Feature        | `IEnumerable<T>` | `IQueryable<T>` |
|---------------|----------------|----------------|
| **Definition** | Represents a forward-only, read-only collection that can be iterated using `foreach`. | Represents a queryable collection that allows LINQ-to-SQL (or Entity Framework) queries to be executed on a database. |
| **Namespace** | `System.Collections.Generic` | `System.Linq` |
| **Execution** | Deferred execution, but processes data in-memory. | Deferred execution and translates queries to SQL for database execution. |
| **Performance** | Less efficient for large datasets because filtering occurs in-memory. | More efficient for large datasets because filtering happens at the database level. |
| **Use Case** | Suitable for in-memory collections like Lists, Arrays, and Collections. | Suitable for querying databases using LINQ-to-SQL or Entity Framework. |
| **Query Execution** | Executes on the client side (pulls all data and then filters). | Executes on the server side (filters before fetching data). |
| **Modification** | Cannot modify the collection during iteration. | Cannot modify data directly, as it represents a queryable source. |

### **Examples**

#### **Using `IEnumerable` (In-Memory Processing)**
```csharp
List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6 };
IEnumerable<int> evenNumbers = numbers.Where(n => n % 2 == 0);  // Filters in-memory

foreach (var num in evenNumbers)
{
    Console.WriteLine(num); // Output: 2, 4, 6
}
```
- The entire list is loaded into memory before filtering.

#### **Using `IQueryable` (Database Processing)**
```csharp
using (var context = new MyDbContext())
{
    IQueryable<User> users = context.Users.Where(u => u.Age > 18); // Query translated to SQL
    
    foreach (var user in users)
    {
        Console.WriteLine(user.Name);
    }
}
```
- The filtering (`WHERE Age > 18`) is applied at the database level before fetching data.

### **When to Use What?**
- Use **`IEnumerable<T>`** for **in-memory collections** (e.g., `List<T>`, `Array<T>`).
- Use **`IQueryable<T>`** when **querying a database** to leverage **lazy loading** and optimize performance.

Would you like me to explain any part in more detail? 🚀
         
         
         */
        #endregion

        #region 2 - Client-Side Validation

        /*
        
        We use Client-Side Validation to prevent too many Invaild requests to the server.
        This is not include tools like Postman, attackers still have the chance to send many requests.
        
        - open wwwroot/lib/Jquery/: then drag Jqueryvalidations to the create and edit view, in order to make client side vaildation
        - Add it as a section to call it anytime, anywhere you need, not just at @RenderBody().
        - Add it JQuery Vaildation after JQuery scripts.  @await RenderSectionAsync("ValidationScripts", required: false)
        - Add Inisde wwwroot/js/js.site a few lines to help user udnderstand what is missing in the form [$(document).ready(function () {
    $('form input, form select, form textarea').on('blur', function () {
        $(this).vaild();
    });
});]
        -
         
         */
        #endregion

        #region 3 - AntiForgeryToken [Action Filter]

        /*
         
        - AntiForgeryToken is generted inside the Form with the help of ASP
        - But Developer could send requests through other tools like postman
        - to avoid this we we will data annotation on the post Actions inside the controllers [ValidateAntiForgeryToken]
        -In order
        
        
        
        
        ### **What is `AntiForgeryToken` in ASP.NET Core Forms?**
`AntiForgeryToken` is a security mechanism in **ASP.NET Core** that helps protect web applications from **Cross-Site Request Forgery (CSRF) attacks**.

### **CSRF Attack Overview**
A **CSRF attack** tricks an authenticated user into making unintended requests to a web application. For example:
- A user logs into a banking website.
- The user unknowingly visits a malicious site that submits a fund transfer request using their **authenticated session**.
- The banking website processes the request because the user is already logged in.

To prevent this, ASP.NET Core provides **Anti-Forgery Tokens** to ensure that form submissions are coming from trusted sources.

---

### **How `AntiForgeryToken` Works**
ASP.NET Core generates two tokens:
1. **A hidden form field (`__RequestVerificationToken`)** stored in the HTML form.
2. **A cookie token** stored in the user's browser.

When the form is submitted:
- The server **validates** if both tokens match.
- If they don’t match (or are missing), the request is **rejected**.

---

### **How to Use `AntiForgeryToken` in ASP.NET Core**
#### **1️⃣ Add `@Html.AntiForgeryToken()` in the Razor View**
In **Razor Pages (CSHTML)**, include the token inside your `<form>`:
```html
<form asp-action="SubmitForm" asp-controller="Home" method="post">
    @Html.AntiForgeryToken()
    <input type="text" name="username" placeholder="Enter name">
    <button type="submit">Submit</button>
</form>
```
🔹 This generates a **hidden input field** with the CSRF token.

---

#### **2️⃣ Enable CSRF Protection in the Controller**
In the corresponding **controller action**, add `[ValidateAntiForgeryToken]` to enforce CSRF validation:
```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult SubmitForm(string username)
{
    // Process form data securely
    return View();
}
```
🔹 If the token is missing or invalid, ASP.NET **rejects the request**.

---

### **Alternative: Implicit Token in Razor Pages**
If you're using **ASP.NET Core Razor Pages**, CSRF protection is **automatically enabled** when using the `form` tag helper:
```html
<form method="post">
    <input type="text" name="username" placeholder="Enter name">
    <button type="submit">Submit</button>
</form>
```
🔹 Here, **ASP.NET automatically adds** the Anti-Forgery token.

---

### **Handling CSRF in AJAX Requests**
If you send **AJAX requests**, you need to include the token manually in the headers.

#### **1️⃣ Add the Anti-Forgery Token to the HTML**
```html
<input type="hidden" id="csrfToken" name="__RequestVerificationToken" value="@Html.AntiForgeryToken()" />
```

#### **2️⃣ Include Token in AJAX Request**
```javascript
$.ajax({
    url: '/Home/SubmitForm',
    type: 'POST',
    data: { username: 'Hassan' },
    headers: {
        "RequestVerificationToken": $("#csrfToken").val()
    },
    success: function (response) {
        console.log("Form submitted successfully");
    }
});
```

---

### **Key Takeaways**
✅ **Prevents CSRF Attacks** by verifying form submissions.  
✅ **Automatically added in Razor Views** when using `@Html.AntiForgeryToken()`.  
✅ **Required in POST requests**, but not for GET requests.  
✅ **For AJAX requests**, you must manually include the token in the headers.  

Would you like me to demonstrate a real example with ASP.NET Core Razor Pages? 🚀
         
         
         */
        #endregion

        #region 4 - Partial Views

        /*
         
         
         
         */
        #endregion
    }
}
