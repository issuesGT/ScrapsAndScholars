<template>
  <section class="foodCard">
    <img class="foodImage" :src="fullImageUrl" alt="food image" />

    <div class="foodInfo">
      <h2 class="foodTitle">{{ shortWord(title, 20) }}</h2>
      <p class="food">Made: {{ dateCooked }}</p>
    </div>

    <div class="userInfo">
      <img class="userAvatar" :src="fullImageUrl" alt="profile image" />
      <div class="user">
        <p class="userName clickable" @click.stop="goToChat">{{ shortWord(postedBy, 10) }}</p>
        <p class="userDistance">{{ distance }} km</p>
        <p class="postedTime">{{ timePosted }}</p>
      </div>
    </div>

    <button @click="handleClick" class="claim">View</button>
  </section>
</template>

<script>
  import image from "../assets/images/heroImage.jpg"

  export default {
    name: 'foodClip',
    props: {
      id: {
        type: [String, Number],
        required: true
      },
      title: String,
      dateCooked: String,
      postedBy: String,
      distance: String,
      timePosted: String,
      imageUrl: {
        type: String,
        default: image
      }
    },
    computed: {
      fullImageUrl() {
        if (!this.imageUrl) return require('@/assets/images/heroImage.jpg');
        return this.imageUrl.startsWith("http")
          ? this.imageUrl
          : `https://localhost:7174${this.imageUrl}`;
      }
    },
    methods: {
      shortWord(text, max = 20) {
        return text.length > max ? text.slice(0, max) + '...' : text;
      },
      handleClick() {
        this.$emit('view', this.id);
      },
      goToChat() {
        this.$router.push({ path: '/chatroom', query: { user: this.postedBy } });
      }
    }
  }
</script>


<style>
  .foodCard {
    width: 280px;
    border: 1px solid #ccc;
    border-radius: 12px;
    overflow: hidden;
    font-family: sans-serif;
    box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    display: flex;
    flex-direction: column;
    background: white;
  }

  .foodImage {
    width: 90%;
    border-radius: 20px;
    height: 10px;
    margin: auto;
    margin-top: 5%;
    height: 150px;
    object-fit: cover;
  }

  .foodInfo {
    padding: 0.5rem 1rem;
  }

  .foodTitle {
    font-size: 1.2rem;
    font-weight: bold;
    margin: 0;
  }

  .food {
    color: #666;
    margin: 0.2rem 0 0;
  }

  .userInfo {
    display: flex;
    align-items: center;
    padding: 0.5rem 1rem;
    border-top: 1px solid #eee;
  }

  .userAvatar {
    width: 48px;
    height: 48px;
    border-radius: 50%;
    margin-right: 1rem;
    object-fit: cover;
  }

  .user {
    font-size: 0.9rem;
  }

  .userName {
    font-weight: bold;
    margin: 0;
  }

  .userDistance,
  .postedTime {
    margin: 0;
    color: #777;
    font-size: 0.85rem;
  }

  .claim {
    background-color: #0057e7;
    color: white;
    width: 80%;
    margin: auto;
    border-radius: 10px;
    margin-bottom: 5%;
    padding: 0.75rem;
    font-size: 1rem;
    font-weight: bold;
    border: none;
    cursor: pointer;
    transition: background 0.3s ease;
  }

    .claim:hover {
      background-color: #0041b3;
    }

  .clickable {
    color: #0057e7;
    cursor: pointer;
    text-decoration: underline;
  }
</style>
