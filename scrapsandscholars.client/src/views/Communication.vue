<template>
  <section class="chatroom">
    <input v-model="message"
           @keyup.enter="sendMessage"
           placeholder="Type your message..."
           autocomplete="off" />
  </section>
</template>

<script>
  import { ref } from 'vue'

  export default {
    name: 'Communications',
    setup() {
      const message = ref('')

      const sendMessage = () => {
        if (message.value.trim() === '') return
        console.log('Send message:', message.value) 
        message.value = ''
      }

      return {
        message,
        sendMessage,
      }
    },
  }
</script>

<style scoped>
  .chatroom {
    max-width: 600px;
    margin: 2rem auto;
    padding: 40rem;
    border: 1px solid #ddd;
    border-radius: 8px;
    font-family: Arial, sans-serif;
  }

  input {
    width: 100%;
    padding: 0.75rem;
    font-size: 1rem;
    border: 1px solid #ccc;
    border-radius: 6px;
    outline: none;
  }

    input:focus {
      border-color: #0057e7;
    }
</style>
