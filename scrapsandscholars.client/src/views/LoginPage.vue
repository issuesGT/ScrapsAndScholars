<template>
  <section id="container">
    <!-- Your login form content here -->
    <div class="login-box">
      <div class="header">
        <h2 class="heading">Scraps & Scholars</h2>
        <img class="headerImage" src="/src/assets/images/loginpagepic.png" alt="icon" style="width: 75px; height: 90px;" />
      </div>
      <h3>Login</h3>
      <form @submit.prevent="handleSubmit">
        <input type="text"
               placeholder="Username"
               v-model="Username" />
        <input type="password"
               placeholder="Password"
               v-model="Password" />
        <button type="submit">Login</button>
      </form>
    </div>
  </section>
</template>

<script>
  import axios from 'axios'

  export default {
    name: 'LoginPage',
    data() {
      return {
        // Data properties for the login form.
        Username: '',
        Password: ''
      };
    },
    // hook to check if the user is already logged in.
    created() {
      // Check if the user is already logged in by looking for a login token in localStorage.
      const student = localStorage.getItem('student');

      // If a login token exists and student data is available.
      if (localStorage.getItem('loginToken') && student) {

        // Redirect the user to the home page.
        this.$router.push('/');
      }
    },
    methods: {
      // Method to handle form submission.
      async handleSubmit() {
        try {
          // Make a POST request to the login API endpoint with the provided username and password.
          const res = await axios.post('https://localhost:7174/api/student/login', {
            email: this.Username,
            password: this.Password
          });
          // If the login is successful, store the login token and student data in localStorage.
          localStorage.setItem('loginToken', 'true');

          // Store student data in localStorage for future reference.
          localStorage.setItem('student', JSON.stringify({
            id: res.data.id,
            email: res.data.email,
            credits: res.data.credits
          }));

          // Redirect the user to the home page.
          this.$router.push('/');
        } catch (err) {
          alert('Login failed');
          console.error(err);
        }
      }
    }
  };
</script>

<style scoped>
  #container {
    background-image: url("../assets/images/niceshape3.jpg");
    background-size: cover;
    height: 100vh;
    width: 100%;
    color: #FFFFFF;
    display: flex;
    justify-content: center;
    align-items: center;
  }

  .header {
      display:grid;
      grid-template-columns: auto auto;
      grid-template-rows: auto;
      grid-template-areas:
          'header image';
  }

  .login-box {
    /*rgba(0, 0, 0, 0.7);*/
    background-color: #FFFFFF;
    width: 300px;
    padding: 40px;
    border-radius: 30px;
    box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.3);
    text-align: center;
  }

    .login-box button {
      padding: 9px;
      margin-bottom: 1px;
      width: 25%;
    }

      /*Just a nice lil hover for the login button*/
      .login-box button:hover {
        background-color: lightgray;
      }

    .login-box input {
      padding: 9px;
      margin-bottom: 15px;
      width: 100%;
      border-radius: 5px;
      border: 1px solid #ccc;
    }

  .register-link {
    color: cornflowerblue;
  }

    .register-link:hover {
      color: darkblue;
    }

  .heading {
    color: black;
    text-align: left;
    padding: 10px;
    grid-area:header;
    
  }

  .headerImage {
    grid-area:image;
  }

  h3 {
    color: black;
  }
</style>
