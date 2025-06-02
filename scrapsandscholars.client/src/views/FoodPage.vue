<template>
  <section id="container">
    <div class="parallax">
    </div>
    <h1>Food that can be yours</h1>
  </section>
  <article>
    <input v-model="searchVal" placeholder="Search food..." />
  </article>
  <main>
    <Food v-for="(post, index) in filteredFoods"
          :key="post.id"
          :id="post.id"
          :title="post.title"
          :dateCooked="post.dateCooked"
          :postedBy="post.postedBy"
          :distance="post.distance"
          :timePosted="post.timePosted"
          :imageUrl="post.imageUrl"
          @view="viewFood"/>
  </main>
</template>

<script>
  import Food from '../components/foodClip.vue'
  import { ref, computed, onMounted } from 'vue'
  import { useRouter } from 'vue-router'
  import axios from 'axios'

  export default {
    name: 'FoodPage',
    components: { Food },

    // This component displays a list of food posts with a search functionality.
    setup() {
      // Reactive references for search value and food posts.
      const searchVal = ref('');

      // Reactive reference for the list of food posts.
      const foods = ref([]);

      // Use Vue Router to navigate to food details.
      const router = useRouter();

      // Computed property to filter foods based on the search value.
      const filteredFoods = computed(() => {

        // If foods is not an array, return an empty array.
        if (!Array.isArray(foods.value)) return [];

        // Filter foods based on the search value.
        return foods.value.filter(f =>
          f.title?.toLowerCase().includes(searchVal.value.toLowerCase())
        );
      });

      // Method to navigate to the food details page.
      // when a food item is clicked
      function viewFood(id) {
        router.push(`/foods/${id}`)
      }

      // hook to fetch food posts when the component is mounted.
      onMounted(async () => {
        try {
          // Make a GET request to fetch food posts from the API.
          const res = await axios.get('/api/student/login/posts');

          // Assign the fetched data to the foods reference.
          foods.value = res.data;
        } catch (err) {
          console.error("Failed to load posts", err);
        }
      });

      // Return the reactive references and methods to the template.
      return {
        searchVal,
        filteredFoods,
        foods,
        viewFood
      };
    }
  };
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

  main {
    width: 100%;
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr;
    grid-template-rows: 1fr 1fr 1fr;
    gap: 0px 0px;
    grid-auto-flow: row;
  }

  article {
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  input {
    display: block;
    position: relative;
    margin: auto;
    padding: 1%;
    width: 30%;
    margin: 3%;
  }

  main > section {
    margin: 5% auto 5% auto;
    flex: 0 0 20%;
  }

  @media screen and (max-width:1600px) {
    main {
      grid-template-columns: 1fr 1fr 1fr 1fr;
    }
  }

  @media screen and (max-width:1250px) {
    main {
      grid-template-columns: 1fr 1fr 1fr;
    }
  }

  @media screen and (max-width:950px) {
    main {
      grid-template-columns: 1fr 1fr;
    }
  }

  @media screen and (max-width:625px) {
    main {
      grid-template-columns: 1fr;
    }
  }
</style>
