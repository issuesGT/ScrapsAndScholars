<template>
  <div v-if="food" class="claim-page">
    <!-- FOOD IMAGE -->
    <div class="image-container">
      <img :src="fullImageUrl" alt="food image" class="food-image" />
    </div>

    <!-- FOOD INFO -->
    <div class="info-container">
      <h1 class="title">{{ food.title }}</h1>
      <p class="meta">
        Cooked on: <strong>{{ food.dateCooked }}</strong>
      </p>
      <p class="meta">
        Posted by <strong>{{ food.postedBy }}</strong>
        ({{ food.timePosted }} ago, {{ food.distance }} km away)
      </p>
      <!-- Add any other fields you like here -->
    </div>

    <!-- PICK UP / DELIVERY BUTTONS -->
    <div class="actions">
      <button @click="handlePickUp" class="btn-pickup">
        Pick Up
      </button>
      <button @click="handleDelivery" class="btn-delivery">
        Delivery
      </button>
    </div>
  </div>

  <div v-else class="loading">Loading…</div>
</template>

<script>
  import axios from 'axios'
  import defaultImage from '../assets/images/heroImage.jpg'

  export default {
    name: 'ClaimFood',

    props: {
      id: {
        type: [String, Number],
        required: true
      }
    },

    data() {
      return {
        food: null
      }
    },

    created() {
      // ✅ Get postId from props
      const postId = this.id;

      axios
        .get(`/api/student/login/posts/${postId}`)
        .then(res => {
          this.food = res.data;
        })
        .catch(err => {
          console.error('Could not load post:', err);
        });
    },

    computed: {
      fullImageUrl() {
        if (!this.food?.imageUrl) {
          return defaultImage;
        }

        const raw = this.food.imageUrl;

        if (raw.startsWith('http')) {
          return raw;
        }

        const fileName = raw.split(/[\\/]/).pop();
        return `https://localhost:7174/uploads/${fileName}`;
      }
    },

    methods: {
      handlePickUp() {
        const postId = this.id; // ✅ From props
        const buyerId = this.$route.query.buyerId;

        if (!buyerId || !postId) {
          alert("Missing buyer or post ID.");
          return;
        }

        axios
          .post(`/api/student/login/claimPost/${buyerId}/${postId}`)
          .then(res => {
            alert(res.data.message);

            // ✅ Update localStorage with new credits
            const student = JSON.parse(localStorage.getItem("student")) || {};
            student.credits = res.data.buyerCredits;
            localStorage.setItem("student", JSON.stringify(student));

            // ✅ Trigger nav bar update
            window.dispatchEvent(new Event("storage"));
          })
          .catch(err => {
            const msg = err.response?.data?.message || "Could not claim post.";
            alert(msg);
          });
      },

      handleDelivery() {
        alert("Not available");
      }
    }
  }
</script>

<style scoped>
  .claim-page {
    display: flex;
    flex-direction: column;
    justify-content: center; /* vertical centering */
    align-items: center; /* horizontal centering */
    min-height: 100vh; /* fill viewport */
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

  .title {
    margin: 0 0 0.5rem;
    font-size: 2rem;
  }

  .meta {
    margin: 0.25rem 0;
    color: #555;
  }

  .actions {
    display: flex;
    gap: 1rem;
    margin-top: 1rem;
  }

  .btn-pickup,
  .btn-delivery {
    flex: 1;
    padding: 0.75rem 1rem;
    font-size: 1rem;
    border: none;
    border-radius: 4px;
    cursor: pointer;
    transition: background-color .2s ease-in-out;
  }

  /* Green for Pick Up */
  .btn-pickup {
    background-color: #28a745;
    color: white;
  }

    .btn-pickup:hover {
      background-color: #218838;
    }

  /* Blue for Delivery */
  .btn-delivery {
    background-color: #007bff;
    color: white;
  }

    .btn-delivery:hover {
      background-color: #0069d9;
    }

  .loading {
    text-align: center;
    padding: 2rem;
    color: #666;
  }
</style>

