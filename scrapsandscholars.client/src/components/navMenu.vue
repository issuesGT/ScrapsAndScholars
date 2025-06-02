<template>
  <nav :class="{scrolled: isScrolled}">
    <div class="handle" @click="toggleMenu">
      <i class="fa fa-bars menu" aria-hidden="true"></i>
    </div>
    <ul :class="{ show: showMenu }">
      <li class="logo"><h1>Scraps & Scholars</h1></li>
      <li><RouterLink to="/">Home</RouterLink></li>
      <li v-if="isLoggedIn"><RouterLink to="/food">Food</RouterLink></li>
      <li v-if="isLoggedIn"><RouterLink to="/">Work</RouterLink></li>
      <li v-if="isLoggedIn"><RouterLink to="/AddFood">Add Food</RouterLink></li>
      <li v-if="!isLoggedIn"><RouterLink to="/login">Login</RouterLink></li>
      <li v-if="isLoggedIn">Credits: {{ credits }}</li>

      <li v-if="isLoggedIn && isMobile"><RouterLink to="/">Profile</RouterLink></li>
      <li v-if="isLoggedIn && isMobile"><RouterLink @click="logout" to="">Logout</RouterLink></li>

      <li v-if="isLoggedIn && !isMobile" class="userProfile" @click="toggleDropdown">
        <RouterLink to="#">
          <i class="fa-solid fa-circle-user"></i>
        </RouterLink>
        <div v-if="dropdownOpen" class="dropdown-content" :class="{scrolled: isScrolled}">
          <a href="#">Profile</a>
          <a @click="logout" href="#">Logout</a>
        </div>
      </li>
    </ul>
  </nav>
</template>

<script>
  import axios from 'axios'
  import { RouterLink } from 'vue-router'

  export default {
    // This component represents the navigation menu of the application.
    name: 'navMenu',
    components: {
      RouterLink
    },
    data() {
      return {
        // Data properties for the navigation menu.
        email: null,
        windowWidth: window.innerWidth,
        showMenu: false,
        isScrolled: false,
        dropdownOpen: false,
        credits: null,
        userId: null,
        isLoggedInFlag: false,
      };
    },
    // Computed properties to determine the state of the menu and user.
    computed: {
      isLoggedIn() {
        // Check if the user is logged in by looking for a login token and student data in localStorage.
        return this.isLoggedInFlag && !!localStorage.getItem('student');
      },
      loggedInUser() {
        // Return the logged in user from localStorage or a default value.
        const student = JSON.parse(localStorage.getItem('student'));

        // If student data is not available, return 'Unknown'
        return student?.email || 'Unknown';
      },
      isMobile() {
        return this.windowWidth <= 850;
      }
    },
    watch: {
      $route() {
        // Load user data from localStorage whenever the route changes.
        this.loadUserFromStorage();
      }
    },
    // hooks to handle window events and load user data.
    created() {
      // Check if the user is logged in by looking for a login token and student data in localStorage.
      window.addEventListener('resize', this.handleResize);

      // Add scroll event listener to handle background color change on scroll.
      window.addEventListener('scroll', this.handleScroll);

      // Update credits on storage change
      window.addEventListener('storage', this.loadUserFromStorage);

      // Initialize windowWidth with the current window size when created.
      this.handleResize();

      // Load user data from localStorage.
      this.loadUserFromStorage();
    },
    unmounted() {

      // Remove event listeners when the component is unmounted.
      window.removeEventListener('resize', this.handleResize);

      // Remove scroll event listener to stop handling background color.
      window.removeEventListener('scroll', this.handleScroll);

      // Clean up event listener
      window.removeEventListener('storage', this.loadUserFromStorage);
    },
    methods: {
      // Method to toggle the visibility of the menu.
      toggleMenu() {
        this.showMenu = !this.showMenu;
      },
      // Method to toggle the dropdown menu for user profile.
      toggleDropdown() {
        this.dropdownOpen = !this.dropdownOpen;
      },

      // Method to check if the window has been scrolled down.
      handleScroll() {
        this.isScrolled = window.scrollY > 0;
      },

      // Method to check if the device is mobile based on window width.
      handleResize() {
        this.windowWidth = window.innerWidth;
      },

      logout() {
        // Clear user authentication and data from localStorage and reset local state, then redirect to login page.
        localStorage.removeItem('loginToken');
        localStorage.removeItem('student');
        this.userId = null;
        this.email = null;
        this.credits = null;
        this.$router.push('/login');
      },
      loadUserFromStorage() {
        // Load the student object from localStorage and update local state.
        // This keeps the navigation menu in sync with the users login status and info.
        const student = JSON.parse(localStorage.getItem('student'));
        this.isLoggedInFlag = !!student;
        if (student) {
          this.userId = student.id;
          this.email = student.email;
          this.credits = student.credits;
        } else {
          this.userId = null;
          this.email = null;
          this.credits = null;
        }
      }
    }
  };
</script>

<style scoped>
  nav {
    background-color: rgba(0, 0, 0, 0.3);
    transition: background-color 0.3s ease, position 0.3s;
    border-bottom: 1px solid #3b3b3b;
  }

    nav.scrolled {
      background-color: rgba(54, 69, 79, 1);
      width: 100%;
      margin: 0;
      padding: 0;
      position: fixed;
    }

    nav > ul > li, div > li {
      padding: 25px 10px 25px 0px;
      display: inline-block;
      list-style-type: none;
      color: white;
      text-decoration: none;
      border: 0 solid;
      transition: 0.15s ease-in-out;
    }

    nav > ul {
      margin: 0;
      padding: 0;
      animation: leftin 1.5s;
      -webkit-transition: max-height 0.4s;
      -ms-transition: max-height 0.4s;
      -moz-transition: max-height 0.4s;
      -o-transition: max-height 0.4s;
      transition: max-height 0.4s;
      margin-left: 60%;
    }

  .fadein2 {
    animation: leftin2 3s;
  }

  .height {
    animation: font 1.5s;
  }

  .height2 {
    animation: font2 1.5s;
  }

  @keyframes font {
    from {
      opacity: 0;
    }

    to {
      opacity: 100;
    }
  }

  @keyframes leftin2 {
    from {
      transform: scale(0);
      opacity: 0;
    }

    to {
      transform: scale(100%);
      opacity: 100;
    }
  }

  @keyframes leftin {
    from {
      transform: translateX(-50px);
      opacity: 0;
    }

    to {
      transform: translateX(0);
      opacity: 100;
    }
  }

  nav > ul > li > a:link {
    color: white;
    text-decoration: none;
    border: 0 solid;
    padding: 15px;
    transition: 0.15s ease-in-out;
  }

  nav > ul > li > a:visited {
    color: white;
    text-decoration: none;
  }

  nav > ul > li > a:focus {
    color: grey;
    padding: 15px;
  }

  nav > ul > li > a:hover {
    border-bottom: 1px solid;
    border-top: 1px solid;
    outline-color: rgba(255, 255, 255, 0);
    outline-offset: 15px;
    padding: 15px;
    text-shadow: 1px 1px 2px #427388;
  }

  nav > ul > li > a:active {
    color: black;
    text-decoration: none;
  }

  .logo {
    float: left;
    margin-left: -120%;
    width: 100%;
    padding-bottom: 0;
  }

  .userProfile {
    position: relative;
    color: white;
    font-size: 28px;
    display: inline-block;
  }

  .profileIcon {
    cursor: pointer;
    padding: 15px;
    transition: 0.15s ease-in-out;
  }

    .profileIcon:hover {
      border-bottom: 1px solid;
      border-top: 1px solid;
      text-shadow: 1px 1px 2px #427388;
    }

  .dropdown-content {
    position: absolute;
    right: 0;
    top: 100%;
    background-color: rgba(0, 0, 0, 0.3);
    border: 1px solid #3b3b3b;
    box-shadow: 0px 8px 16px rgba(0, 0, 0, 0.2);
    min-width: 160px;
    z-index: 3;
    padding: 10px 0;
    font-size: 18px;
  }

    .dropdown-content.scrolled {
      background-color: rgba(54, 69, 79, 1);
    }

    .dropdown-content a {
      display: block;
      padding: 15px 20px;
      color: white;
      text-decoration: none;
      transition: 0.2s;
    }

      .dropdown-content a:hover {
        background-color: rgba(255, 255, 255, 0.1);
        text-shadow: 1px 1px 2px #427388;
      }

  nav > ul > li > h1 {
    margin: 0;
    color: #f8f8f8;
  }

  .handle {
    width: 100%;
    background-color: rgba(7, 6, 12,0.5);
    text-align: left;
    box-sizing: border-box;
    cursor: pointer;
    padding: 10px 15px;
    color: white;
    font-size: 23px;
    display: none;
    font-weight: bold;
    text-shadow: none;
    transition: all 1250ms cubic-bezier(0.19, 1, 0.22, 1);
  }

  @media screen and (max-width:1370px) {
    nav > ul {
      margin-left: 50%;
    }

    .logo {
      float: left;
      margin-left: -80%;
      width: 50%;
      padding-bottom: 0;
    }
  }

  @media screen and (max-width:1090px) {
    nav > ul {
      margin-left: 30%;
    }

    .logo {
      float: left;
      margin-left: -20%;
      width: 40%;
      padding-bottom: 0;
    }
  }

  @media screen and (max-width:990px) {
    nav > ul {
      margin-left: 20%;
    }

    .logo {
      float: left;
      margin-left: -20%;
      width: 40%;
      padding-bottom: 0;
    }
  }

  @media screen and (max-width:870px) {

    .logo {
      display: none;
    }

    .handle {
      display: block;
      padding: 20px 20px 20px 20px;
    }

    .show {
      max-height: 28em;
    }

    nav > ul {
      overflow: hidden;
      max-height: 0px;
      margin-left: 0%;
    }

      nav > ul > li {
        box-sizing: border-box;
        width: 100%;
        padding: 25px;
        text-align: left;
        font-size: 20px;
        margin: 0;
      }
  }
</style>
