<template>
  <div v-if="food" class="food-details">
    <!-- IMAGE -->
    <div class="image-container">
      <img :src="fullImageUrl" alt="food image" class="food-image" />
    </div>

    <!-- INFO -->
    <div class="info-container">
      <h1>{{ food.title }}</h1>
      <p class="food-description">
        {{ food.description }}
      </p>
      <p>Portion size: {{ food.portion }}</p>
      <p>Cooked on: {{ food.dateCooked }}</p>
      <p>
        Posted by {{ food.postedBy }}
        ({{ food.timePosted }} ago, {{ food.distance }} km away)
      </p>
    </div>

    <!-- ACTION -->
    <div class="actions">
      <button @click="claimFood" class="btn-claim">Claim</button>
    </div>
  </div>

  <div v-else class="loading">
    Loading…
  </div>
</template>

<script>
  import axios from 'axios'
  import defaultImage from '../assets/images/heroImage.jpg'

  export default {
    name: 'FoodDetails',

    data() {
      return {
        // Reactive data property to hold the food post details.
        food: null
      }
    },

    // hook to fetch food post details when the component is created.
    created() {
      // Get the food post ID from the  global route parameter.
      const id = this.$route.params.id
      axios
        .get(`/api/student/login/posts/${id}`)
        .then(res => {
          // Assign the fetched food post data to the food property.
          this.food = res.data
        })
        .catch(err => {
          console.error('Could not load post:', err)
        })
    },

    // Computed property to generate the full image URL.
    computed: {
      fullImageUrl() {
        // If no food post or image URL return default image.
        if (!this.food?.imageUrl) {
          return defaultImage
        }

        // Get the raw image URL from the food post.
        const raw = this.food.imageUrl

        if (raw.startsWith('http')) {
          return raw
        }

        // If the image URL is a local path, extract the file name
        // and construct the full URL to serve from wwwroot/uploads.
        // Split on either / or \\ depending on the OS.
        const fileName = raw.split(/[\\/]/).pop()
        // Return the full URL to the image.
        return `https://localhost:7174/uploads/${fileName}`
      }
    },

    methods: {
      // Claim food
      claimFood() {
        if (!this.food || !this.food.id) {
          console.warn("No food or no ID to claim")
          return
        }

        const student = JSON.parse(localStorage.getItem("student")) || {}
        const buyerId = student.id

        if (!buyerId) {
          alert("You must be logged in to claim.")
          return
        }

        this.$router.push({
          path: `/foods/claim/${this.food.id}`,
          query: { buyerId }
        })
      }
    }
  }
</script>

<style scoped>
  .food-details {
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
    padding: 1rem;
    width: 90%;
    max-width: 600px;
    margin: 0 auto;
    background: #fff;
    box-shadow: 0 2px 8px rgba(0,0,0,0.1);
    border-radius: 8px;
  }

  .image-container {
    width: 300px;
    height: 200px;
    overflow: hidden;
    border-radius: 8px;
    box-shadow: 0 1px 4px rgba(0,0,0,0.1);
    margin-bottom: 1.5rem;
  }

  .food-image {
    width: 100%;
    height: 100%;
    object-fit: cover;
  }

  .info-container {
    text-align: center;
    margin-bottom: 1.5rem;
  }

  .food-description {
    margin: 0.5rem 0;
    font-style: italic;
    color: #555;
  }

  .actions {
    text-align: center;
    margin-bottom: 1rem;
  }

  .btn-claim {
    background-color: #28a745;
    color: white;
    border: none;
    padding: 0.75rem 1.5rem;
    font-size: 1rem;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color .2s ease-in-out;
  }

    .btn-claim:hover {
      background-color: #218838;
    }

  .loading {
    text-align: center;
    padding: 2rem;
    color: #666;
  }
</style>

