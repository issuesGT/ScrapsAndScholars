using Microsoft.AspNetCore.Mvc;
using ScrapsAndScholars.Server.Model;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace ScrapsAndScholars.Server.Controllers
{
    [ApiController]
    [Route("api/student/login")]
    public class StudentController : ControllerBase
    {
        // Path to the JSON file containing student data
        private readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Properties", "students.json");

        // api to handle student login
        [HttpPost]
        // This call handles the login of a student
        public IActionResult Login([FromBody] LoginRequest login)
        {
            // Validate the incoming request
            var students = LoadStudents();

            // Check if the login request is valid
            var student = students.FirstOrDefault(studentObject =>
                // Check if email and password match
                studentObject.Email.Equals(login.Email, StringComparison.OrdinalIgnoreCase) &&
                studentObject.Password == login.Password);

            if (student == null)
                return Unauthorized();

            // If valid, return student details
            return Ok(new
            {
                id = student.Id,
                email = student.Email,
                credits = student.Credits
            });
        }

        // api to get student details by ID
        [HttpPost("uploadPost/{id}")]
        // This call handles the upload of a food post by a student
        public async Task<IActionResult> UploadPost(int id)
        {
            var form = Request.Form;

            // Load student data
            var students = LoadStudents();
            var student = students.FirstOrDefault(studentObject => studentObject.Id == id);
            if (student == null)
                return NotFound("Student not found");

            // Handle uploaded image
            var image = form.Files.GetFile("image");
            string imageUrl = string.Empty;

            if (image != null && image.Length > 0)
            {
                // Define the uploads folder path
                // This is where uploaded images will be stored
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Ensure the uploads folder exists
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                // Generate a unique file name using GUID
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                // Combine the uploads folder path with the new file name
                var filePath = Path.Combine(uploadsFolder, fileName);

                // Save the uploaded image to the server
                using var stream = new FileStream(filePath, FileMode.Create);

                // Copy the uploaded image to the file stream
                await image.CopyToAsync(stream);

                // Set the image URL to be used in the post
                imageUrl = $"/uploads/{fileName}"; // public path for frontend
            }

            // Create a new food post
            var newPost = new FoodPost
            {
                // Populate the new post with data from the form
                // Use empty string if the form field is missing or null to avoid null reference errors
                Title = form["title"].ToString() ?? string.Empty,
                DateCooked = form["dateCooked"].ToString() ?? string.Empty,
                Distance = form["distance"].ToString() ?? string.Empty,
                TimePosted = form["timePosted"].ToString() ?? string.Empty,
                PostedBy = form["postedBy"].ToString() ?? string.Empty,
                Description = form["description"].ToString() ?? string.Empty,
                Portion = form["portionSize"].ToString() ?? string.Empty,
                ImageUrl = imageUrl
            };

            // Assign a unique ID to the new post
            // This ID is generated based on the existing posts of all students
            var allPosts = students.SelectMany(studentObject => studentObject.Posts);
            newPost.Id = allPosts.Any()
                ? allPosts.Max(postObject => postObject.Id) + 1
                : 1;
            // Add the new post to the student's posts
            student.Posts.Add(newPost);
            SaveStudents(students);

            return Ok(newPost);
        }

        // api to get all posts by all students
        [HttpGet("posts")]
        // This call retrieves all posts made by students
        public IActionResult GetAllPosts()
        {
            // Load all students from the JSON file
            // and select all posts from each student
            var students = LoadStudents();
            var allPosts = students.SelectMany(s => s.Posts).ToList();
            return Ok(allPosts);
        }

        // api to get a specific post by its ID
        [HttpGet("posts/{postId}")]
        // This call retrieves a specific post by its ID
        public IActionResult GetPostById(int postId)
        {
            // Load all students and find the post with the specified ID
            // If the post is not found, return a 404 Not Found response
            var students = LoadStudents();
            var post = students.SelectMany(s => s.Posts).FirstOrDefault(p => p.Id == postId);

            if (post == null) return NotFound();
            return Ok(post);
        }
        

        [HttpPost("claimPost/{buyerId}/{postId}")]
        public IActionResult ClaimPost(int buyerId, int postId)
        {
            var students = LoadStudents();

            var buyer = students.FirstOrDefault(s => s.Id == buyerId);
            if (buyer == null)
                return NotFound("Buyer not found");

            var seller = GetOwnerByPostId(students, postId);
            if (seller == null)
                return NotFound("Seller not found");

            // Check so buyer does not claim his own Food-post.
            if(seller.Id == buyerId)
                return BadRequest("You can't claim your own food");

            var post = seller.Posts.FirstOrDefault(p => p.Id == postId);
            if (post == null)
                return NotFound("FoodPost not found");

            if (buyer.Credits < 100)
                return BadRequest("Not enough credits");

            // Perform transaction
            buyer.Credits -= 100;
            seller.Credits += 100;

            // Remove post from seller
            seller.Posts.Remove(post);

            // Save changes to file
            SaveStudents(students);

            return Ok(new
            {
                message = "Claim successful",
                buyerId = buyer.Id,
                buyerCredits = buyer.Credits,
                sellerId = seller.Id,
                sellerCredits = seller.Credits
            });
        }

        [NonAction]
        public Student GetOwnerByPostId(List<Student> students, int postId)
        {
            return students.FirstOrDefault(studentObject => studentObject.Posts.Any(postObject => postObject.Id == postId));
        }

        // api to get all posts by a specific student
        private List<Student> LoadStudents()
        {
            // Load students from the JSON file
            var json = System.IO.File.ReadAllText(filePath);

            // Deserialize the JSON content into a list of Student objects
            // If deserialization fails, return an empty list
            return JsonSerializer.Deserialize<List<Student>>(json) ?? new();
        }

        // api to save students back to the JSON file
        private void SaveStudents(List<Student> students)
        {
            // Serialize the list of students to JSON and save it to the file
            var jsonPath = filePath;

            // Ensure the directory exists
            var json = JsonSerializer.Serialize(students, new JsonSerializerOptions { WriteIndented = true });
            System.IO.File.WriteAllText(jsonPath, json);
        }
    }

}