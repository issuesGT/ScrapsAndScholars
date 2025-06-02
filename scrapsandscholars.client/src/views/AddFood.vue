<template>
  <section id="container">
    <div class="parallax"></div>
    <h1>Whats your offer?</h1>
  </section>

  <section class="formSection">
    <form @submit.prevent="submitFood">
      <section class="barcode">
        <button type="button">Scan barcode</button>
      </section>
      <label for="title">Food Name</label>
      <input type="text" id="title" v-model="form.title" required />

      <label for="date">Date Cooked</label>
      <input type="date" id="date" v-model="form.dateCooked" required />

      <label for="description">Description</label>
      <textarea id="description" v-model="form.description" rows="4" required></textarea>

      <label for="portion">Portion Size</label>
      <input type="text" id="portion" v-model="form.portionSize" required />

      <label for="image">Upload Image</label>
      <input type="file" id="image" @change="handleImageUpload" required/>

      <button type="submit">Post Food</button>
    </form>
  </section>
</template>

<script>
import axios from 'axios'

export default {
  name: "AddFood",
    data() {
    // Initialize the form data and student information.
    const student = JSON.parse(localStorage.getItem('student')) || {};
    return {
      // Use the student data from localStorage or set defaults.
      studentId: student.id || null,
      studentEmail: student.email || "Anonymous",
      form: {
        title: "",
        dateCooked: "",
        description: "",
        portionSize: "",
        image: null
      }
    };
  },
  methods: {
    // Handle image upload and store the file in the form data.
    handleImageUpload(event) {
      // Check if a file is selected.
      this.form.image = event.target.files[0];
    },
    // Submit the food post to the server.
    async submitFood() {
      // Current date.
      const now = new Date();

      // Format the time posted as a string.
      const timePosted = now.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });

      // Create a FormData object to send the form data.
      const formData = new FormData();
      formData.append("title", this.form.title);
      formData.append("dateCooked", this.form.dateCooked);
      formData.append("description", this.form.description);
      formData.append("portionSize", this.form.portionSize);
      formData.append("distance", "1");
      formData.append("timePosted", timePosted);
      formData.append("postedBy", this.studentEmail);
      // Append the image file if it exists.
      if (this.form.image) {
        formData.append("image", this.form.image);
      }
      try {
        // Ensure the student ID is set before making the request.
        await axios.post(`/api/student/login/uploadPost/${this.studentId}`, formData, {
          headers: { "Content-Type": "multipart/form-data" }
        });
        // Notify the user.
        alert("Food posted!");
        // Redirect to the food page after successful post.
        this.$router.push("/food");
      } catch (error) {
        console.error("Failed to post food", error);
        alert("Something went wrong.");
      }
    }
  }
}
</script>


<style scoped>
  #container {
    background-image: url("../assets/images/meal.jpg");
    background-attachment: fixed;
    background-position: center;
    background-size: cover;
    height: 50vh;
    width: 100%;
    color: #FFFFFF;
    display: flex;
    justify-content: center;
    align-items: center;
  }

    #container > .parallax {
      position: absolute;
      top: 0;
      left: 0;
      width: 100%;
    }

    #container > h1 {
      z-index: 0;
      font-size: 60px;
      text-align: center;
      width: 35%;
      text-shadow: 1px 1px 2px black;
      font-family: 'Exo 2', sans-serif;
      animation: leftin2 2s;
    }

  @keyframes leftin2 {
    from {
      opacity: 0;
    }

    to {
      opacity: 100;
    }
  }
  .barcode button {
    margin-top: 1.5rem;
    padding: 0.75rem;
    background: rgb(30, 138, 186);
    color: white;
    border: groove;
    border-radius: 5px;
    font-weight: bold;
    cursor: pointer;
    width: 600px;
  }

  .formSection {
    width: 100%;
    max-width: 600px;
    margin: 2rem auto;
    padding: 1rem;
    background: #fff;
    border-radius: 10px;
    font-family: 'Exo 2', sans-serif;
    box-shadow: 0 4px 8px rgba(0,0,0,0.1);
  }

  form {
    display: flex;
    flex-direction: column;
  }

  label {
    margin-top: 1rem;
    font-weight: bold;
    color: #333;
  }

  input, textarea {
    padding: 0.5rem;
    margin-top: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 5px;
    font-family: inherit;
  }

  button {
    margin-top: 1.5rem;
    padding: 0.75rem;
    background: rgba(54, 69, 79, 1);
    color: white;
    border: none;
    border-radius: 5px;
    font-weight: bold;
    cursor: pointer;
  }

    button:hover {
      background: rgba(54, 69, 79, 1);
    }
</style>
